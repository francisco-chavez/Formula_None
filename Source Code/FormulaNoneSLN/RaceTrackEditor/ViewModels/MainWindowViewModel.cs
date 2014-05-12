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

		private void ExitApplication()
		{
			App.Current.Shutdown();
		}
		#endregion
	}
}
