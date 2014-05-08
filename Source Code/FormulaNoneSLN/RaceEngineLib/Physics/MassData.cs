using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Unv.RaceEngineLib.Physics
{
	public class MassData
	{
		public float Mass
		{
			get { return m_mass; }
			set
			{
				m_mass = value;
				MassInverse = 1f / value;
			}
		}
		public float m_mass = float.PositiveInfinity;

		public float Inertia
		{
			get { return m_inertia; }
			set
			{
				m_inertia = value;
				InertiaInverse = 1f / value;
			}
		}
		private float m_inertia = float.PositiveInfinity;


		public float MassInverse	{ get; private set; }
		public float InertiaInverse { get; private set; }
	}
}
