using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

using Unv.RaceTrackEditor.Core.Models;


namespace Unv.RaceTrackEditor.ViewModels
{
	public class ObstacleDataViewModel
		: SingleModelViewModel<ObstacleDataModel>
	{
		#region Properties
		public ObservableCollection<ObstacleLayerViewModel> ObstacleLayers
		{
			get { return mn_obstacleLayers; }
			private set
			{
				if (mn_obstacleLayers != value)
				{
					mn_obstacleLayers = value;
					OnPropertyChanged("ObstacleLayers");
				}
			}
		}
		private ObservableCollection<ObstacleLayerViewModel> mn_obstacleLayers;

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

		public Thickness ImageMargin
		{
			get { return mn_imageMargin; }
			set
			{
				if (mn_imageMargin != value)
				{
					mn_imageMargin = value;
					OnPropertyChanged("ImageMargin");
				}
			}
		}
		private Thickness mn_imageMargin;

		public ObstacleLayerViewModel TestLayer
		{
			get { return m_TestLayer; }
			set
			{
				if (m_TestLayer != value)
				{
					m_TestLayer = value;
					OnPropertyChanged("TestLayer");
				}
			}
		}
		private ObstacleLayerViewModel m_TestLayer;
		
		#endregion


		#region Constructors
		public ObstacleDataViewModel()
			: base("Obstacle Editor")
		{
			ObstacleLayers = new ObservableCollection<ObstacleLayerViewModel>();

			ImageMargin = new Thickness(50);
		}
		#endregion


		#region Methods
		public override void ClearOutModelData()
		{
			foreach (var layerVM in ObstacleLayers)
				layerVM.ClearOutModelData();

			ObstacleLayers.Clear();

			base.ClearOutModelData();
		}

		public override void LoadModelData()
		{
			foreach (var layerModel in Model.ObstacleLayers)
			{
				var layerVM = new ObstacleLayerViewModel();
				layerVM.Model = layerModel;

				ObstacleLayers.Add(layerVM);
			}

			if (TestLayer == null)
				TestLayer = new ObstacleLayerViewModel();

			TestLayer.Model = Model.ObstacleLayers.First();

			base.LoadModelData();
		}
		#endregion
	}
}
