using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;

using Unv.RaceTrackEditor.Core;
using Unv.RaceTrackEditor.Core.Models;


namespace Unv.RaceTrackEditor.Services
{
	public class ZipProjectReaderWriterService
		: IProjectFileReader, IProjectFileWriter
	{
		#region Properties
		public string FileExtension { get { return FILE_EXTENSION; } }
		private static readonly string FILE_EXTENSION = "zip";

		public string ExtensionDescription { get { return EXTENSION_DESCRIPTION; } }
		private static readonly string EXTENSION_DESCRIPTION = "Formula None Project";
		#endregion


		#region Constructors
		public ZipProjectReaderWriterService() 
		{ 
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

			using (Package package = ZipPackage.Open(filePath))
			{
			}

			var projectModel = new ProjectModel()
			{
				ProjectFilePath = filePath
			};

			return projectModel;


			// Place project information into project file
			//Uri targetImageUri = new Uri("/RaceTrackImage.png", UriKind.Relative);
			
			//using(Package package = ZipPackage.Open(projectFilePath, FileMode.CreateNew))
			//{
			//    PackagePart imagePackagePart = package.CreatePart(targetImageUri, "Image/png");

			//    using (FileStream imageFileStream = new FileStream(projectInformation.RaceTrackImagePath, FileMode.Open, FileAccess.Read))
			//    {
			//        CopyStream(imageFileStream, imagePackagePart.GetStream());
			//    }
			//}
		}

		public ProjectModel OpenProject(string filePath)
		{
			if (File.Exists(filePath))
			{
				ProjectModel projectModel = new ProjectModel();
				projectModel.ProjectFilePath = filePath;
				return projectModel;
			}
			else
			{
				throw new IOException(string.Format("Project File: {0} not found.", filePath));
			}
			throw new NotImplementedException();
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
