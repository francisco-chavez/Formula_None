using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;


namespace Unv.RaceTrackEditor.ViewModels
{
	public class ObstacleDataViewModel
		: ViewModelBase
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
		#endregion


		#region Constructors
		public ObstacleDataViewModel()
			: base("Obstacle Editor")
		{
			ObstacleLayers = new ObservableCollection<ObstacleLayerViewModel>();
		}
		#endregion


		#region Methods
		public void AddLayer(ObstacleLayerViewModel layer)
		{
			ObstacleLayers.Add(layer);
		}
		#endregion
	}
}
