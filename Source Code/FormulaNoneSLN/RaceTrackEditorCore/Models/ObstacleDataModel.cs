using System;
using System.Collections.Generic;


namespace Unv.RaceTrackEditor.Core.Models
{
	public class ObstacleDataModel
	{
		#region Properties
		public virtual IEnumerable<ObstacleLayerModel> ObstacleLayers
		{
			get { return m_obstacleLayers; }
			protected set 
			{
				if (value == null)
					m_obstacleLayers = null;
				else
					m_obstacleLayers = new List<ObstacleLayerModel>(value);
			}
		}
		private List<ObstacleLayerModel> m_obstacleLayers;
		#endregion


		#region Constructors
		public ObstacleDataModel()
		{
			ObstacleLayers = new List<ObstacleLayerModel>();
		}
		#endregion


		#region Methods
		public virtual void AddObstacleLayer(ObstacleLayerModel layerModel)
		{
			if (layerModel == null)
				throw new ArgumentNullException();
			if (m_obstacleLayers.Contains(layerModel))
				throw new ArgumentException("The Obstacle Data Model already contains the given layer.");

			m_obstacleLayers.Add(layerModel);
		}
		#endregion
	}
}
