using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

using Unv.RaceTrackEditor.Core;
using Unv.RaceTrackEditor.Core.Models;
using Unv.RaceTrackEditor.Core.Zip.Models;


namespace Unv.RaceTrackEditor.Core.Zip
{
	public class ZipProjectReaderWriter
		: IProjectFileReader, IProjectFileWriter
	{
		#region Attributes
		private static readonly Uri IMAGE_URI			= new Uri("/RaceTrackImage.png", UriKind.Relative);
		private static readonly Uri OBSTACLE_DATA_URI	= new Uri("/ObstacleData.xml", UriKind.Relative);

		private ProjectManagerZip m_projectManager;
		#endregion


		#region Properties
		public string FileExtension 
		{
			get{return m_fileExtension;}
			set{m_fileExtension = value;}
		}
		private string m_fileExtension = "zip";

		public string ExtensionDescription
		{
			get{return m_extensionDescription;}
			set{m_extensionDescription = value;}
		}
		private string m_extensionDescription = "Formula None Project";
		#endregion


		#region Constructors
		public ZipProjectReaderWriter() 
			: this(null)
		{ 
		}

		public ZipProjectReaderWriter(ProjectManagerZip projectManager)
		{
			m_projectManager = projectManager;
		}
		#endregion


		#region Methods
		public ProjectModel OpenProject(string filePath)
		{
			if (File.Exists(filePath))
			{
				var projectModel = new ProjectModelZip(m_projectManager);
				projectModel.ProjectFilePath = filePath;

				BitmapImage			image			= null;
				ObstacleDataModel	obstacleData	= null;

				using (Package package = ZipPackage.Open(projectModel.ProjectFilePath, FileMode.Open))
				{
					if (package.PartExists(IMAGE_URI))
					{
						PackagePart imagePackagePart = package.GetPart(IMAGE_URI);

						image = new BitmapImage();
						image.BeginInit();
						image.CacheOption = BitmapCacheOption.Default;
						image.StreamSource = imagePackagePart.GetStream(FileMode.Open, FileAccess.Read);
						image.EndInit();
					}

					if (package.PartExists(OBSTACLE_DATA_URI))
					{
						PackagePart obstacleDataPart = package.GetPart(OBSTACLE_DATA_URI);
						XmlSerializer ser = new XmlSerializer(typeof(ObstacleDataModel));

						obstacleData = (ObstacleDataModel) ser.Deserialize(obstacleDataPart.GetStream());
					}
				}

				if (image != null || obstacleData != null)
				{
					projectModel.RaceTrackModel = new RaceTrackModel();

					projectModel.RaceTrackModel.RaceTrackImage	= image;
					projectModel.RaceTrackModel.Obstacles		= obstacleData;
				}

				return projectModel;
			}
			else
			{
				throw new IOException(string.Format("Project File: {0} not found.", filePath));
			}
		}

		public ProjectModel CreateNewProject(NewProjectInfoModel projectInformation)
		{
			string	filePath		= projectInformation.FilePath;
			bool	canCreateNew	= !File.Exists(filePath);
			string	projectName		= Path.GetFileNameWithoutExtension(filePath);

			if (!canCreateNew)
			{
				var userInput = 
			        MessageBox.Show(
						string.Format("Project: {0} already exists in selected location. Do you wish to replace it?", projectName),
						"Problem with Project Creation",
						MessageBoxButton.YesNo,
						MessageBoxImage.Question);

				if (userInput == MessageBoxResult.Yes)
				{
					while (File.Exists(filePath))
					{
						bool forgetIt = false;
						try
						{
							File.Delete(filePath);

							// Give the system some time to get rid of the file
							// Side Note: On a slow system or a system underload,
							//			  this might not be enough time.
							Thread.Sleep(5);
						}
						catch (Exception ex)
						{
							userInput =
								MessageBox.Show(
								"There was a problem getting rid of the old project file. Do you wish to try again?",
								"Deletion Problem",
								MessageBoxButton.YesNo,
								MessageBoxImage.Error);

							forgetIt = userInput == MessageBoxResult.No;
						}

						if (forgetIt)
							return null;
					}
				}
				else
				{
					return null;
				}
			}


			var defaultLayer = new ObstacleLayerModel()
			{
				LayerName = "Layer 1"
			};
			defaultLayer.Obstacles.Add(new ObstacleModel() { X = 10, Y = 15 });
			defaultLayer.Obstacles.Add(new ObstacleModel() { X = 50, Y = 20 });

			var obstacleData = new ObstacleDataModel();
			obstacleData.AddLayer(defaultLayer);

			var raceTrackModel = new RaceTrackModel()
			{
				RaceTrackImage	= null,
				Obstacles		= obstacleData
			};

			var projectModel = new ProjectModelZip(m_projectManager)
			{
				ProjectFilePath = filePath,
				RaceTrackModel	= raceTrackModel
			};

			SaveProject(projectModel);

			return projectModel;
		}

		public void SaveProject(ProjectModel projectModel)
		{
			SaveProject(projectModel, null);
		}

		public void SaveRaceTrackImage(ProjectModel projectModel, string imagePath)
		{
			SaveProject(projectModel, imagePath);
		}


		private void SaveProject(ProjectModel projectModel, string imagePath)
		{
			if (projectModel.RaceTrackModel == null && !string.IsNullOrWhiteSpace(imagePath))
				projectModel.RaceTrackModel = new RaceTrackModel();

			using (Package package = ZipPackage.Open(projectModel.ProjectFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
			{
				if (projectModel.RaceTrackModel != null)
				{
					if (!string.IsNullOrWhiteSpace(imagePath))
					{
						PackagePart imagePackagePart;

						if (package.PartExists(IMAGE_URI))
							imagePackagePart = package.GetPart(IMAGE_URI);
						else
							imagePackagePart = package.CreatePart(IMAGE_URI, "Image/PNG");

						using (FileStream imageFileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
						{
							CopyStream(imageFileStream, imagePackagePart.GetStream());
						}

						BitmapImage image = new BitmapImage();
						image.BeginInit();
						image.CacheOption = BitmapCacheOption.Default;
						image.StreamSource = imagePackagePart.GetStream(FileMode.Open, FileAccess.Read);
						image.EndInit();

						projectModel.RaceTrackModel.RaceTrackImage = image;
					}

					if (projectModel.RaceTrackModel.Obstacles != null)
					{
						
						PackagePart obstaclePackagePart;
						
						if(package.PartExists(OBSTACLE_DATA_URI))
							obstaclePackagePart = package.GetPart(OBSTACLE_DATA_URI);
						else
							obstaclePackagePart = package.CreatePart(OBSTACLE_DATA_URI, MediaTypeNames.Text.Xml);
						
						XmlSerializer ser = new XmlSerializer(typeof(ObstacleDataModel));

						ser.Serialize(obstaclePackagePart.GetStream(), projectModel.RaceTrackModel.Obstacles);
					}
				}
			}
		}

		private static void CopyStream(Stream source, Stream target)
		{
			const int bufferSize = 0x1000;
			byte[] buffer = new byte[bufferSize];
			int bytesRead = 0;
			while ((bytesRead = source.Read(buffer, 0, bufferSize)) > 0)
				target.Write(buffer, 0, bytesRead);
		}
		#endregion
	}
}
