using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Unv.RaceEngineLib.Physics.Measurements;


namespace Unv.RaceEngineLib.Physics
{
	public class PhysicsEngine
	{
		private List<Body> m_mobileBodies = new List<Body>();
		private List<Body> m_immobileBodies = new List<Body>();

		private List<Pair> m_pairs = new List<Pair>();

		/// <summary>
		/// This is a Broad Phase Collision Check. If two bodies might be
		/// colliding and one of those bodies is mobile, those two bodies
		/// will be thrown into the pairs list to take a close look at
		/// when we do the Narrows Phase Collision Check.
		/// </summary>
		public void GeneratePairs()
		{
			m_pairs.Clear();

			// mobile body to mobile body quick check
			for (int i = 0; i < m_mobileBodies.Count; i++)
			{
				for (int j = 0; j < m_mobileBodies.Count; j++)
				{
					float r = m_mobileBodies[i].QuickSize + m_mobileBodies[j].QuickSize;
					float r2 = r * r;

					float distance2 = (m_mobileBodies[i].Position - m_mobileBodies[j].Position).LengthSquared();

					if (distance2 > r2)
						continue;

					m_pairs.Add(new Pair() { A = m_mobileBodies[i], B = m_mobileBodies[j] });
				}
			}

			// mobile body to immobile body quck check
			// I'm expecting more immobile bodies than mobile bodies, so
			// I'm placing the immobile bodies in the outter loop to
			// reduce the amount of caching needed from memory.
			for (int i = 0; i < m_immobileBodies.Count; i++)
			{
				for (int j = 0; j < m_mobileBodies.Count; j++)
				{
					float r = m_immobileBodies[i].QuickSize + m_mobileBodies[j].QuickSize;
					float r2 = r * r;

					float distance2 = (m_immobileBodies[i].Position - m_mobileBodies[j].Position).LengthSquared();

					if (distance2 > r2)
						continue;

					m_pairs.Add(new Pair() { A = m_immobileBodies[i], B = m_mobileBodies[j] });
				}
			}
		}
	}
}
