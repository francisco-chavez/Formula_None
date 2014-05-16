using System.Collections.Generic;


namespace Unv.RaceTrackEditor.Core.Models
{
	public class ObstacleLayerModel
	{
		public virtual string						LayerName { get; set; }
		public virtual IEnumerable<ObstacleModel>	Obstacles { get; protected set; }
		public virtual bool							IsVisable { get; set; }


		public ObstacleLayerModel()
		{
			Obstacles = new List<ObstacleModel>();
		}
	}
}
