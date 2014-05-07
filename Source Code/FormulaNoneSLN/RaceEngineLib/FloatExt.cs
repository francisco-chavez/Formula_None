using System;

using Microsoft.Xna.Framework;


namespace Unv.RaceEngineLib
{
	public static class FloatExt
	{
		/// <summary>
		/// Treats the float as a radian value and converts it into a unit vector
		/// in the XY-Plain with a theta value (off the positive X-Axis) equal to 
		/// the radian value.
		/// </summary>
		public static Vector2 ToUnitVector2(this float rads)
		{
			return new Vector2((float) Math.Cos(rads), (float) Math.Sin(rads));
		}
	}
}
