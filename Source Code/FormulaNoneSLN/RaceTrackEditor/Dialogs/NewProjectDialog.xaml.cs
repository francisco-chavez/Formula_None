using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Microsoft.Win32;

using DialogResult	= System.Windows.Forms.DialogResult;
using Forms			= System.Windows.Forms;


namespace Unv.RaceTrackEditor.Dialogs
{
	/// <summary>
	/// Interaction logic for NewProjectDialog.xaml
	/// </summary>
	public partial class NewProjectDialog 
		: Window
	{
		#region Attributes
		public static readonly DependencyProperty ProjectNameProperty;
		public static readonly DependencyProperty ProjectLocationProperty;
		public static readonly DependencyProperty RaceTrackImagePathProperty;
		#endregion


		#region Properties
		public string ProjectName
		{
			get { return (string) GetValue(ProjectNameProperty); }
			set { SetValue(ProjectNameProperty, value); }
		}

		public string ProjectLocation
		{
			get { return (string) GetValue(ProjectLocationProperty); }
			set { SetValue(ProjectLocationProperty, value); }
		}

		public string RaceTrackImagePath
		{
			get { return (string) GetValue(RaceTrackImagePathProperty); }
			set { SetValue(RaceTrackImagePathProperty, value); }
		}
		#endregion


		#region Commands
		public ICommand SelectProjectLocationCommand
		{
			get
			{
				if (m_selectProjectLocationCommand == null)
					m_selectProjectLocationCommand = new RelayCommand(p => this.ProjectLocationDialog());
				return m_selectProjectLocationCommand;
			}
		}
		private ICommand m_selectProjectLocationCommand;
		#endregion


		#region Constructors
		public NewProjectDialog()
		{
			InitializeComponent();
			this.DataContext = this;
		}

		static NewProjectDialog()
		{
			ProjectNameProperty = DependencyProperty.Register(
				"ProjectName",
				typeof(string),
				typeof(NewProjectDialog));

			ProjectLocationProperty = DependencyProperty.Register(
				"ProjectLocation",
				typeof(string),
				typeof(NewProjectDialog));

			RaceTrackImagePathProperty = DependencyProperty.Register(
				"RaceTrackImagePath",
				typeof(string),
				typeof(NewProjectDialog),
				new PropertyMetadata(OnRaceTrackImagePathChanged));
		}
		#endregion


		#region Dependency Property Event Handlers
		private static void OnRaceTrackImagePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
		}
		#endregion


		#region Methods
		private void ProjectLocationDialog()
		{
			var dialog = new Forms.FolderBrowserDialog();
			dialog.ShowNewFolderButton = true;

			var dialogResult = dialog.ShowDialog();

			switch (dialogResult)
			{
			case Forms.DialogResult.Abort:
			case Forms.DialogResult.Cancel:
			case Forms.DialogResult.Ignore:
			case Forms.DialogResult.No:
			case Forms.DialogResult.None:
			case Forms.DialogResult.Retry:
				break;

			case Forms.DialogResult.OK:
			case Forms.DialogResult.Yes:
				this.ProjectLocation = dialog.SelectedPath;
				break;
			}
		}
		#endregion
	}
}
