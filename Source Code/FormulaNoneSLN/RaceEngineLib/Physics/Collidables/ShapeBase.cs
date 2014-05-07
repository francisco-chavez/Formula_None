using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Unv.RaceEngineLib.Physics.Collidables
{
	public class ShapeBase
	{
		public float Mass
		{
			get { return m_mass; }
			set
			{
				// We don't accept negative mass
				m_mass = value < 0f ? 0f : value;
				MassInverse = 1f / m_mass;
			}
		}
		private float m_mass = float.PositiveInfinity;

		public float	MassInverse { get; private set; }
		public Vector2	Position	{ get; set; }
		public Vector2	Velocity	{ get; set; }
		public float	Restitution { get; set; }
	}
}
