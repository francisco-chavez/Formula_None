

namespace Unv.RaceEngineLib.Physics.Measurements
{
	public static class LengthConverter
	{
		// 1 meter  is about 0.0250000000002418080000002302012 inches if I don't correct for significant figures.
		public const float INCH_TO_METER_SCALE = 0.0254000000f;


		public static float InchToMeter(float inches)
		{
			// 1 meter  is about 0.0250000000002418080000002302012 inches if I don't correct for significant figures.
			return inches * INCH_TO_METER_SCALE;
		}

		public static float MeterToInch(float meters)
		{
			return meters * 39.3700787f;
		}

		public static float MeterToFoot(float meters)
		{
			return meters * 3.28083990f;
		}

		public static float FootToMeter(float feet)
		{
			return feet * 0.304800000f;
		}
	}
}
