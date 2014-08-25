using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;


namespace Unv.RaceEngineLib.Physics.Measurements
{
	public struct ForceOnCar
	{
		public Vector2	AccelerationMetric	{ get; set; }
		public float	Torque				{ get; set; }
	}
}
