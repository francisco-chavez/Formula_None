using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Unv.RaceEngineLib.CarControl;
using Unv.RaceEngineLib.Physics;
using Unv.RaceEngineLib.Physics.Measurements;
using Unv.RaceEngineLib.Physics.Shapes;


namespace Unv.RaceEngineLib
{
	public class RaceCar
	{
		#region Attributes
		/*
		 * This is the starting info for the tires. I'm going to assume
		 * that the rims are a really light form of plastic, so I won't
		 * be estimating their data.
		 */
		public const float TIRE_RADIUS_REAR_INCHES		= 10f;
		public const float TIRE_RADIUS_FRONT_INCHES		= 7f;
		public const float TIRE_WIDTH_REAR_INCHES		= 8f;
		public const float TIRE_WIDTH_FRONT_INCHES		= 5f;
		public const float TIRE_RUBBER_THICKNESS_INCHES = 2f;

		private static readonly float TIRE_RADIUS_REAR		= LengthConverter.InchToMeter(TIRE_RADIUS_REAR_INCHES);
		private static readonly float TIRE_RADIUS_FRONT		= LengthConverter.InchToMeter(TIRE_RADIUS_FRONT_INCHES);
		private static readonly float TIRE_WIDTH_REAR		= LengthConverter.InchToMeter(TIRE_WIDTH_REAR_INCHES);
		private static readonly float TIRE_WIDTH_FRONT		= LengthConverter.InchToMeter(TIRE_WIDTH_FRONT_INCHES);
		private static readonly float TIRE_RUBBER_THICKNESS = LengthConverter.InchToMeter(TIRE_RUBBER_THICKNESS_INCHES);

		/*
		 * The positions of these tires are offset the same way that the car body shape is initially offset.
		 * They will need to be moved over so that they that they end up being relative to the car's center
		 * of gravity.
		 */
		public static readonly Vector2 LEFT_REAR_TIRE_POSITION_INCHES	= new Vector2(5f, 4f);
		public static readonly Vector2 RIGHT_REAR_TIRE_POSITION_INCHES	= new Vector2(5f, 27f);
		public static readonly Vector2 LEFT_FRONT_TIRE_POSITION_INCHES	= new Vector2(36f, 3.5f);
		public static readonly Vector2 RIGHT_FRONT_TIRE_POSITION_INCHES = new Vector2(36f, 27.5f);


		/// <summary>
		/// These points form a convex polynormal shape that is based
		/// on the images of the race cars. This is set in pixels which
		/// has a 1 to 1 conversion value to inches.
		/// </summary>
		private static readonly Vector2[] BORDER_POINTS_BASE = 
			new Vector2[] 
			{ 
				new Vector2(00f, 31f), new Vector2(36f, 31f), new Vector2(47f, 20f), 
				new Vector2(48f, 12f), new Vector2(36f, 01f), new Vector2(00f, 00f)
			};

		public const			float ENGINE_HORSEPOWER		= 4.1f;
		public static readonly	float ENGINE_POWER_WATTS	= WorkConverter.HorsepowerToWatt(ENGINE_HORSEPOWER);

		/// <summary>
		/// All cars have the same shape, so they'll be sharing the same 
		/// shape in memory.
		/// </summary>
		public static readonly Shape CAR_SHAPE;

		private static readonly Vector2 CENTER_OF_MASS_SHIFT_IN_INCHES;

		private CarTire m_rearLeftTire;
		private CarTire m_rearRightTire;
		private CarTire m_frontLeftTire;
		private CarTire m_frontRightTire;
		#endregion


		#region Properties
		public Body		Body				{ get; internal set; }
		public Vector2	CenterOfMassShift	{ get { return CAR_SHAPE.CenterOfMassShift; } }
		public Vector2	Position			{ get { return Body.Position; } }
		public Vector2	PositionInInches	{ get { return new Vector2(LengthConverter.MeterToInch(Body.Position.X), LengthConverter.MeterToInch(Body.Position.Y)); } }
		public float	Rotation			{ get { return Body.Rotation; } }

		public ICarController CarControls { get; set; }
		#endregion


		#region Constructors
		static RaceCar()
		{
			Vector2[] baseBorderPointsInMeters = new Vector2[BORDER_POINTS_BASE.Length];
			for (int i = 0; i < BORDER_POINTS_BASE.Length; i++)
			{
				var point = BORDER_POINTS_BASE[i];
				var pointPrime = 
					new Vector2(
						LengthConverter.InchToMeter(point.X), 
						LengthConverter.InchToMeter(point.Y));

				baseBorderPointsInMeters[i] = pointPrime;
			}

			CAR_SHAPE = new ConvexPolygon(baseBorderPointsInMeters);

			var centerOfMassShift = CAR_SHAPE.CenterOfMassShift;

			CENTER_OF_MASS_SHIFT_IN_INCHES =
				new Vector2(
					LengthConverter.MeterToInch(centerOfMassShift.X),
					LengthConverter.MeterToInch(centerOfMassShift.Y));
		}

		public RaceCar()
		{
			m_frontLeftTire		= new CarTire() { Density = 0.3f, Radius = TIRE_RADIUS_FRONT, RubberThickness = TIRE_RUBBER_THICKNESS, Width = TIRE_WIDTH_FRONT };
			m_frontRightTire	= new CarTire() { Density = 0.3f, Radius = TIRE_RADIUS_FRONT, RubberThickness = TIRE_RUBBER_THICKNESS, Width = TIRE_WIDTH_FRONT };
			m_rearLeftTire		= new CarTire() { Density = 0.3f, Radius = TIRE_RADIUS_REAR, RubberThickness = TIRE_RUBBER_THICKNESS, Width = TIRE_WIDTH_REAR };
			m_rearRightTire		= new CarTire() { Density = 0.3f, Radius = TIRE_RADIUS_REAR, RubberThickness = TIRE_RUBBER_THICKNESS, Width = TIRE_WIDTH_REAR };


			m_frontLeftTire.CarCenterOffset		= FindAdjustedTirePositionMetric(LEFT_FRONT_TIRE_POSITION_INCHES);
			m_frontRightTire.CarCenterOffset	= FindAdjustedTirePositionMetric(RIGHT_FRONT_TIRE_POSITION_INCHES);
			m_rearLeftTire.CarCenterOffset		= FindAdjustedTirePositionMetric(LEFT_REAR_TIRE_POSITION_INCHES);
			m_rearRightTire.CarCenterOffset		= FindAdjustedTirePositionMetric(RIGHT_REAR_TIRE_POSITION_INCHES);
		}
		#endregion


		#region Methods
		private Vector2 FindAdjustedTirePositionMetric(Vector2 startingPositionInches)
		{
			Vector2 position = startingPositionInches * LengthConverter.INCH_TO_METER_SCALE;
			position += CAR_SHAPE.CenterOfMassShift;

			return position;
			throw new NotImplementedException();
		}
		#endregion
	}
}
