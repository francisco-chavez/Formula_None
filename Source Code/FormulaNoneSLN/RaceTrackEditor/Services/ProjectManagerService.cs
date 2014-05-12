using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

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
			NewProjectInfoModel newProjectInfo = new NewProjectInfoModel();
			var dialog = new NewProjectDialog()
			{
				Owner = App.Current.MainWindow
			};

			newProjectInfo.CreateProject				= dialog.ShowDialog() == true;

			newProjectInfo.ProjectLocation		= dialog.ProjectLocation;
			newProjectInfo.ProjectName			= dialog.ProjectName;
			newProjectInfo.RaceTrackImagePath	= dialog.RaceTrackImagePath;

			return newProjectInfo;
		}

		public ProjectModel CreateNewProject(NewProjectInfoModel projectInfo)
		{
			if (!projectInfo.CreateProject)
				throw new Exception("Can not create a project that is flagged as do not create.");
			throw new NotImplementedException();
		}

		public void SaveCurrentProject()
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
