using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


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
