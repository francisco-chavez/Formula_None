using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Unv.RaceEngineLib.CarControl;


namespace Unv.FormulaNone
{
	public class CarController_Player
		: ICarController
	{
		#region Attributes
		#endregion


		#region Properties
		public float SteeringPosition
		{
			get { throw new NotImplementedException(); }
		}

		public float GasPedalPosition
		{
			get { throw new NotImplementedException(); }
		}

		public float BreakPdealPosition
		{
			get { throw new NotImplementedException(); }
		}

		public bool IsInReverse
		{
			get { throw new NotImplementedException(); }
		}

		public RaceEngineLib.RaceCar RaceCar
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public RaceEngineLib.RaceEngine RaceEngine
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}
		#endregion


		#region Constructors
		public CarController_Player(InputState input, PlayerIndex playerIndex)
		{
		}
		#endregion


		#region Methods
		public void Update(float elapsedTimeMS)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
