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
		public static readonly string FILE_EXTENSION = "zip";

		#region Constructors
		public ZipProjectReaderWriterService() 
		{ 
		}
		#endregion


		#region Methods
		public bool CanCreateNewProject(string directory, string projectName)
		{
			string filePath = GetFilePath(directory, projectName);

			bool canCreateNew = !File.Exists(filePath);
			return canCreateNew;
		}

		public ProjectModel CreateNewProject(NewProjectInfoModel projectInformation)
		{
			bool	canCreateNew	= CanCreateNewProject(projectInformation.ProjectLocation, projectInformation.ProjectName);
			string	projectFilePath = GetFilePath(projectInformation.ProjectLocation, projectInformation.ProjectName);

			if (!canCreateNew)
			{
				var userInput = 
					MessageBox.Show(
						string.Format("Project: {0} already exists. Do you wish to replace it?", projectInformation.ProjectName), 
						"Problem with Project Creation", 
						MessageBoxButton.YesNo, 
						MessageBoxImage.Question);

				if (userInput == MessageBoxResult.Yes)
				{
					while (File.Exists(projectFilePath))
					{
						bool forgetIt = false;
						try
						{
							File.Delete(projectFilePath);

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


			// Place project information into project file
			Uri targetImageUri = new Uri("/RaceTrackImage.png", UriKind.Relative);
			
			using(Package package = ZipPackage.Open(projectFilePath, FileMode.CreateNew))
			{
				PackagePart imagePackagePart = package.CreatePart(targetImageUri, "Image/png");

				using (FileStream imageFileStream = new FileStream(projectInformation.RaceTrackImagePath, FileMode.Open, FileAccess.Read))
				{
					CopyStream(imageFileStream, imagePackagePart.GetStream());
				}
			}

			ProjectModel result = new ProjectModel()
			{
			};

			return result;
		}

		private string GetFilePath(string directoryPath, string projectName)
		{
			string fileName = string.Format("{0}.{1}", projectName, FILE_EXTENSION);
			string filePath = Path.Combine(directoryPath, fileName);
			return filePath;
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
