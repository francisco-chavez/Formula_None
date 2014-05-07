using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Unv.RaceEngineLib.Physics.Collidables
{
	public class ConvexPolygon
		: ShapeBase
	{
		private Vector2		m_centerOffMass;
		private Vector2[]	m_borderPoints;


		public ConvexPolygon(IEnumerable<Vector2> borderPoints, Vector2 centerOfMass)
		{
			if (borderPoints == null)
				throw new ArgumentNullException("A set of 3 or more points are required to create a ConvexPolygon");

			if (borderPoints.Count() < 3)
				throw new ArgumentException("A set of 3 or more points are required to create a ConvexPolygon");

			// This creates an IEnumerable of Vectory2 with the same points as
			// the borderPoints value that was given, copies those points into
			// an array, and sets that array to m_borderPoints. I'm copying 
			// the points because I don't know what will happen if the borderPoints
			// is a Vector2[] array and I call ToArray(). Will it give me a new
			// array or will it give me itself (which the user has direct access
			// to)? After all, I am putting this in a private attribute to stop
			// the user from accessing it.
			// -FCT
			m_borderPoints = borderPoints.Select(b => { return b; }).ToArray();
			m_centerOffMass = centerOfMass;
		}


		public float Direction
		{
			get { return mn_direction; }
			set { mn_direction = MathHelper.WrapAngle(value); }
		}
		private float mn_direction = 0f;
	}
}
