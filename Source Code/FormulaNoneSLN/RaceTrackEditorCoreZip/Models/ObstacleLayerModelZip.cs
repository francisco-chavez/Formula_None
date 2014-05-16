using System.Collections.Generic;

using Unv.RaceTrackEditor.Core.Models;


namespace Unv.RaceTrackEditor.Core.Zip.Models
{
	public class ObstacleLayerModelZip
		: ObstacleLayerModel
	{
		public ObstacleLayerModelZip()
		{
			Obstacles = new List<ObstacleModelZip>();
		}
	}
}
