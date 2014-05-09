using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

using Unv.RaceTrackEditor.Dialogs;


namespace Unv.RaceTrackEditor.ViewModels
{
	public class MainWindowViewModel
		: ViewModelBase
	{
		public ICommand CreateNewProjectCommand
		{
			get
			{
				if(m_createNewProjectCommand == null)
				{
					m_createNewProjectCommand =
						new RelayCommand(p => this.CreateNewProject());
				}

				return m_createNewProjectCommand;
			}
		}
		private RelayCommand m_createNewProjectCommand;


		public MainWindowViewModel()
			: base("Race Track Editor")
		{
		}


		private void CreateNewProject()
		{
			var dialog = new NewProjectDialog();
			dialog.Owner = App.Current.MainWindow;

			var keepGoing = dialog.ShowDialog() == true;
		}
	}
}
