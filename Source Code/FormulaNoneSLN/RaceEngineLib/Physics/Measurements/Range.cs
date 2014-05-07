using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;


namespace Unv.RaceEngineLib.Physics.Measurements
{
	public struct Range
	{
		public float Min;
		public float Max;
		public float Length { get { return Max - Min; } }

		/// <summary>
		/// Given an arbitrary axis and a set of points in space, this method 
		/// will return the range that these points enxtend over the given axis.
		/// </summary>
		/// <param name="axisUnit">A unit vector that represents the axis</param>
		/// <param name="points">A set of points in 2D space</param>
		public static Range FindExtent(Vector2 axisUnit, params Vector2[] points)
		{
			Range r = new Range();
			r.Max = float.MinValue;
			r.Min = float.MaxValue;

			for (int i = 0; i < points.Length; i++)
			{
				float projection = Vector2.Dot(axisUnit, points[i]);

				r.Min = Math.Min(projection, r.Min);
				r.Max = Math.Max(projection, r.Max);
			}

			return r;
		}
	}
}
