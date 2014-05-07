using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Unv.RaceEngineLib.Physics.Collidables;


namespace Unv.RaceEngineLib.Physics.Measurements
{
	public struct Manifold
	{
		public ShapeBase	ShapeA;
		public ShapeBase	ShapeB;
		public float		Penetration;
		public Vector2		Normal;
	}
}
