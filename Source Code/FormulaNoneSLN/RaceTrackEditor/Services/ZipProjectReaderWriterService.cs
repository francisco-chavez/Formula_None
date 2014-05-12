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
		public static readonly string FILE_EXTENSION = "fnp";

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

			using (Package package = Package.Open(projectFilePath, FileMode.CreateNew))
			{
			}

			return null;
		}

		private string GetFilePath(string directoryPath, string projectName)
		{
			string fileName= string.Format("{0}.{1}", projectName, FILE_EXTENSION);
			string filePath = Path.Combine(directoryPath, fileName);
			return filePath;
		}
		#endregion
	}
}
