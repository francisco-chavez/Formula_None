using System.Collections.Generic;

using Unv.RaceTrackEditor.Core.Models;


namespace Unv.RaceTrackEditor.Core.Zip.Models
{
	public class ObstacleDataModelZip
		: ObstacleDataModel
	{
		public ObstacleDataModelZip()
		{
			ObstacleLayers = new List<ObstacleLayerModelZip>();
		}
	}
}
