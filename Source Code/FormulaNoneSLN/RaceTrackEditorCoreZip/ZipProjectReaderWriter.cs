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


			var defaultLayer = new ObstacleLayerModelZip()
			{
				LayerName = "Layer 1"
			};

			var obstacleData = new ObstacleDataModelZip();
			obstacleData.AddObstacleLayer(defaultLayer);

			var raceTrackModel = new RaceTrackModelZip()
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
			Uri imageDestinationUri			= new Uri("/RaceTrackImage.png", UriKind.Relative);
			Uri obstacleDataDestinationUri	= new Uri("/ObstacleData.xml", UriKind.Relative);

			using (Package package = ZipPackage.Open(projectModel.ProjectFilePath, FileMode.Create))
			{
				if (projectModel.RaceTrackModel != null)
				{
					if (projectModel.RaceTrackModel.RaceTrackImage != null)
					{
						PackagePart imagePackagePart = package.CreatePart(imageDestinationUri, "Image/PNG");
						CopyStream(projectModel.RaceTrackModel.RaceTrackImage.StreamSource, imagePackagePart.GetStream());
					}

					if (projectModel.RaceTrackModel.Obstacles != null)
					{
						PackagePart obstaclePackagePart = 
							package.CreatePart(obstacleDataDestinationUri, MediaTypeNames.Text.Xml);
						XmlSerializer ser = new XmlSerializer(typeof(ObstacleDataModelZip));

						ser.Serialize(obstaclePackagePart.GetStream(), projectModel.RaceTrackModel.Obstacles);
					}
				}
			}
		}

		public ProjectModel OpenProject(string filePath)
		{
			if (File.Exists(filePath))
			{
				var projectModel = new ProjectModelZip(m_projectManager);
				projectModel.ProjectFilePath = filePath;

				Uri imageUri = new Uri("/RaceTrackImage.png", UriKind.Relative);

				BitmapImage image = null;

				using (Package package = ZipPackage.Open(projectModel.ProjectFilePath, FileMode.Open))
				{
					if (package.PartExists(imageUri))
					{
						PackagePart imagePackagePart = package.GetPart(imageUri);

						image = new BitmapImage();
						image.BeginInit();
						image.CacheOption = BitmapCacheOption.Default;
						image.StreamSource = imagePackagePart.GetStream(FileMode.Open, FileAccess.Read);
						image.EndInit();
					}
				}

				if (image != null)
				{
					projectModel.RaceTrackModel = new RaceTrackModel();
					projectModel.RaceTrackModel.RaceTrackImage = image;
				}

				return projectModel;
			}
			else
			{
				throw new IOException(string.Format("Project File: {0} not found.", filePath));
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

		public BitmapImage SaveRaceTrackImage(ProjectModel projectModel, string imagePath)
		{
			Uri imageDestinationUri = new Uri("/RaceTrackImage.png", UriKind.Relative);

			BitmapImage image = null;

			using (Package package = ZipPackage.Open(projectModel.ProjectFilePath, FileMode.Create))
			{
				PackagePart imagePackagePart = package.CreatePart(imageDestinationUri, "Image/PNG");

				using (FileStream imageFileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
				{
					CopyStream(imageFileStream, imagePackagePart.GetStream());
				}

				image = new BitmapImage();
				image.BeginInit();
				image.CacheOption = BitmapCacheOption.Default;
				image.StreamSource = imagePackagePart.GetStream(FileMode.Open, FileAccess.Read);
				image.EndInit();
				
			}

			return image;
		}
		#endregion
	}
}
