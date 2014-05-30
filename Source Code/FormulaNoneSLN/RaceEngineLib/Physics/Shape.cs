using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;


namespace Unv.RaceEngineLib.Physics
{
	public abstract class Shape
	{
		/// <summary>
		/// Returns the distance vector the Shape was tranlated over to center its
		/// mass to the point of origin.
		/// </summary>
		public abstract Vector2 CenterOfMassShift { get; }

		/// <summary>
		/// Returns the Center of mass of the shape.
		/// </summary>
		public Vector2 CenterOfMass { get { return Vector2.Zero; } }

		/// <summary>
		/// Returns the area of the shape.
		/// </summary>
		public abstract float	Area			{ get; }

		/// <summary>
		/// Returns a radius from the CenterOfMass that contains
		/// all points making up the shape. This is meant to be
		/// used for a quick collision check between two shapes.
		/// </summary>
		public abstract float	QuickRadius		{ get; }
	}
}
