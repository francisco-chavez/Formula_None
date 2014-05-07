using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unv.RaceEngineLib.Physics.Collidables
{
	public class Circular
		: ShapeBase
	{
		public float Radius
		{
			get { return m_radius; }
			set
			{
				// I don't expect any values less then 4, but
				// I still want to keep things positive.
				// -FCT
				m_radius = value <= 0.001f ? 0.001f : value;
			}
		}
		private float m_radius = 10f;
	}
}
