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


			// Using my quick work around, I count a total of
			// 2 square root operations.
			Vector2 baseVector = a - b;
			float baseLength = baseVector.Length();
			Vector2 verticalDirection = baseVector.NormalVector();
			Range heightRange = Range.FindExtent(verticalDirection, a, c);
			float height = heightRange.Length;

			m_area = 0.5f * baseLength * height;
		}
	}
}
