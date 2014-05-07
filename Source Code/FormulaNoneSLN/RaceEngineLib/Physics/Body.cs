using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;


namespace Unv.RaceEngineLib.Physics
{
	public class Body
	{
		public MassData MassData		{ get; set; }
		public Vector2	Velocity		{ get; set; }
		public Vector2	Force			{ get; set; }
		public Vector2	Position		{ get; set; }
		public Vector2	Acceleration	{ get; set; }

		public float	Orientation		{ get; set; }
		public float	AngularVelocity { get; set; }
		public float	Torque			{ get; set; }

		public float	QuickSize		{ get; private set; }
	}
}
