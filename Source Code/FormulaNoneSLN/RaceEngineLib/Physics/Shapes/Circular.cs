using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;


namespace Unv.RaceEngineLib.Physics.Shapes
{
	public class Circular
		: Shape
	{
		public float Radius
		{
			get { return m_radius; }
		}
		private readonly float m_radius = 10f;

		public override float Area
		{
			get { return MathHelper.Pi * Radius * Radius; }
		}

		public override Vector2 CenterOfMass { get { return Vector2.Zero; } }

		public Circular(float radius)
		{
			m_radius = radius;
		}
	}
}
