using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Unv.RaceEngineLib.Physics.Measurements;


namespace Unv.RaceEngineLib.Physics
{
	public class PhysicsEngine
	{
		#region Events
		public event EventHandler UpdateForcesEvent;
		#endregion


		#region Attributes
		private List<Body> m_mobileBodies;
		private List<Body> m_immobileBodies;

		private List<Pair> m_pairs;
		#endregion


		#region Constructors
		public PhysicsEngine()
		{
			m_mobileBodies		= new List<Body>();
			m_immobileBodies	= new List<Body>();
			m_pairs				= new List<Pair>();
		}
		#endregion


		#region Methods
		public Body AddMobileBody(Shape shape, Vector2 position, string materialKey)
		{
			var material = MaterialSettings.Instance.Materials[materialKey];

			Body b = new Body();
			b.Shape = shape;
			b.Resitution = material.Restitution;

			MassData massData = new MassData();
			massData.Mass = material.Density * shape.Area;

			return b;
		}

		public Body AddImmobileBody(Shape shape, Vector2 position, string materialKey)
		{
			var material = MaterialSettings.Instance.Materials[materialKey];

			Body body = new Body();
			body.Shape			= shape;
			body.Resitution		= material.Restitution;

			MassData massData = new MassData();
			massData.Mass		= float.PositiveInfinity;
			massData.Inertia	= float.PositiveInfinity;

			body.MassData		= massData;

			m_immobileBodies.Add(body);

			return body;
		}

		public void ClearMobileBodies()
		{
			m_mobileBodies.Clear();
		}

		public void ClearImmobileBodies()
		{
			m_immobileBodies.Clear();
		}

		public void Step(float timeMS)
		{
			GeneratePairs();

			if (UpdateForcesEvent != null)
				UpdateForcesEvent(this, null);

			GenerateChanges();
			ApplyChanges();
		}

		/// <summary>
		/// This is a Broad Phase Collision Check. If two bodies might be
		/// colliding and one of those bodies is mobile, those two bodies
		/// will be thrown into the pairs list to take a close look at
		/// when we do the Narrows Phase Collision Check.
		/// </summary>
		private void GeneratePairs()
		{
			m_pairs.Clear();

			// mobile body to mobile body quick check
			for (int i = 0; i < m_mobileBodies.Count; i++)
			{
				for (int j = 0; j < m_mobileBodies.Count; j++)
				{
					float r = m_mobileBodies[i].Shape.QuickRadius + m_mobileBodies[j].Shape.QuickRadius;
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
					float r = m_immobileBodies[i].Shape.QuickRadius + m_mobileBodies[j].Shape.QuickRadius;
					float r2 = r * r;

					float distance2 = (m_immobileBodies[i].Position - m_mobileBodies[j].Position).LengthSquared();

					if (distance2 > r2)
						continue;

					m_pairs.Add(new Pair() { A = m_immobileBodies[i], B = m_mobileBodies[j] });
				}
			}
		}

		private void GenerateChanges()
		{
		}

		private void ApplyChanges()
		{
		}
		#endregion
	}
}
