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

		private static bool DoTheyCollide(Circular shapeA, Circular shapeB, Vector2 positionA, 
										  Vector2 positionB, out Vector2 normal, out float penetration)
		{
			normal = Vector2.Zero;
			penetration = 0f;

			Vector2 positionDelta = positionB - positionA;
			float minR = shapeA.Radius + shapeB.Radius;

			bool collide = positionDelta.LengthSquared() <= minR;

			if (collide)
			{
				float deltaLength	= positionDelta.Length();
				normal				= positionDelta / deltaLength;
				penetration			= minR - deltaLength;
			}

			return collide;
		}

		private static bool DoTheyCollide(Triangle shapeA, Circular shapeB, Body bodyA, 
										  Vector2 positionB, out Vector2 impactNormal, out float penetration)
		{
			impactNormal = Vector2.Zero;
			penetration = 0f;

			Vector2[] pointsA = shapeA.GetAdjustedPointPositions(bodyA.Position, bodyA.Rotation);
			Vector2[] normals = CreateNormalUnitVectors(pointsA);

			float	minPenatration	= float.MaxValue;
			bool	doTheyCollide	= true;

			Vector2 positionDelta = positionB - bodyA.Position;

			for (int i = 0; i < 3; i++)
			{
				Vector2 radiusVector = normals[i] * shapeB.Radius;
				Range extentRangeA = Range.FindExtent(normals[i], pointsA);
				Range extentRangeB = Range.FindExtent(normals[i], positionB - radiusVector, positionB + radiusVector);

				if (extentRangeA.Max < extentRangeB.Min || extentRangeB.Max < extentRangeB.Min)
				{
					doTheyCollide = false;
					break;
				}

				float halfExtentLengthA = extentRangeA.Length / 2f;
				float halfExtentLengthB = extentRangeB.Length / 2f;

				float positionDeltaExtent = Vector2.Dot(positionDelta, normals[i]);

				float overlap = halfExtentLengthA + halfExtentLengthB - Math.Abs(positionDeltaExtent);
				
				if (overlap < minPenatration)
				{
					minPenatration = overlap;
					impactNormal = normals[i];
				}
			}

			if (doTheyCollide)
				penetration = minPenatration;

			return doTheyCollide;
		}

		private static bool DoTheyCollide(ConvexPolygon shapeA, Circular shapeB, Body bodyA, 
										  Vector2 positionB, out Vector2 impactNormal, out float penetration)
		{
			impactNormal = Vector2.Zero;
			penetration  = 0f;

			Vector2[] pointsA = shapeA.GetAdjustedPointPositions(bodyA.Position, bodyA.Rotation);
			Vector2[] normals = CreateNormalUnitVectors(pointsA);

			float	minPenatration	= float.MaxValue;
			bool	doTheyCollide	= true;

			Vector2 positionDelta = positionB - bodyA.Position;

			for (int i = 0; i < 3; i++)
			{
				Vector2 radiusVector = normals[i] * shapeB.Radius;
				Range extentRangeA = Range.FindExtent(normals[i], pointsA);
				Range extentRangeB = Range.FindExtent(normals[i], positionB - radiusVector, positionB + radiusVector);

				if (extentRangeA.Max < extentRangeB.Min || extentRangeB.Max < extentRangeB.Min)
				{
					doTheyCollide = false;
					break;
				}

				float halfExtentLengthA = extentRangeA.Length / 2f;
				float halfExtentLengthB = extentRangeB.Length / 2f;

				float positionDeltaExtent = Vector2.Dot(positionDelta, normals[i]);

				float overlap = halfExtentLengthA + halfExtentLengthB - Math.Abs(positionDeltaExtent);

				if (overlap < minPenatration)
				{
					minPenatration = overlap;
					impactNormal = normals[i];
				}
			}

			if (doTheyCollide)
				penetration = minPenatration;

			return doTheyCollide;
		}

		private static bool DoTheyCollide(Triangle shapeA, ConvexPolygon shapeB, Body bodyA, 
										  Body bodyB, out Vector2 impactNormal, out float penetration)
		{
			impactNormal = Vector2.Zero;
			penetration  = 0f;

			Vector2[] pointsA = shapeA.GetAdjustedPointPositions(bodyA.Position, bodyA.Rotation);
			Vector2[] pointsB = shapeB.GetAdjustedPointPositions(bodyB.Position, bodyB.Rotation);

			Vector2[] normalsA = CreateNormalUnitVectors(pointsA);
			Vector2[] normalsB = CreateNormalUnitVectors(pointsB);

			Vector2 positionDelta = bodyB.Position - bodyA.Position;

			Vector2 impactVector1;
			float	penetration1;
			Vector2 impactVector2;
			float	penetration2;

			bool collisionCheck1 = 
				DoTheyCollide(pointsA, pointsB, positionDelta, normalsA, out impactVector1, out penetration1);

			if (!collisionCheck1)
				return false;

			bool collisionCheck2 =
				DoTheyCollide(pointsA, pointsB, positionDelta, normalsB, out impactVector2, out penetration2);

			if (!collisionCheck2)
				return false;

			if (penetration1 <= penetration2)
			{
				penetration  = penetration1;
				impactNormal = impactVector1;
			}
			else
			{
				penetration  = penetration2;
				impactNormal = impactVector2;
			}

			return true;
		}

		private static bool DoTheyCollide(Vector2[] borderPointsA, Vector2[] borderPointsB, 
										  Vector2 positionDelta, Vector2[] unitNormals, 
										  out Vector2 impactNormal, out float penetration)
		{
			bool	doTheyCollide	= true;
			float	minPenetration	= float.MaxValue;
					impactNormal	= Vector2.Zero;
					penetration		= 0f;

			for (int i = 0; i < unitNormals.Length; i++)
			{
				Range extentRangeA = Range.FindExtent(unitNormals[i], borderPointsA);
				Range extentRangeB = Range.FindExtent(unitNormals[i], borderPointsB);

				if (extentRangeA.Max < extentRangeB.Min || extentRangeB.Max < extentRangeB.Min)
				{
					doTheyCollide = false;
					break;
				}

				float halfExtentLengthA = extentRangeA.Length / 2f;
				float halfExtentLengthB = extentRangeB.Length / 2f;

				float positionDeltaExtent = Vector2.Dot(positionDelta, unitNormals[i]);

				float overlap = halfExtentLengthA + halfExtentLengthB - Math.Abs(positionDeltaExtent);

				if (overlap < minPenetration)
				{
					minPenetration = overlap;
					impactNormal = unitNormals[i];
				}
			}

			if (doTheyCollide)
				penetration = minPenetration;

			return doTheyCollide;
		}

		private static Vector2[] CreateNormalUnitVectors(Vector2[] positions)
		{
			Vector2[] normals = new Vector2[positions.Length];

			for (int i = 0; i < normals.Length; i++)
			{
				normals[i] = positions[(i + 1) % normals.Length] - positions[i];
				normals[i] = normals[i].NormalVector();
			}

			return normals;
		}
		#endregion
	}
}
