using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Unv.RaceEngineLib.Physics;
using Unv.RaceEngineLib.Physics.Shapes;
using Unv.RaceEngineLib.Storage;


namespace Unv.RaceEngineLib
{
	public class RaceEngine
	{
		private PhysicsEngine	m_physicsEngine;
		private List<Tire>		m_obstacles;

		public RaceEngine()
		{
			m_physicsEngine = new PhysicsEngine();
			m_obstacles		= new List<Tire>(50);
		}

		public void AddObstacles(ObstacleMap obstacleMap)
		{
			if (obstacleMap == null)
				throw new ArgumentNullException();

			foreach (var obstacle in obstacleMap.Obstacles)
			{
				var shape = new Circular(obstacle.Radius);

				// Bouncy balls are made from rubber.
				Body body = m_physicsEngine.AddImmobileBody(shape, obstacle.Position, "BouncyBall");
				body.Position = obstacle.Position;

				Tire tire = new Tire(body);
				m_obstacles.Add(tire);
			}
		}

		public void ReplaceObstacles(ObstacleMap obstacleMap)
		{
			ClearObstacles();

			AddObstacles(obstacleMap);
		}

		public void ClearObstacles()
		{
			m_physicsEngine.ClearImmobileBodies();
			m_obstacles.Clear();
		}
	}
}
