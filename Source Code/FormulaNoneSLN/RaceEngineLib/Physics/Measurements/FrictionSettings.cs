using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unv.RaceEngineLib.Physics.Measurements
{
	public static class FrictionSettings
	{
		public static readonly FrictionCoefficient RubberOnConcrete	= new FrictionCoefficient(1f, 0.8f, .02f);
		public static readonly FrictionCoefficient SteelOnSteel		= new FrictionCoefficient(0.8f, 0.6f, 0.002f);

		/// <summary>
		/// I didn't find any concrete info for rubber on steel, so I 
		/// averaged out the rubber on concrete and the steel on steel.
		/// </summary>
		public static readonly FrictionCoefficient RubberOnSteel	= new FrictionCoefficient(0.9f, 0.7f, 0.101f);


		public struct FrictionCoefficient
		{
			public readonly float StaticFriction;
			public readonly float KineticFriction;
			public readonly float RollingFriction;

			public FrictionCoefficient(float staticFriction, float kineticFriction, float rollingFriction)
			{
				StaticFriction = staticFriction;
				KineticFriction = kineticFriction;
				RollingFriction = rollingFriction;
			}
		}
	}
}
