using System;

using Microsoft.Xna.Framework;


namespace Unv.RaceEngineLib
{
	public static class Vector2Ext
	{
		/// <summary>
		/// Returns a copy of the vector that is rotated by the given number
		/// of radians about the origin.
		/// </summary>
		/// <param name="rads">The amount to rotate the vector by.</param>
		public static Vector2 Rotate(this Vector2 vector, float rads)
		{
			float cos = (float) Math.Cos(rads);
			float sin = (float) Math.Sin(rads);

			float xPrime = vector.X * cos + vector.Y * -sin;
			float yPrime = vector.X * sin + vector.Y * cos;

			return new Vector2(xPrime, yPrime);
		}

		/// <summary>
		/// Returns a copy of the vector that is rotated by the given number
		/// of radian about the origin.
		/// </summary>
		/// <param name="rads">The amount to rotate the vector by.</param>
		/// <param name="origin">The origin about which to rotate the vector.</param>
		public static Vector2 Rotate(this Vector2 vector, float rads, Vector2 origin)
		{
			Vector2 vectorPrime = vector - origin;
			Vector2 rotatedVector = vectorPrime.Rotate(rads);
			return rotatedVector + origin;
		}

		/// <summary>
		/// Gives us the angle of the Vector with respect to the positive X-Axis.
		/// </summary>
		public static float GetTheta(this Vector2 vector)
		{
			return (float) Math.Atan2(vector.Y, vector.X);
		}
	}
}
