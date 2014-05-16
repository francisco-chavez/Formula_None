using System.Collections.Generic;


namespace Unv.RaceTrackEditor.Core.Models
{
	public class ObstacleDataModel
	{
		public virtual IEnumerable<ObstacleLayerModel> ObstacleLayers { get; protected set; }


		public ObstacleDataModel()
		{
			ObstacleLayers = new List<ObstacleLayerModel>();
		}
	}
}
