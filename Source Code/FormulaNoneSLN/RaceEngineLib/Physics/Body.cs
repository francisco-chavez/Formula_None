using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Unv.RaceEngineLib.Physics.Measurements;
using Unv.RaceEngineLib.Physics.Shapes;


namespace Unv.RaceEngineLib.Physics
{
	public class Body
	{
		#region Properties
		public Shape	CollisionDetectionShape { get; set; }
		public float	Resitution			{ get; set; }


		//
		// Linear Motion Properties
		//
		public Vector2	Position	{ get; set; }
		public Vector2	Velocity	{ get; set; }

		public float Mass
		{
			get { return mn_mass; }
			set
			{
				mn_mass = value;
				MassInverse = 1f / value;
			}
		}
		private float mn_mass;

		public float MassInverse { get; private set; }


		//
		// Rotation Motion Properties
		//
		public float Rotation		{ get; set; }
		public float RotationSpeed	{ get; set; }

		public float Inertia
		{
			get { return mn_inertia; }
			set
			{
				mn_inertia = value;
				InertiaInverse = 1f / value;
			}
		}
		private float mn_inertia;

		public float InertiaInverse { get; private set; }
		#endregion


		#region Constructor
		public Body()
		{
		}
		#endregion


		#region Methods
		public static bool DoTheyCollide(Body a, Body b, out Impact impactA, out Impact impactB)
		{
			impactA = new Impact();
			impactB = new Impact();

			return false;
		}

		private static bool DoTheyCollide(Circular shapeA, Circular shapeB, Vector2 positionA, Vector2 positionB, out Vector2 normal, out float penatration)
		{
			normal = Vector2.Zero;
			penatration = 0f;

			Vector2 positionDelta = positionB - positionA;
			float minR = shapeA.Radius + shapeB.Radius;

			bool collide = positionDelta.LengthSquared() <= minR;

			if (collide)
			{
				float deltaLength	= positionDelta.Length();
				normal				= positionDelta / deltaLength;
				penatration			= minR - deltaLength;
			}

			return collide;
		}

		private static bool DoTheyCollide(Triangle shapeA, Circular shapeB, Body bodyA, Vector2 positionB, out Vector2 impactNormal, out float penatration)
		{
			impactNormal = Vector2.Zero;
			penatration = 0f;

			Vector2[] points = new Vector2[3];
			points[0] = shapeA.PointA.Rotate(bodyA.Rotation) + bodyA.Position;
			points[1] = shapeA.PointB.Rotate(bodyA.Rotation) + bodyA.Position;
			points[2] = shapeA.PointC.Rotate(bodyA.Rotation) + bodyA.Position;

			Vector2[] normals = new Vector2[3];
			for (int i = 0; i < 3; i++)
			{
				normals[i] = points[(i + 1) % 3] - points[i];
				normals[i] = normals[i].NormalVector();
			}

			float	minPenatration	= float.MaxValue;
			bool	doTheyCollide	= false;

			for (int i = 0; i < 3; i++)
			{
				Vector2 radiusVector = normals[i] * shapeB.Radius;
				Range extentRangeA = Range.FindExtent(normals[i], points);
				Range extentRangeB = Range.FindExtent(normals[i], positionB - radiusVector, positionB + radiusVector);

				if (extentRangeA.Max < extentRangeB.Min || extentRangeB.Max < extentRangeB.Min)
					continue;

				doTheyCollide = true;

				Vector2 positionDelta = positionB - bodyA.Position;

				float positionExtentA = Vector2.Dot(bodyA.Position, normals[i]);
				float positionExtentB = Vector2.Dot(positionB, normals[i]);

				throw new NotImplementedException();
				
				float pen = 0f;
				if (pen < minPenatration)
				{
					minPenatration = pen;
					impactNormal = Vector2.Zero;
				}
			}

			if (doTheyCollide)
				penatration = minPenatration;

			return doTheyCollide;
		}
		#endregion
	}
}
