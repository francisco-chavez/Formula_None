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
		#region Attributes
		public readonly Vector2[]	BorderPoints;

		private readonly float		m_area;
		private readonly float		m_quickRadius;
		private readonly Vector2	m_centerOfMassShift;
		#endregion


		#region Attributes
		public override Vector2 CenterOfMassShift { get { return m_centerOfMassShift; } }
		public override float	Area			{ get { return m_area; } }
		public override float	QuickRadius		{ get { return m_quickRadius; } }

		/// <summary>
		/// Returns (Izz over mass) at the point of origin.
		/// </summary>
		public override float InertiaOverMass
		{
			get
			{
				if (!float.IsNaN(m_inertiaOverMass))
				{
					m_inertiaOverMass = 0f;

					for (int i = 0; i < BorderPoints.Length; i++)
					{
						Vector2 A = BorderPoints[i];
						Vector2 B = BorderPoints[(i + 1) % BorderPoints.Length];

						m_inertiaOverMass += Triangle.MomentOfInertiaTriangleOverMass(A, B); 
					}
				}

				return m_inertiaOverMass;
			}
		}
		private float m_inertiaOverMass = float.NaN;
		#endregion


		#region Constructors
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
			Vector2[] pointPositions = borderPoints.Select(point => { return point; }).ToArray();


			// Break the shape into trianlge. This will give us a list of components 
			// with their own area and center of mass. We sum up all their areas to 
			// get the area of the shape. We then sum up the weighted center of mass's 
			// of the triangles to find the center of mass of the overall shape.
			Vector2 a = pointPositions[0];
			Vector2 b;
			Vector2 c;

			Triangle[] trianles = new Triangle[pointPositions.Length - 2];

			float totalArea = 0f;
			for (int i = 2; i < pointPositions.Length; i++)
			{
				b = pointPositions[i - 1];
				c = pointPositions[i];

				trianles[i - 2] = new Triangle(a, b, c);
				totalArea += trianles[i - 2].Area;
			}

			Vector2 weightedCenterSum = Vector2.Zero;
			foreach (var component in trianles)
				weightedCenterSum += component.Area * component.CenterOfMassShift;
			
			m_centerOfMassShift = weightedCenterSum / totalArea;
			m_area = totalArea;

			// Shift the border point positions so that the shape's center of
			// mass is at the point of origin.
			for (int i = 0; i < pointPositions.Length; i++)
				pointPositions[i] = pointPositions[i] + m_centerOfMassShift;
			BorderPoints = pointPositions;

			// Find a radius from the center of mass to use that 
			// includes all the points making up of the shape.
			Vector2 radius = Vector2.Zero;

			foreach (var point in BorderPoints)
			{
				if (point.LengthSquared() > radius.LengthSquared())
					radius = point;
			}

			m_quickRadius = radius.Length() + 1f;
		}
		#endregion
	}
}
