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

		/// <summary>
		/// Returns (Izz over mass) at the point of origin.
		/// </summary>
		public override float InertiaOverMass
		{
			get
			{
				if (float.IsNaN(m_inertiaOverMass))
					m_inertiaOverMass = 0.5f * Radius * Radius;

				return m_inertiaOverMass;
			}
		}
		private float m_inertiaOverMass = float.NaN;
		#endregion


		public Circular(float radius)
		{
			m_radius = radius;
		}
	}
}
