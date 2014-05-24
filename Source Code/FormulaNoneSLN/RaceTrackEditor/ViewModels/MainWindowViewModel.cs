using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

using Unv.RaceTrackEditor.Core;
using Unv.RaceTrackEditor.Core.Models;


namespace Unv.RaceTrackEditor.ViewModels
{
	public class MainWindowViewModel
		: ViewModelBase
	{
		#region Attributes
		#endregion


		#region Properties
		public IProjectManager ProjectManager { get; set; }

		public ProjectViewModel ProjectViewModel
		{
			get { return mn_projectViewModel; }
			set
			{
				if (mn_projectViewModel != value)
				{
					mn_projectViewModel = value;
					OnPropertyChanged("ProjectViewModel");
				}
			}
		}
		private ProjectViewModel mn_projectViewModel;
		
		#endregion


		#region Commands
		public ICommand CreateNewProjectCommand
		{
			get
			{
				if(mn_createNewProjectCommand == null)
				{
					mn_createNewProjectCommand =
						new RelayCommand(p => this.CreateNewProject());
				}

				return mn_createNewProjectCommand;
			}
		}
		private RelayCommand mn_createNewProjectCommand;

		public ICommand ExitApplicationCommand
		{
			get
			{
				if (mn_exitApplicationCommand == null)
					mn_exitApplicationCommand = new RelayCommand(p => ExitApplication());
				return mn_exitApplicationCommand;
			}
		}
		private RelayCommand mn_exitApplicationCommand;

		public ICommand SaveProjectCommand
		{
			get
			{
				if (mn_saveProjectCommand == null)
					mn_saveProjectCommand = new RelayCommand(p => SaveProject(), p => CanSaveProject(null));
				return mn_saveProjectCommand;
			}
		}
		private RelayCommand mn_saveProjectCommand;

		public ICommand OpenProjectCommand
		{
			get
			{
				if (mn_openProjectCommand == null)
					mn_openProjectCommand = new RelayCommand(p => OpenProject());
				return mn_openProjectCommand;
			}
		}
		private RelayCommand mn_openProjectCommand;


		public ICommand SelectRaceTrackImageCommand
		{
			get
			{
				if (mn_selectRaceTrackImageCommand == null)
					mn_selectRaceTrackImageCommand = 
						new RelayCommand(p => SelectRaceTrackImage(), p => CanSelectRaceTrackImage(null));
				
				return mn_selectRaceTrackImageCommand;
			}
		}
		private RelayCommand mn_selectRaceTrackImageCommand;

		public ICommand ExportRaceTrackObstaclesCommand
		{
			get
			{
				if (mn_exportRaceTrackObstaclesCommand == null)
					mn_exportRaceTrackObstaclesCommand = new RelayCommand(p => ExportRaceTrackObstacles(), p => CanExportRaceTrackObstacles(null));
				return mn_exportRaceTrackObstaclesCommand;
			}
		}
		private RelayCommand mn_exportRaceTrackObstaclesCommand;
		#endregion


		#region Constructors
		public MainWindowViewModel()
			: base("Race Track Editor")
		{
			ProjectManager = App.ProjectManager;
		}
		#endregion


		#region Methods
		private void CreateNewProject()
		{

			NewProjectInfoModel info = ProjectManager.GetNewProjectInfo();
			
			if (!info.CreateProject)
				return;

			var projectModel = ProjectManager.CreateNewProject(info);

			this.ProjectViewModel = new ProjectViewModel(projectModel);
		}

		private void SaveProject()
		{
			RebuildModel();
			ProjectManager.SaveCurrentProject();
		}

		private bool CanSaveProject(object parameter)
		{
			if (this.ProjectViewModel == null || this.ProjectViewModel.ProjectModel == null)
				return false;

			return true;
		}

		private void OpenProject()
		{
			var projectModel = ProjectManager.OpenProject();

			if (projectModel == null)
				return;

			this.ProjectViewModel = new ProjectViewModel(projectModel);
		}

		private void ExitApplication()
		{
			App.Current.Shutdown();
		}


		private void SelectRaceTrackImage()
		{
			this.ProjectViewModel.SelectRaceTrackImage();
		}

		private bool CanSelectRaceTrackImage(object parameters)
		{
			return !(this.ProjectViewModel == null || this.ProjectViewModel.ProjectModel == null);
		}

		private void ExportRaceTrackObstacles()
		{
		}

		private bool CanExportRaceTrackObstacles(object parameters)
		{
			return false;
		}


		public override void RebuildModel()
		{
			this.ProjectViewModel.RebuildModel();
			base.RebuildModel();
		}
		#endregion
	}
}
