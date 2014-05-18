using System;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace Unv.RaceTrackEditor.Core.Models
{
	public class ObstacleLayerModel
		: IModel
	{
		#region Properties
		[XmlAttribute]
		public virtual string				LayerName { get; set; }

		[XmlAttribute]
		public virtual bool					IsVisable { get; set; }
		public virtual List<ObstacleModel>	Obstacles { get; protected set; }
		#endregion


		#region Constructors
		public ObstacleLayerModel()
		{
			IsVisable = true;
			Obstacles = new List<ObstacleModel>();
		}
		#endregion


		#region Methods
		public virtual void AddObstacle(ObstacleModel obstacleModel)
		{
			if (obstacleModel == null)
				throw new ArgumentNullException();
			if (Obstacles.Contains(obstacleModel))
				throw new ArgumentException("Obstacle Layer already contains added obstacle.");

			Obstacles.Add(obstacleModel);
		}
		#endregion
	}
}
