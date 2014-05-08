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
		private readonly float		m_quickRadius;


		public override Vector2 CenterOfMass	{ get { return m_centerOfMass; } }
		public override float	Area			{ get { return m_area; } }
		public override float	QuickRadius		{ get { return m_quickRadius; } }


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


			// Break the shape into trianlge. This will give us a list of components 
			// with their own area and center of mass. We sum up all their areas to 
			// get the area of the shape. We then sum up the weighted center of mass's 
			// of the triangles to find the center of mass of the overall shape.
			Vector2 a = m_borderPoints[0];
			Vector2 b;
			Vector2 c;

			Triangle[] trianles = new Triangle[m_borderPoints.Length - 2];

			float totalArea = 0f;
			for (int i = 2; i < m_borderPoints.Length; i++)
			{
				b = m_borderPoints[i - 1];
				c = m_borderPoints[i];

				trianles[i - 2] = new Triangle(a, b, c);
				totalArea += trianles[i - 2].Area;
			}

			Vector2 weightedCenterSum = Vector2.Zero;
			foreach (var component in trianles)
				weightedCenterSum += component.Area * component.CenterOfMass;
			
			m_centerOfMass = weightedCenterSum / totalArea;
			m_area = totalArea;

			// Find a radius from the center of mass to use
			// that includes all the points of the shape.
			Vector2 l = Vector2.Zero;

			foreach (var point in m_borderPoints)
			{
				Vector2 diff = point - CenterOfMass;
				if (diff.LengthSquared() > l.LengthSquared())
					l = diff;
			}

			m_quickRadius = l.Length() + 1f;
		}
	}
}
