using System;
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


		public override void AddObstacleLayer(ObstacleLayerModel layerModel)
		{
			if (!(layerModel is ObstacleLayerModelZip))
				throw new Exception();

			base.AddObstacleLayer(layerModel);
		}
	}
}
