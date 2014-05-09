using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Unv.RaceEngineLib.Physics;
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
		}

		public void ReplaceObstacles(ObstacleMap obstacleMap)
		{
		}

		public void ClearObstacles()
		{
		}
	}
}
