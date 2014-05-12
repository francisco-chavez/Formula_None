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
		#endregion


		public MainWindowViewModel()
			: base("Race Track Editor")
		{
			ProjectManager = App.ProjectManager;
		}


		private void CreateNewProject()
		{

			NewProjectInfoModel info = ProjectManager.GetNewProjectInfo();
			
			if (!info.CreateProject)
				return;

			var projectModel = ProjectManager.CreateNewProject(info);
		}
	}
}
