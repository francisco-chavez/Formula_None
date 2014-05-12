using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Microsoft.Win32;

using Unv.RaceTrackEditor.Core;
using Unv.RaceTrackEditor.Core.Models;
using Unv.RaceTrackEditor.Dialogs;


namespace Unv.RaceTrackEditor.Services
{
	public class ProjectManagerService
		: IProjectManager
	{
		#region Attributes
		#endregion


		#region Properties
		public ProjectModel CurrentProject
		{
			get { return mn_currentProject; }
			set
			{
				if (mn_currentProject != null && mn_currentProject != value)
				{
					var userInput =
						MessageBox.Show(
						"Do you wish to save the current project progress before switching projects?", 
						"Save Project", 
						MessageBoxButton.YesNo, 
						MessageBoxImage.Question);

					if (userInput == MessageBoxResult.Yes)
						SaveCurrentProject();
				}

				mn_currentProject = value;
			}
		}
		private ProjectModel mn_currentProject;

		public IProjectFileReader ProjectReader { get; set; }
		public IProjectFileWriter ProjectWriter { get; set; }
		#endregion


		#region Constructors
		public ProjectManagerService() { }
		#endregion


		#region Methods
		public NewProjectInfoModel GetNewProjectInfo()
		{
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.CreatePrompt = false;
			dlg.Title = "Select New Project File";
			dlg.Filter = string.Format("{0} (*.{1})|*.{1}", ProjectWriter.ExtensionDescription, ProjectWriter.FileExtension);

			bool keepGoing = dlg.ShowDialog(App.Current.MainWindow) == true;

			NewProjectInfoModel info = new NewProjectInfoModel();
			info.CreateProject = keepGoing;
			info.FilePath = dlg.FileName;

			return info;
		}

		public ProjectModel CreateNewProject(NewProjectInfoModel projectInfo)
		{
			if (!projectInfo.CreateProject)
				throw new Exception("Can not create a project that is flagged as do not create.");

			var newProject = this.ProjectWriter.CreateNewProject(projectInfo);
			newProject.ProjectManager = this;

			return newProject;
		}

		public void SaveCurrentProject()
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
