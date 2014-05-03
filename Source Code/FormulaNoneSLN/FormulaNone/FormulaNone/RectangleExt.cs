using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;


namespace Unv.FormulaNone
{
	/// <summary>
	/// This class contains Extentions methods for the Rectangle class.
	/// </summary>
	public static class RectangleExt
	{
		/// <summary>
		/// Returns the XY-Position of the Rectangle as a Vector2.
		/// </summary>
		public static Vector2 Position(this Rectangle rect)
		{
			return new Vector2(rect.X, rect.Y);
		}

		/// <summary>
		/// Returns the size of the Rectangle as a Vector2.
		/// </summary>
		public static Vector2 Size(this Rectangle rect)
		{
			return new Vector2(rect.Width, rect.Height);
		}
	}
}
