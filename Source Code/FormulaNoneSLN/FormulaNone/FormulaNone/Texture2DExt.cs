using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Unv.FormulaNone
{
	public static class Texture2DExt
	{
		/// <summary>
		/// Returns the size of the Texture2D as a Vector2.
		/// </summary>
		public static Vector2 Size(this Texture2D texture)
		{
			return new Vector2(texture.Width, texture.Height);
		}
	}
}
