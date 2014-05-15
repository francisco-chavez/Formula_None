using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


namespace Unv.RaceTrackEditor.ViewModels
{
	public class ObstacleLayerViewModel
		: ViewModelBase
	{
		#region Properties
		public bool IsVisable
		{
			get { return mn_isVisable; }
			set
			{
				if (mn_isVisable != value)
				{
					mn_isVisable = value;
					OnPropertyChanged("IsVisable");
				}
			}
		}
		private bool mn_isVisable;

		public ObservableCollection<ObstacleViewModel> Obstacles
		{
			get { return mn_obstacles; }
			private set
			{
				if (mn_obstacles != value)
				{
					mn_obstacles = value;
					OnPropertyChanged("Obstacles");
				}
			}
		}
		private ObservableCollection<ObstacleViewModel> mn_obstacles;
		#endregion


		#region Constructors
		public ObstacleLayerViewModel()
		{
			DisplayTitle = string.Empty;
			IsVisable = true;
			Obstacles = new ObservableCollection<ObstacleViewModel>();
		}
		#endregion
	}
}
