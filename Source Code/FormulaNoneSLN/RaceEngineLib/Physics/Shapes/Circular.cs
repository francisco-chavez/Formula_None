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
		#region Properties
		public override Vector2 CenterOfMassShift
		{
			get { return Vector2.Zero; }
		}

		public float Radius
		{
			get { return m_radius; }
		}
		private readonly float m_radius = 10f;

		public override float Area
		{
			get { return MathHelper.Pi * Radius * Radius; }
		}

		public override float QuickRadius { get { return Radius + 0.1f; } }
		#endregion


		public Circular(float radius)
		{
			m_radius = radius;
		}
	}
}
