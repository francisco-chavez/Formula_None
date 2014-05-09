using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Unv.RaceEngineLib;


namespace Unv.FormulaNone.Screens
{
	public class RacingScreen
		: GameScreen
	{
		private RaceEngine m_raceEngine;


		public RacingScreen()
		{
			this.TransitionOnTime = TimeSpan.FromSeconds(0.5);
			this.TransitionOffTime = TimeSpan.FromSeconds(0.5);
		}
	}
}
