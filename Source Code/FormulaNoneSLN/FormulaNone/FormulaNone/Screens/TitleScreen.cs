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
	public class TitleScreen
		: GameScreen
	{
		#region Attributes
		private ContentManager m_content;

		private Texture2D m_background;
		private Texture2D m_aButtonImage;
		private Texture2D m_bButtonImage;
		#endregion


		#region Initialization
		public TitleScreen()
		{
			TransitionOffTime = TimeSpan.FromSeconds(0.35);
			TransitionOnTime  = TimeSpan.FromSeconds(0.35);
		}

		public override void LoadContent()
		{
			if (m_content == null)
				m_content = new ContentManager(Game.Services, "Content");

			m_aButtonImage	= m_content.Load<Texture2D>("Images/UI/Button_A_32");
			m_bButtonImage	= m_content.Load<Texture2D>("Images/UI/Button_B_32");
			m_background	= m_content.Load<Texture2D>("Images/Backgrounds/TitlePageBackground");

			base.LoadContent();
		}

		public override void UnloadContent()
		{
			m_aButtonImage	= null;
			m_background	= null;
			m_bButtonImage	= null;

			if (m_content != null)
			{
				m_content.Unload();
				m_content.Dispose();
				m_content = null;
			}

			base.UnloadContent();
		}
		#endregion


		#region Update & Draw
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, false);
		}

		public override void Draw(GameTime gameTime)
		{
			string[] strings = new string[] { "Press ", " to continue", " to exit" };

			var spriteBatch = ScreenManager.SpriteBatch;
			var font		= ScreenManager.Font;
			var clientBound = Game.Window.ClientBounds;

			float yPosition = clientBound.Height * 2 / 3;
			Color drawColor = Color.White * TransitionAlpha;


			spriteBatch.Begin();

			spriteBatch.Draw(m_background, new Rectangle(0, 0, clientBound.Width, clientBound.Height), drawColor);
			DrawSpan(spriteBatch, font, drawColor, clientBound.Width, yPosition, strings[0], m_aButtonImage, strings[1]);
			DrawSpan(spriteBatch, font, drawColor, clientBound.Width, yPosition + font.LineSpacing, strings[0], m_bButtonImage, strings[2]);

			spriteBatch.End();
		}

		/// <summary>
		/// This method draws a spanning line of text and images as if they were a single line of text.
		/// </summary>
		/// <remarks>
		/// I know this isn't a very adaptable method, but the way of drawing one line was the same as
		/// drawing another line (that includes the math of where to place the items). This method cuts
		/// down on the amout of clutter in the draw method).
		/// -FCT
		/// </remarks>
		private void DrawSpan(SpriteBatch spriteBatch, SpriteFont font, Color drawColor, float boundingWidth,
							  float y, string item1, Texture2D item2, string item3)
		{
			Vector2 stringSize		= font.MeasureString(item1 + item3);
			float	contentWidth	= stringSize.X + item2.Width;
			float	xPrime			= (boundingWidth - contentWidth) / 2;
			float	yImage			= y + (stringSize.Y - item2.Height) / 2;	// Make sure the image is centered on the line of text

			// Draw item 1
			spriteBatch.DrawString(font, item1, new Vector2(xPrime, y), drawColor);

			// Draw item 2
			xPrime += font.MeasureString(item1).X;
			spriteBatch.Draw(item2, new Vector2(xPrime, yImage), drawColor);

			// Draw item 3
			xPrime += item2.Width;
			spriteBatch.DrawString(font, item3, new Vector2(xPrime, y), drawColor);
		}

		public override void HandleInput(InputState input)
		{
			if (!IsActive)
				return;

			PlayerIndex playerIndex;

			if (input.IsNewButtonPress(Buttons.B, null, out playerIndex))
			{
				const string message = "Are you sure you want to exit this game?";
				MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);
				confirmExitMessageBox.Accepted += ConfirmExitMessageBox_Accepted;
				ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
			}
			else if (input.IsNewButtonPress(Buttons.A, null, out playerIndex))
			{
				LoadingScreen.Load(
					ScreenManager,
					false,
					playerIndex,
					new SelectionScreen());
			}
		}

		void ConfirmExitMessageBox_Accepted(object sender, PlayerIndexEventArgs e)
		{
			Game.Exit();
		}
		#endregion
	}
}
