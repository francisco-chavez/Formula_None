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
		public override Vector2 CenterOfMass
		{
			get { throw new NotImplementedException(); }
		}
		private readonly Vector2 m_centerOfMass;

		public override float Area
		{
			get { return m_area; }
		}
		private readonly float m_area;


		public Triangle(Vector2 a, Vector2 b, Vector2 c)
		{
			//// Using Heron's Formula to find the triangle's area.
			//// I count a total of 4 square root operations.
			//float l1 = (b - a).Length();
			//float l2 = (c - b).Length();
			//float l3 = (a - c).Length();

			//float s = 0.5f * (l1 + l2 + l3);
			//m_area = (float) Math.Sqrt(s * (s - l1) * (s - l2) * (s - l3));


			// Using my quick work around, I count a total of  2 square root operations.
			// I also did a bit of testing with some streched out triangles, and it turns
			// out that this is closer to the Heron's Formula using doubles than the 
			// Heron's Formula using floats. In other words, if we calculate the area
			// with Heron's Formula and Doubles, and use that as our goal. This is is
			// closer to the goal than the Heron's Formula with Floats.
			// -FCT
			
			// Use points a & b to create the base of the triangle
			Vector2 baseVector		= a - b;
			
			// Get the length of the base
			float	baseLength		= baseVector.Length();
			
			// Create a unit axis that is perpendicular to the  base.
			// This axis is parallel to the line representing the height
			// of the triangle.
			Vector2 verticalDirection = baseVector.NormalVector();

			// Find the extent of the trianle on the height axis
			Range	heightRange		= Range.FindExtent(verticalDirection, a, c);

			// From the extent, get the height of the triangle
			float	height			= heightRange.Length;

			// one half base times height
			m_area = 0.5f * baseLength * height;


			// The center of mass of a triangle with uniform density
			// is the median point of the triangle. This was the easiest
			// way I was able to find to give me the height base on the
			// points making up the triangle's bounds.
			m_centerOfMass = (a + b + c) / 3f;
		}
	}
}
