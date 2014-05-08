﻿using System;
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

		public override float QuickRadius
		{
			get { return m_quickRadius; }
		}
		private readonly float m_quickRadius;


		public Triangle(Vector2 a, Vector2 b, Vector2 c)
		{
			// At first I used Heron's Formula to find the area of the triangle, but this 
			// used a lot of square roots (a total of four). Then I realized that I already 
			// have the code for finding the height of the triangle by using the triangle's 
			// extent over the normal unit axis of the base vector for the height. This 
			// cuts down the number of square root operation down to two. Using doubles to 
			// run the calculations of Heron's Formula as my base case, I tested this method 
			// and compared it to using floats to calculate Heron's Formula. This is XNA, 
			// we're supposed to use floats instead of doubles. Long story short, the error 
			// on this method was about an order of magnitude smaller than the Heron Formula 
			// with floats. To be honest, the error of Heron's Formula with floats was 
			// really small to begin with.

			Vector2 baseVector	= a - b;						// Use points a & b to create the base of the triangle			
			float	baseLength	= baseVector.Length();			// Get the length of the base
			Vector2 unitVert	= baseVector.NormalVector();	// Get the perpendicular unit axis of the base
			float	height		= Range.FindExtent(unitVert, a, c).Length;	// a is on the base line, and
																			// c is the only missing point
			// one half base times height
			m_area = 0.5f * baseLength * height;


			// The center of mass of a triangle with uniform density is the median point 
			// of the triangle. This was the easiest way I was able to find to give me 
			// the position bases on the points making up the triangle's bounds.
			m_centerOfMass = (a + b + c) / 3f;


			// Find a radius from the center of mass that contains
			// all points in the triangle.
			Vector2 u = a - b;
			Vector2 v = b - c;
			Vector2 w = c - a;

			Vector2 l = u;
			if (l.LengthSquared() < v.LengthSquared())
				l = v;
			if (l.LengthSquared() < w.LengthSquared())
				l = w;

			m_quickRadius = l.Length() + 1f;
		}
	}
}
