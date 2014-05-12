using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

using Microsoft.Win32;

using Forms		= System.Windows.Forms;
using IOPath	= System.IO.Path;


namespace Unv.RaceTrackEditor.Dialogs
{
	/// <summary>
	/// Interaction logic for NewProjectDialog.xaml
	/// </summary>
	public partial class NewProjectDialog 
		: Window, INotifyPropertyChanged
	{
		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion


		#region Attributes
		public static readonly DependencyProperty ProjectPathProperty;
		public static readonly DependencyProperty RaceTrackImagePathProperty;
		#endregion


		#region Properties
		public string ProjectPath
		{
			get { return (string) GetValue(ProjectPathProperty); }
			set { SetValue(ProjectPathProperty, value); }
		}

		public string RaceTrackImagePath
		{
			get { return (string) GetValue(RaceTrackImagePathProperty); }
			set { SetValue(RaceTrackImagePathProperty, value); }
		}

		public BitmapImage RaceTrackImage
		{
			get { return mn_raceTrackImage; }
			set
			{
				if (mn_raceTrackImage != value)
				{
					mn_raceTrackImage = value;
					OnPropertyChanged("RaceTrackImage");
				}
			}
		}
		private BitmapImage mn_raceTrackImage;
		#endregion


		#region Commands
		public ICommand SelectProjectLocationCommand
		{
			get
			{
				if (mn_selectProjectLocationCommand == null)
					mn_selectProjectLocationCommand = new RelayCommand(p => this.ProjectLocationDialog());
				return mn_selectProjectLocationCommand;
			}
		}
		private ICommand mn_selectProjectLocationCommand;

		public ICommand SelectTrackImageCommand
		{
			get
			{
				if (mn_selectTrackImageCommand == null)
					mn_selectTrackImageCommand = new RelayCommand(p => this.SelectTrackImageDialog());
				return mn_selectTrackImageCommand;
			}
		}
		private ICommand mn_selectTrackImageCommand;

		public ICommand CreateProjectCommand
		{
			get
			{
				if (mn_createProjectCommand == null)
					mn_createProjectCommand = new RelayCommand(p => AcceptSelectedProjectValues(), p => AreSelectedValueAcceptable(null));
				return mn_createProjectCommand;
			}
		}
		private ICommand mn_createProjectCommand;
		#endregion


		#region Constructors
		public NewProjectDialog()
		{
			InitializeComponent();
			this.DataContext = this;
		}

		static NewProjectDialog()
		{
			ProjectPathProperty = DependencyProperty.Register(
				"ProjectPath",
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
			NewProjectDialog dlg = (NewProjectDialog) d;

			BitmapImage image;
			string value = (string) e.NewValue;
			
			// If the image path doesn't event look somewhat valid, then
			// don't even try to set the image from it.
			if (!dlg.IsImagePathValidish(value))
			{
				image = null;
			}
			else
			{
				try
				{
					image = new BitmapImage();
					image.BeginInit();
					image.UriSource = new Uri(value, UriKind.RelativeOrAbsolute);
					image.EndInit();
				}
				catch
				{
					image = null;
				}
			}

			dlg.RaceTrackImage = image;
		}
		#endregion


		#region Methods
		private void AcceptSelectedProjectValues()
		{
			this.DialogResult = true;
		}

		private bool AreSelectedValueAcceptable(object parameter)
		{
			if (RaceTrackImage == null)
				return false;

			//if (!IsProjectNameValidish(this.ProjectName))
			//    return false;

			//if (!IsProjectDirectoryValidish(this.ProjectLocation))
			//    return false;

			return true;
		}

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
				//this.ProjectLocation = dialog.SelectedPath;
				break;
			}
		}

		private void SelectTrackImageDialog()
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Track Image (*.png)|*.png";
			dlg.Multiselect = false;
			dlg.Title = "Select Race Track Image";

			if (!string.IsNullOrWhiteSpace(this.RaceTrackImagePath) &&
				this.RaceTrackImagePath.EndsWith(".png", StringComparison.OrdinalIgnoreCase) &&
				File.Exists(this.RaceTrackImagePath))
			{
				dlg.FileName = this.RaceTrackImagePath;
			}

			bool keepGoing = dlg.ShowDialog(this) == true;
			if (!keepGoing)
				return;

			this.RaceTrackImagePath = dlg.FileName;
		}

		private bool IsImagePathValidish(string path)
		{
			if (string.IsNullOrWhiteSpace(path))
				return false;

			if (!path.EndsWith(".png"))
				return false;

			if (!System.IO.File.Exists(path))
				return false;

			return true;
		}

		private bool IsProjectNameValidish(string path)
		{
			if (string.IsNullOrWhiteSpace(path))
				return false;

			var invalidChars = IOPath.GetInvalidFileNameChars();
			if (path.Any(c => { return invalidChars.Contains(c); }))
				return false;

			return true;
		}

		private bool IsProjectDirectoryValidish(string path)
		{
			if (string.IsNullOrEmpty(path))
				return false;

			var invalidVars = IOPath.GetInvalidPathChars();
			if (path.Any(c => { return invalidVars.Contains(c); }))
				return false;

			return true;
		}

		private void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}
}
