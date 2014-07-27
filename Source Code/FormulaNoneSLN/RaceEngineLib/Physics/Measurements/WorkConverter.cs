

namespace Unv.RaceEngineLib.Physics.Measurements
{
	public static class WorkConverter
	{
		public static float HorsepowerToWatt(float hp)
		{
			return hp * 745.699872f;
		}

		public static float WattToHorsepower(float watts)
		{
			return watts * 0.00134102209f;
		}
	}
}
