using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Unv.RaceEngineLib;
using Unv.RaceEngineLib.Storage;


namespace Unv.FormulaNone.Screens
{
	public class RacingScreen
		: GameScreen
	{
		#region Attributes
		private ContentManager	m_content;
		private Texture2D		m_trackImage;
		private Texture2D		m_background;
		private RaceEngine		m_raceEngine;
		#endregion


		#region Initialization
		public RacingScreen()
		{
			this.TransitionOnTime  = TimeSpan.FromSeconds(0.5);
			this.TransitionOffTime = TimeSpan.FromSeconds(0.5);
		}

		public override void LoadContent()
		{
			if (m_content == null)
				m_content = new ContentManager(Game.Services, "Content");

			string	selectedTrack	= SelectionScreen.SelectedTrack;
			string	selectedCar		= SelectionScreen.SelectedCar;
			var		carTypes		= SelectionScreen.CarTypes;

			var obstacleMap = m_content.Load<ObstacleMap>(string.Format("DataMaps/Obstacles/{0}", selectedTrack));
			m_trackImage	= m_content.Load<Texture2D>(string.Format("Images/RaceTracks/{0}", selectedTrack));

			m_background = m_content.Load<Texture2D>("Images/Backgrounds/DefaultBackground");

			base.LoadContent();
		}

		public override void UnloadContent()
		{
			if (m_content != null)
			{
				m_content.Unload();
				m_content.Dispose();
				m_content = null;
			}

			base.UnloadContent();
		}
		#endregion


		#region Methods
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
		}

		public override void HandleInput(InputState input)
		{
			base.HandleInput(input);
		}

		public override void Draw(GameTime gameTime)
		{
			var raceTrackOffset = (Game.Window.ClientBounds.Size() - m_trackImage.Size()) / 2;
			var lighting = new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha);

			var spriteBatch = this.ScreenManager.SpriteBatch;

			spriteBatch.Begin();

			spriteBatch.Draw(m_background, Vector2.Zero, lighting);
			spriteBatch.Draw(m_trackImage, raceTrackOffset, lighting);

			spriteBatch.End();

			base.Draw(gameTime);
		}
		#endregion
	}
}
