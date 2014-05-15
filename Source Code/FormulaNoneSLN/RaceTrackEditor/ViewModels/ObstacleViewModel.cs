using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Unv.RaceTrackEditor.ViewModels
{
	public class ObstacleViewModel
		: ViewModelBase
	{
		#region Properties
		public double X
		{
			get { return mn_x; }
			set
			{
				if (mn_x != value)
				{
					mn_x = value;
					OnPropertyChanged("X");
				}
			}
		}
		private double mn_x;

		public double Y
		{
			get { return mn_y; }
			set
			{
				if (mn_y != value)
				{
					mn_y = value;
					OnPropertyChanged("Y");
				}
			}
		}
		private double mn_y;
		#endregion


		#region Constructors
		public ObstacleViewModel()
		{
			DisplayTitle = null;
			X = 0.0;
			Y = 0.0;
		}
		#endregion
	}
}
