using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Unv.RaceEngineLib.Physics.Measurements;


namespace Unv.RaceEngineLib.Physics.Shapes
{
	public class Triangle
		: Shape
	{
		#region Attributes
		private readonly Vector2[] m_originalPositions;
		#endregion


		#region Properties
		public Vector2 PointA { get; private set; }
		public Vector2 PointB { get; private set; }
		public Vector2 PointC { get; private set; }

		public override Vector2 CenterOfMassShift
		{
			get { return m_centerOfMassShift; }
		}
		private Vector2 m_centerOfMassShift = Vector2.Zero;

		public override float Area
		{
			get { return m_area; }
		}
		private readonly float m_area;

		public override float QuickRadius
		{
			get { return m_quickRadius; }
		}
		private readonly float m_quickRadius;

		/// <summary>
		/// Returns (Izz over mass) at the point of origin.
		/// </summary>
		public override float InertiaOverMass
		{
			get
			{
				if (float.IsNaN(m_inertiaOverMass))
				{
					Vector2 a = m_originalPositions[0];
					Vector2 b = m_originalPositions[1];
					Vector2 c = m_originalPositions[2];

					m_inertiaOverMass = Triangle.MomentOfInertiaTriangleOverMass(b - a, c - a, true);
				}

				return m_inertiaOverMass;
			}
		}
		private float m_inertiaOverMass = float.NaN;
		#endregion


		#region Constructors
		public Triangle(Vector2 a, Vector2 b, Vector2 c)
		{
			m_originalPositions = new Vector2[] { a, b, c };
			m_centerOfMassShift = Vector2.Zero;

			// At first I used Heron's Formula to find the area of the triangle, but this 
			// used a lot of square roots (a total of four). Then I realized that I already 
			// have the code for finding the height of the triangle by using the triangle's 
			// extent over the normal unit axis of the base vector for the height. This 
			// cuts down the number of square root operations down to one. Using doubles to 
			// run the calculations of Heron's Formula as my base case, I tested this method 
			// and compared it to using floats to calculate Heron's Formula. This is XNA, 
			// we're supposed to use floats instead of doubles. Long story short, the error 
			// on this method was about an order of magnitude smaller than the Heron Formula 
			// with floats. To be honest, the error of Heron's Formula with floats was 
			// really small to begin with.

			// In this case, I'm going to normalize the normal vector into a unit vector 
			// myself. The reason I'm doing this is because we need to caluculate the length 
			// of a vector to turn it into a unit vector. This adds a square root operation,
			// which isn't needed because the already got that measurement when we grabed the 
			// length of the base.
			Vector2 baseVector	= a - b;						// Use points a & b to create the base of the triangle			
			float	baseLength	= baseVector.Length();			// Get the length of the base
			Vector2 unitVert	= baseVector.NormalVector(false) / baseLength;	// Get the perpendicular unit axis of the base
			float	height		= Range.FindExtent(unitVert, a, c).Length;	// a is on the base line, and
																			// c is the only missing point
			// one half base times height
			m_area = 0.5f * baseLength * height;


			// The center of mass of a triangle with uniform density is the median point 
			// of the triangle. This was the easiest way I was able to find to give me 
			// the position bases on the points making up the triangle's bounds. Since
			// all shapes have a center of mass at (0, 0), we need to set the center of
			// mass shift to the negative of the center of mass of the original points
			// that created the triangle.
			Vector2 centerOfMass = (a + b + c) / 3f;
			m_centerOfMassShift = -centerOfMass;

			PointA = a + CenterOfMassShift;
			PointB = b + CenterOfMassShift;
			PointC = c + CenterOfMassShift;


			// Find a radius from the center of mass that contains
			// all points in the triangle.
			Vector2 l = PointA;
			if (l.LengthSquared() < PointB.LengthSquared())
				l = PointB;
			if (l.LengthSquared() < PointC.LengthSquared())
				l = PointC;

			m_quickRadius = l.Length() + 1f;
		}
		#endregion


		#region Methods
		/// <summary>
		/// Given a shape with a uniform density, that is in the form of a triangle from overhead 
		/// (Z-Axix is up), and one point at the point of origin, this will given you the moment
		/// of inertia in the Z-Axis from the point of origin divided by the mass of the shape.
		/// All you need to provide are the other two points.
		/// </summary>
		/// <returns></returns>
		public static float MomentOfInertiaTriangleOverMass(Vector2 A, Vector2 B, bool fromCenterOfMass = false)
		{
			// b stands for base length, which happens to be Mag(A - (0, 0))
			float b = A.Length();

			// N is the unit vector perpendicular to base b
			Vector2 N = A.NormalVector(false) / b;

			// h is the height of the triangle, which happens to be (B dot N) - ((A dot N) or ((0, 0) dot N)
			// since both of those points are on the base, they both have the same dot product with N. I just
			// removed  the minus 0
			float h = Vector2.Dot(B, N);

			// U is the unit vector parallel to the base pointing to point A
			Vector2 U = A / b;

			// a is the base length of the far triangle that is created when we split
			// the main triangle along the height, B dot U gives us the length of the
			// near triangle's base
			float a = b - Vector2.Dot(B, U);

			// C is the center of the triangle (which also happens to be the triangle's center of
			// mass. This is found by averaging all three points. I didn't bother adding (0, 0).
			Vector2 C = (A + B) / 3;

			// The equation that I found for the moment of inertia for a triangle at the triangle's
			// center of mass is:
			// ((b^3)h - (b^2)ha + bh(a^2) + b(h^3)) / 36
			// I found this online, so I know it's true
			float iTriOverMass = (b * b * b * h - b * b * h * a + b * h * a * a + b * h * h * h) / 36;

			if (fromCenterOfMass)
				// return the moment from point C, not from point (0, 0)
				return iTriOverMass;

			// We need to transfer the inertia from C to (0, 0), which is distance squared * mass.
			// The inertia of the triangle is alreay missing the mass, so we can insert that after
			// combining the two
			float moment = C.LengthSquared() + iTriOverMass;

			// Since we don't have the actual mass, we'll return what we already have.
			return moment;
		}
		#endregion
	}
}
