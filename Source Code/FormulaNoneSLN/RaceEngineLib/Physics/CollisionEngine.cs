using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Unv.RaceEngineLib.Physics.Measurements;
using Unv.RaceEngineLib.Physics.Shapes;


namespace Unv.RaceEngineLib.Physics
{
	internal class CollisionEngine
	{
		#region Attributes
		private List<Body> _mobileBodies	= null;
		private List<Body> _immobileBodies	= null;
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
			List<Pair>		needCloserLook	= new List<Pair>();
			List<Impact>	impacts			= new List<Impact>();

			for (int i = 0; i < _mobileBodies.Count; i++)
			{
				for (int j = 0; j < _immobileBodies.Count; j++)
					if (ShouldLookCloser(_mobileBodies[i], _immobileBodies[j]))
						needCloserLook.Add(new Pair() { A = _mobileBodies[i], B = _immobileBodies[j] });

				for (int j = i + 1; j < _mobileBodies.Count; j++)
					if (ShouldLookCloser(_mobileBodies[i], _mobileBodies[j]))
						needCloserLook.Add(new Pair() { A = _mobileBodies[i], B = _mobileBodies[j] });
			}

			foreach (var pair in needCloserLook)
			{
				Impact impactOnA;
				Impact impactOnB;

				bool doTheyCollide = DoTheyCollide(pair.A, pair.B, out impactOnA, out impactOnB);

				if (doTheyCollide)
				{
					impacts.Add(impactOnA);
					impacts.Add(impactOnB);
				}
			}

			needCloserLook.Clear();
			impacts.Clear();
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

		private bool DoTheyCollide(Body bodyA, Body bodyB, out Impact impactOnA, out Impact impactOnB)
		{
			///
			/// I'm cheating a bit because I know:
			/// * That all the cars use the same ConvexPolygon for their collision shape
			/// * That the first body is always a car body
			/// * That the second body is either a car or an obstacle
			/// * That the obstacle body collision shape is always a Circular shape
			/// * That the Circular shapes are immobile
			if (bodyB.CollisionDetectionShape is Circular)
				return DoTheyCollide(bodyA, (ConvexPolygon) bodyA.CollisionDetectionShape, out impactOnA, bodyB, (Circular) bodyB.CollisionDetectionShape, out impactOnB);
			else if (bodyB.CollisionDetectionShape is ConvexPolygon)
				return DoTheyCollide(bodyA, out impactOnA, bodyB, out impactOnB, (ConvexPolygon) bodyA.CollisionDetectionShape);

			throw new ArgumentException();
		}

		private bool DoTheyCollide(Body bodyA, ConvexPolygon impactShapeA, out Impact impactOnA, Body bodyB, Circular impactShapeB, out Impact impactOnB)
		{
			impactOnA = new Impact();
			impactOnB = new Impact();

			Vector2[] bodyAPoints = new Vector2[impactShapeA.BorderPoints.Length];
			impactShapeA.BorderPoints.CopyTo(bodyAPoints, 0);

			for (int i = 0; i < bodyAPoints.Length; i++)
			{
				bodyAPoints[i] = bodyAPoints[i].Rotate(bodyA.Rotation);
				bodyAPoints[i] = bodyAPoints[i] + bodyA.Position;
			}

			Vector2 axisNorm	= Vector2.UnitX;
			float	minPen		= float.MaxValue;

			for (int i = 0; i < bodyAPoints.Length; i++)
			{
				Vector2 point1		= bodyAPoints[i];
				Vector2 point2		= bodyAPoints[(i + 1) % bodyAPoints.Length];
				Vector2 line		= point2 - point2;
				Vector2 normalAxis	= line.NormalVector();

				float bExtentCenter = Vector2.Dot(normalAxis, bodyB.Position);

				float aMax = float.MinValue;
				float aMin = float.MaxValue;

				float bMax = bExtentCenter + impactShapeB.Radius;
				float bMin = bExtentCenter - impactShapeB.Radius;


				for (int j = 0; i < bodyAPoints.Length; j++)
				{
					aMax = (float) Math.Max(aMax, Vector2.Dot(normalAxis, bodyAPoints[j]));
					aMin = (float) Math.Min(aMin, Vector2.Dot(normalAxis, bodyAPoints[j]));
				}

				float aExtentCenter = (aMax + aMin) / 2f;

				if (aMax < bMin)
					return false;

				float penAlongThisAxis = ((aMax - aMin) / 2f) + ((bMax - bMin) / 2f) - Math.Abs(bExtentCenter - aExtentCenter);

				if (penAlongThisAxis <= 0)
					return false;

				if (penAlongThisAxis < minPen)
				{
					axisNorm = normalAxis;
					minPen = penAlongThisAxis;
				}
			}

			// We now have both the axis of minimum penatration and the amount of penatration along that axis.

			throw new NotImplementedException();
		}

		private bool DoTheyCollide(Body bodyA, out Impact impactOnA, Body bodyB, out Impact impactOnB, ConvexPolygon impactShape)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
