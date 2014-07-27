
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Unv.RaceEngineLib;
using Unv.RaceEngineLib.CarControl;


namespace Unv.FormulaNone
{
	public class CarController_Player
		: ICarController
	{
		#region Attributes
		private InputState	m_input;
		private PlayerIndex m_playerIndex;
		#endregion


		#region Properties
		public float		SteeringPosition	{ get; private set; }
		public float		GasPedalPosition	{ get; private set; }
		public float		BreakPedalPosition	{ get; private set; }
		public bool			IsInReverse			{ get; private set; }

		public RaceCar		RaceCar				{ get; set; }
		public RaceEngine	RaceEngine			{ get; set; }
		#endregion


		#region Constructors
		public CarController_Player(InputState input, PlayerIndex playerIndex)
		{
			m_input			= input;
			m_playerIndex	= playerIndex;
		}
		#endregion


		#region Methods
		public void Update(float elapsedTimeMS)
		{
			var gamePadState = m_input.CurrentGamePadStates[(int) m_playerIndex];

			this.BreakPedalPosition = gamePadState.Triggers.Left;
			this.GasPedalPosition = gamePadState.Triggers.Right;
			this.IsInReverse = gamePadState.Buttons.A == ButtonState.Pressed;
			this.SteeringPosition = gamePadState.ThumbSticks.Left.X;
		}
		#endregion
	}
}
