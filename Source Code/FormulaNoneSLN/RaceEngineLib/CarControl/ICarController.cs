using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Unv.RaceEngineLib.CarControl
{
	public interface ICarController
	{
		float	SteeringPosition	{ get; }
		float	GasPedalPosition	{ get; }
		float	BreakPdealPosition	{ get; }
		bool	IsInReverse			{ get; }

		RaceCar		RaceCar		{ get; set; }
		RaceEngine	RaceEngine	{ get; set; }


		void Update(float elapsedTimeMS);
	}
}
