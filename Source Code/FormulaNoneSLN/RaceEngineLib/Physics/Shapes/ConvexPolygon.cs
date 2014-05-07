using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Unv.RaceEngineLib.Physics.Measurements;


namespace Unv.RaceEngineLib.Physics.Shapes
{
	public class ConvexPolygon
		: Shape
	{
		private readonly Vector2[]	m_borderPoints;
		private readonly Vector2	m_centerOfMass;
		private readonly float		m_area;


		public override Vector2 CenterOfMass { get { return m_centerOfMass; } }
		public override float	Area { get { return m_area; } }


		public ConvexPolygon(IEnumerable<Vector2> borderPoints)
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
			m_borderPoints = borderPoints.Select(point => { return point; }).ToArray();

			Vector2 a = m_borderPoints[0];
			Vector2 b;
			Vector2 c;

			for (int i = 2; i < m_borderPoints.Length; i++)
			{
				b = m_borderPoints[i - 1];
				c = m_borderPoints[i];

				Vector2 center;
				float area;

				// Using Heron's Formula to find triangle area.
				float l1 = (b - a).Length();
				float l2 = (c - b).Length();
				float l3 = (a - c).Length();

				float s = 0.5f * (l1 + l2 + l3);
				area = (float) Math.Sqrt(s * (s - l1) * (s - l2) * (s - l3));
			}
		}
	}
}
