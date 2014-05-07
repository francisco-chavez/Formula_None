﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Unv.RaceEngineLib.Physics;


namespace Unv.RaceEngineLib.Physics.Measurements
{
	public struct Manifold
	{
		public Shape	ShapeA;
		public Shape	ShapeB;
		public float	Penetration;
		public Vector2	Normal;
	}
}
