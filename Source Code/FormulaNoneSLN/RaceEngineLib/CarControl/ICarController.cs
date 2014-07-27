using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Unv.RaceEngineLib.CarControl
{
	public interface ICarController
	{
		/// <summary>
		/// Gets the position of the Car's Steering Wheel. This should range from -1.0f to 1.0f.
		/// </summary>
		float	SteeringPosition	{ get; }

		/// <summary>
		/// Gets the position of the Gas Pedal. This should range from 0.0f to 1.0f.
		/// </summary>
		float	GasPedalPosition	{ get; }

		/// <summary>
		/// Gets the position of the Break Pedal. This shold range from 0.0f to 1.0f.
		/// </summary>
		float	BreakPedalPosition	{ get; }

		/// <summary>
		/// Gets whether or not the car is set to reverse.
		/// </summary>
		bool	IsInReverse			{ get; }

		/// <summary>
		/// Gets or sets the car that is supposed to be using these control settings.
		/// </summary>
		RaceCar		RaceCar		{ get; set; }

		/// <summary>
		/// Gets or set the RaceEngine, that the RaceCar is suppose to be controlling
		/// the RaceCar.
		/// </summary>
		RaceEngine	RaceEngine	{ get; set; }


		/// <summary>
		/// Updates the control settings for the car.
		/// </summary>
		void Update(float elapsedTimeMS);
	}
}
