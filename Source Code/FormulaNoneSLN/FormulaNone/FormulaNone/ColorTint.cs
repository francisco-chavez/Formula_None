using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;


namespace Unv.FormulaNone
{
	public class ColorTint
	{
		/// <summary>
		/// Gets or set a by which to tint your color. 0 uses
		/// returns a color's original color, 1 changes the color
		/// to your tint.
		/// </summary>
		public float TintAmount
		{
			get { return m_tintAmount; }
			set { m_tintAmount = MathHelper.Clamp(value, 0f, 1f); }
		}
		private float m_tintAmount = 0.05f;

		/// <summary>
		/// Gets or sets the color we'll be tinting any
		/// given color with.
		/// </summary>
		public Color Tint
		{
			get { return m_tint; }
			set { m_tint = value; }
		}
		private Color m_tint = Color.Black;


		public Color TintColor(Color originalColor)
		{
			Vector3 original = originalColor.ToVector3();
			Vector3 tint = Tint.ToVector3();

			original *= (1f - TintAmount);
			tint *= TintAmount;

			return new Color(original + tint);
		}


		#region Color Tint Managment
		private static Dictionary<string, ColorTint> StoredTints = new Dictionary<string, ColorTint>();


		public static void SetTint(string key, ColorTint tinter)
		{
			if (key == null)
				throw new ArgumentNullException("A key/name is required in order to set the tint.");
			if (tinter == null)
				throw new ArgumentNullException("In order to set a ColorTint, a ColorTint must be given.");

			if (StoredTints.ContainsKey(key))
				StoredTints[key] = tinter;
			else
				StoredTints.Add(key, tinter);
		}

		public static ColorTint GetTint(string key)
		{
			if (key == null)
				throw new ArgumentNullException("All ColorTints must be stored under a key.");

			if (!StoredTints.ContainsKey(key))
				throw new KeyNotFoundException(string.Format("No ColorTint is stored under the given key: {0}", key));

			return StoredTints[key];
		}

		public static void RemoveTint(string key)
		{
			if (StoredTints.ContainsKey(key))
				StoredTints.Remove(key);
		}
		#endregion
	}
}
