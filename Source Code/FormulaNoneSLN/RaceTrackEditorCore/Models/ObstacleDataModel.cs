using System.Collections.Generic;


namespace Unv.RaceTrackEditor.Core.Models
{
	public class ObstacleDataModel
	{
		public virtual List<ObstacleLayerModel> ObstacleLayers { get; protected set; }


		public ObstacleDataModel() { }
	}
}
