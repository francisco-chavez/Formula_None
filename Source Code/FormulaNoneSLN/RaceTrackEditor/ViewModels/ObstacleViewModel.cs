using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Unv.RaceTrackEditor.Core.Models;


namespace Unv.RaceTrackEditor.ViewModels
{
	public class ObstacleViewModel
		: SingleModelViewModel<ObstacleModel>
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


		#region Methods
		public override void ClearOutModelData()
		{
			DisplayTitle = null;
			// The X and Y properties are structs, so clearing 
			// them out won't really free anything in memory.
			// -FCT
		}

		public override void LoadModelData()
		{
			DisplayTitle	= Model.Name;
			X				= Model.X;
			Y				= Model.Y;

			base.LoadModelData();
		}
		#endregion
	}
}
