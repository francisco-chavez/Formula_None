using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using Unv.RaceTrackEditor.ViewModels;


namespace Unv.RaceTrackEditor.Dialogs
{
	/// <summary>
	/// Interaction logic for ObstacleExportDialog.xaml
	/// </summary>
	public partial class ObstacleExportDialog 
		: Window
	{
		#region Attributes
		public static readonly DependencyProperty ObstacleLayersProperty;
		public static readonly DependencyProperty ExportPathProperty;
		#endregion


		#region Commands
		public ICommand ExportCommand
		{
			get
			{
				if (mn_exportCommand == null)
					mn_exportCommand = new RelayCommand(p => ExportItems(), p => CanExportItems(null));
				return mn_exportCommand;
			}
		}
		private RelayCommand mn_exportCommand;
		#endregion


		#region Properties
		public ObservableCollection<ObstacleLayerViewModel> ObstacleLayers
		{
			get { return (ObservableCollection<ObstacleLayerViewModel>) GetValue(ObstacleLayersProperty); }
			set { SetValue(ObstacleLayersProperty, value); }
		}

		public string ExportPath
		{
			get { return (string) GetValue(ExportPathProperty); }
			set { SetValue(ExportPathProperty, value); }
		}

		public ObstacleLayerViewModel[] SelectedObstacleLayers	{ get; private set; }

		public string[]					FileTypes				{ get; set; }
		#endregion


		#region Constructors
		static ObstacleExportDialog()
		{
			ObstacleLayersProperty = DependencyProperty.Register(
				"ObstacleLayers",
				typeof(ObservableCollection<ObstacleLayerViewModel>),
				typeof(ObstacleExportDialog),
				new PropertyMetadata(null));

			ExportPathProperty = DependencyProperty.Register(
				"ExportPath",
				typeof(string),
				typeof(ObstacleExportDialog),
				new PropertyMetadata(null));
		}

		public ObstacleExportDialog()
		{
			InitializeComponent();
			this.DataContext = this;

			this.Loaded += ObstacleExportDialog_Loaded;
		}
		#endregion


		#region Event Handlers
		private void Browse_Click(object sender, RoutedEventArgs e)
		{
			StringBuilder s = new StringBuilder();

			for (int i = 0; i < FileTypes.Length; i++)
			{
				s.Append(FileTypes[i]);
				s.Append(i == FileTypes.Length - 1 ? string.Empty : "|");
			}

			var dlg = new SaveFileDialog();
			dlg.Filter = s.ToString();
			dlg.Title = "Selected Obstacle Export Path";

			if (!string.IsNullOrWhiteSpace(ExportPath))
				dlg.FileName = ExportPath;

			bool keepGoing = dlg.ShowDialog(this) == true;

			if (!keepGoing)
				return;

			this.ExportPath = dlg.FileName;
		}

		void ObstacleExportDialog_Loaded(object sender, RoutedEventArgs e)
		{
			if (FileTypes == null)
				throw new NullReferenceException("There are no file types to choose from for exporting the obstacle data.");
			if (FileTypes.Length == 0)
				throw new ArgumentException("There are no file types to choose from for exporting the obstacle data.");
		}
		#endregion


		#region Methods
		private void ExportItems()
		{
		}

		private bool CanExportItems(object parameters)
		{
			return false;
		}
		#endregion
	}
}
