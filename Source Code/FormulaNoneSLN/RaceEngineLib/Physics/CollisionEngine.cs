using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Unv.RaceEngineLib.Physics.Measurements;


namespace Unv.RaceEngineLib.Physics
{
	internal class CollisionEngine
	{
		#region Attributes
		private List<Body> _mobileBodies	= null;
		private List<Body> _immobileBodies	= null;
		private List<Pair> _pairs			= null;
		#endregion


		#region Constructors
		public CollisionEngine()
			: this(4, 100) { }

		/// <summary>
		/// This is a constructor for the Collision Engine.
		/// </summary>
		/// <param name="mobileCapacity">Sets starting capacity for the mobile bodies container.</param>
		/// <param name="immobileCapacity">Sets the starting capacity for the immobile bodies container.</param>
		public CollisionEngine(int mobileCapacity, int immobileCapacity)
		{
			if (mobileCapacity < 1)
				throw new ArgumentOutOfRangeException("The \"mobileCapacity\" must be greather than 0.");

			if (immobileCapacity < 1)
				throw new ArgumentOutOfRangeException("The \"immobileCapacity\" must be greather than 0.");

			_mobileBodies	= new List<Body>(mobileCapacity);
			_immobileBodies = new List<Body>(immobileCapacity);
			_pairs			= new List<Pair>(mobileCapacity);
		}
		#endregion


		#region Methods
		public void AddMobileBody(Body body)
		{
			if (body == null)
				throw new ArgumentNullException();

			if (_mobileBodies.Contains(body) || _immobileBodies.Contains(body))
				throw new ArgumentException();

			_mobileBodies.Add(body);
		}

		public void AddImmobileBody(Body body)
		{
			if (body == null)
				throw new ArgumentNullException();

			if (_mobileBodies.Contains(body) || _immobileBodies.Contains(body))
				throw new ArgumentException();

			_immobileBodies.Add(body);
		}

		public void Clear()
		{
			_mobileBodies.Clear();
			_immobileBodies.Clear();
		}

		public void ClearMobileBodies()
		{
			_mobileBodies.Clear();
		}

		public void ClearImmobileBodies()
		{
			_immobileBodies.Clear();
		}

		public void GenerateInitialCollisionData()
		{
			for (int i = 0; i < _mobileBodies.Count; i++)
			{
				for (int j = 0; j < _immobileBodies.Count; j++)
					if (ShouldLookCloser(_mobileBodies[i], _immobileBodies[j]))
						_pairs.Add(new Pair() { A = _mobileBodies[i], B = _immobileBodies[j] });

				for (int j = i + 1; j < _mobileBodies.Count; j++)
					if (ShouldLookCloser(_mobileBodies[i], _mobileBodies[j]))
						_pairs.Add(new Pair() { A = _mobileBodies[i], B = _mobileBodies[j] });
			}

			foreach (var pair in _pairs)
			{
			}

			_pairs.Clear();
		}

		/// <summary>
		/// This method tells us if we need to take a closer look to see if two bodies are colliding.
		/// </summary>
		/// <returns>
		/// If this is true, then the two bodies might be colliding. If false, then they are not colliding.
		/// </returns>
		private bool ShouldLookCloser(Body a, Body b)
		{
			float r			= a.CollisionDetectionShape.QuickRadius + b.CollisionDetectionShape.QuickRadius;
			float r2		= r * r;
			float distance2 = (a.Position - b.Position).LengthSquared();

			bool lookCloser = distance2 < r2;

			return lookCloser;
		}
		#endregion
	}
}
