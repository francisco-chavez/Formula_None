using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Unv.FormulaNone.Screens
{
	public class CreditsScreen
		: GameScreen
	{
		#region Initialization
		public CreditsScreen()
		{
			this.TransitionOffTime = TimeSpan.FromSeconds(0.3);
			this.TransitionOnTime = TimeSpan.FromSeconds(0.3);
		}

		public override void LoadContent()
		{
			base.LoadContent();
		}

		public override void UnloadContent()
		{
			base.UnloadContent();
		}
		#endregion


		#region Methods
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, false);
		}

		public override void HandleInput(InputState input)
		{
			if (this.IsActive)
			{
				PlayerIndex playerIndex;

				if (input.IsNewButtonPress(Buttons.B, this.ControllingPlayer, out playerIndex))
					this.ExitScreen();
			}

			base.HandleInput(input);
		}

		public override void Draw(GameTime gameTime)
		{
			var spriteBatch = ScreenManager.SpriteBatch;
			var canvas = Game.WhiteTexture2D;

			spriteBatch.Begin();

			spriteBatch.Draw(
				canvas,
				new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height),
				new Color(TransitionAlpha * 0f, TransitionAlpha * 0.2f, TransitionAlpha * 0f));

			spriteBatch.End();

			base.Draw(gameTime);
		}
		#endregion
	}
}
