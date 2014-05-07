using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;


namespace Unv.RaceEngineLib.Physics
{
	public abstract class Shape
	{
		public abstract Vector2 CenterOfMass	{ get; }
		public abstract float	Area			{ get; }
	}
}
