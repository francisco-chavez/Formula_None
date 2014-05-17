using System;
using System.Collections.Generic;


namespace Unv.RaceTrackEditor.Core.Models
{
	public class ObstacleDataModel
	{
		#region Properties
		public virtual List<ObstacleLayerModel> ObstacleLayers { get; protected set; }
		#endregion


		#region Constructors
		public ObstacleDataModel()
		{
			ObstacleLayers = new List<ObstacleLayerModel>();
		}
		#endregion


		#region Methods
		public virtual void AddLayer(ObstacleLayerModel layerModel)
		{
			if (layerModel == null)
				throw new ArgumentNullException();
			if (ObstacleLayers.Contains(layerModel))
				throw new ArgumentException("The Obstacle Data Model already contains the given layer.");

			ObstacleLayers.Add(layerModel);
		}
		#endregion
	}
}
