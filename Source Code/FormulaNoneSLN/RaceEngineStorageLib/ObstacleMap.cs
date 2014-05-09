using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;


namespace Unv.RaceEngineLib.Storage
{
	public class ObstacleMap
	{
		public List<Obstacle> Obstacles { get; private set; }

		public ObstacleMap()
		{
			Obstacles = new List<Obstacle>();
		}
	}
}
