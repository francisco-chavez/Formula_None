#region File Description
//-----------------------------------------------------------------------------
// Game.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------


//-----------------------------------------------------------------------------
// A few things have been changed or added for the 
// sake of this game
// -FCT
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Unv.FormulaNone.Screens;
#endregion


namespace Unv.FormulaNone
{
	/// <summary>
	/// Sample showing how to manage different game states, with transitions
	/// between menu screens, a loading screen, the game itself, and a pause
	/// menu. This main game class is extremely simple: all the interesting
	/// stuff happens in the ScreenManager component.
	/// </summary>
	public class RaceGame 
		: Microsoft.Xna.Framework.Game
	{
		#region Fields

		GraphicsDeviceManager graphics;
		ScreenManager screenManager;


		// By preloading any assets used by UI rendering, we avoid framerate glitches
		// when they suddenly need to be loaded in the middle of a menu transition.
		static readonly string[] preloadAssets =
        {
            "gradient",
        };


		#endregion


		#region Properties
		public static RaceGame Instance { get; private set; }

		public Texture2D DefaultBackground { get; private set; }
		public Texture2D WhiteTexture2D { get; private set; }
		#endregion


		#region Initialization


		/// <summary>
		/// The main game constructor.
		/// </summary>
		public RaceGame()
		{
			/// I know that the proper way to do this is to set this
			/// constructor private and use the static constructor
			/// to set this property. Yet, there is only one copy of 
			/// the game class running (making it safe), and doing it
			/// this way requires less changes to the code I'm using
			/// as my base.
			/// -FCT
			Instance = this;

			Content.RootDirectory = "Content";

			/// I'm setting the game to 720p because that feels like 
			/// a safe image resolution these days. I know that a 
			/// variable resolution would be better, but this will do 
			/// for now.
			/// -FCT
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = 1280;
			graphics.PreferredBackBufferHeight = 720;

			// Create the screen manager component.
			screenManager = new ScreenManager(this);

			Components.Add(screenManager);

			// Activate the first screens.
			screenManager.AddScreen(new SplashScreen(), null);
		}


		/// <summary>
		/// Loads graphics content.
		/// </summary>
		protected override void LoadContent()
		{
			foreach (string asset in preloadAssets)
			{
				Content.Load<object>(asset);
			}

			DefaultBackground = Content.Load<Texture2D>("Images/Backgrounds/DefaultBackground");
			WhiteTexture2D = Content.Load<Texture2D>("Blank");
		}


		#endregion

		#region Draw


		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.Black);

			// The real drawing happens inside the screen manager component.
			base.Draw(gameTime);
		}


		#endregion
	}


	#region Entry Point

	/// <summary>
	/// The main entry point for the application.
	/// </summary>
	static class Program
	{
		static void Main()
		{
			using (RaceGame game = new RaceGame())
			{
				game.Run();
			}
		}
	}

	#endregion
}
