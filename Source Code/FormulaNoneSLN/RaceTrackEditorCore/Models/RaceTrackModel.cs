using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;


namespace Unv.RaceTrackEditor.Core.Models
{
	public class RaceTrackModel
	{
		public virtual BitmapImage		RaceTrackImage		{ get; set; }
		public virtual int				RaceTrackWidth		{ get; set; }
		public virtual int				RaceTrackHeight		{ get; set; }
		public virtual ObstaclesModel	Obstacles			{ get; set; }
	}
}
