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
		public Body AddMobileBody(Shape shape, float shapeThickness, Vector2 position, string materialKey)
		{
			var material = MaterialSettings.Instance.Materials[materialKey];

			Body body = new Body();
			body.CollisionDetectionShape	= shape;
			body.Resitution					= material.Restitution;
			body.Mass						= material.Density * shape.Area * shapeThickness;
			body.Inertia					= shape.InertiaOverMass * body.Mass;
			body.Position					= position;

			return body;
		}

		public Body AddImmobileBody(Shape shape, Vector2 position, string materialKey)
		{
			var material = MaterialSettings.Instance.Materials[materialKey];

			Body body = new Body();
			body.CollisionDetectionShape	= shape;
			body.Resitution					= material.Restitution;
			body.Mass						= float.PositiveInfinity;
			body.Inertia					= float.PositiveInfinity;
			body.Position					= position;

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
			if (UpdateForcesEvent != null)
				UpdateForcesEvent(this, null);

			ApplyChanges();
		}


		private void ApplyChanges()
		{
		}
		#endregion
	}
}
