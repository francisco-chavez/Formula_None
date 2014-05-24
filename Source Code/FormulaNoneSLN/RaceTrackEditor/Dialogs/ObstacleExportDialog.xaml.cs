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
		private static readonly DependencyPropertyKey SelectedObstacleLayersPropertyKey;
		public static readonly DependencyProperty ExportPathProperty;
		#endregion


		#region Properties
		public ObservableCollection<ObstacleLayerViewModel> ObstacleLayers
		{
			get { return (ObservableCollection<ObstacleLayerViewModel>) GetValue(ObstacleLayersProperty); }
			set { SetValue(ObstacleLayersProperty, value); }
		}

		public ObservableCollection<ObstacleLayerViewModel> SelectedObstacleLayers
		{
			get { return (ObservableCollection<ObstacleLayerViewModel>) GetValue(SelectedObstacleLayersPropertyKey.DependencyProperty); }
			private set { SetValue(SelectedObstacleLayersPropertyKey, value); }
		}

		public string ExportPath
		{
			get { return (string) GetValue(ExportPathProperty); }
			set { SetValue(ExportPathProperty, value); }
		}
		#endregion


		#region Constructors
		static ObstacleExportDialog()
		{
			ObstacleLayersProperty = DependencyProperty.Register(
				"ObstacleLayers",
				typeof(ObservableCollection<ObstacleLayerViewModel>),
				typeof(ObstacleExportDialog),
				new PropertyMetadata(null));

			SelectedObstacleLayersPropertyKey = DependencyProperty.RegisterReadOnly(
				"SelectedObstacleLayers",
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

			this.SelectedObstacleLayers = new ObservableCollection<ObstacleLayerViewModel>();
		}
		#endregion


		#region Event Handlers
		private void Button_Click(object sender, RoutedEventArgs e)
		{

		}
		#endregion
	}
}
