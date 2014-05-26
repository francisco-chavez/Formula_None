using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

using Microsoft.Xna.Framework;

using Unv.RaceEngineLib.Storage;
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

		public override void RebuildModel()
		{
			foreach (var layerModel in Model.ObstacleLayers)
				layerModel.Obstacles.Clear();
			Model.ObstacleLayers.Clear();

			foreach (var layerViewModel in this.ObstacleLayers)
			{
				layerViewModel.RebuildModel();
				Model.ObstacleLayers.Add(layerViewModel.Model);
			}
		}

		public ObstacleMap BuildMap(ObstacleLayerViewModel[] selectedLayers)
		{
			ObstacleMap result = new ObstacleMap();
			
			if (selectedLayers == null)
				return result;

			Vector2 margin = new Vector2(50f, 50f);

			foreach (var layer in selectedLayers)
				foreach (var obs in layer.Obstacles)
				{
					Vector2 positionOnCanvas = new Vector2((float) obs.X, (float) obs.Y);

					result.Obstacles.Add(
						new Obstacle() 
						{ 
							Radius = 16f, 
							Position = positionOnCanvas - margin 
						});
				}

			return result;
		}
		#endregion
	}
}
