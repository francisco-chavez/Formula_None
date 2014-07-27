
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Unv.RaceEngineLib;
using Unv.RaceEngineLib.CarControl;


namespace Unv.FormulaNone
{
	/// <summary>
	/// This class converts the input from a GamePad, into input for a RaceCar instance.
	/// </summary>
	public class CarController_Player
		: ICarController
	{
		#region Attributes
		/// <summary>
		/// This class pulls keeps track of the input from the game pads and keyboard.
		/// </summary>
		private InputState	m_input;

		/// <summary>
		/// This tells us which GamePad to pull our information from.
		/// </summary>
		private PlayerIndex m_playerIndex;
		#endregion


		#region Properties
		/// <summary>
		/// Gets the position of the Car's Steering Wheel. This should range from -1.0f to 1.0f.
		/// </summary>
		public float		SteeringPosition	{ get; private set; }

		/// <summary>
		/// Gets the position of the Gas Pedal. This should range from 0.0f to 1.0f.
		/// </summary>
		public float		GasPedalPosition	{ get; private set; }

		/// <summary>
		/// Gets the position of the Break Pedal. This shold range from 0.0f to 1.0f.
		/// </summary>
		public float		BreakPedalPosition	{ get; private set; }

		/// <summary>
		/// Gets whether or not the car is set to reverse.
		/// </summary>
		public bool			IsInReverse			{ get; private set; }

		/// <summary>
		/// Gets or sets the car that is supposed to be using these control settings.
		/// </summary>
		public RaceCar		RaceCar				{ get; set; }

		/// <summary>
		/// Gets or set the RaceEngine, that the RaceCar is suppose to be controlling
		/// the RaceCar.
		/// </summary>
		public RaceEngine	RaceEngine			{ get; set; }
		#endregion


		#region Constructors
		public CarController_Player(InputState input, PlayerIndex playerIndex)
		{
			this.m_input			= input;
			this.m_playerIndex		= playerIndex;

			this.BreakPedalPosition = 0f;
			this.GasPedalPosition	= 0f;
			this.IsInReverse		= false;
			this.RaceCar			= null;

			this.RaceEngine			= null;
			this.SteeringPosition	= 0f;
		}
		#endregion


		#region Methods
		/// <summary>
		/// Updates the control settings for the car.
		/// </summary>
		public void Update(float elapsedTimeMS)
		{
			var gamePadState = m_input.CurrentGamePadStates[(int) m_playerIndex];

			this.BreakPedalPosition = MathHelper.Clamp(gamePadState.Triggers.Left,		 0f, 1f);
			this.GasPedalPosition	= MathHelper.Clamp(gamePadState.Triggers.Right,		 0f, 1f);
			this.SteeringPosition	= MathHelper.Clamp(gamePadState.ThumbSticks.Left.X, -1f, 1f);
			this.IsInReverse		= gamePadState.Buttons.A == ButtonState.Pressed;
		}
		#endregion
	}
}
