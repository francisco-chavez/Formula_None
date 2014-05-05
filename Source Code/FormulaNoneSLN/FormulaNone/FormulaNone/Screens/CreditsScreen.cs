using System;
using System.Collections.Generic;
using System.IO;
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
		#region Attributes
		private List<Tuple<string, string>> m_credits;
		private float						m_maxTitleWidth;
		private float						m_maxNameWidth;

		private ContentManager				m_content;
		private SpriteFont					m_titleFont;
		private SpriteFont					m_creditsFont;

		private Rectangle					m_drawArea;
		private Vector2						m_creditPositionOffset;
		#endregion


		#region Initialization
		public CreditsScreen()
		{
			this.TransitionOffTime = TimeSpan.FromSeconds(0.3);
			this.TransitionOnTime = TimeSpan.FromSeconds(0.3);
		}

		public override void LoadContent()
		{
			if (m_content == null)
				m_content = new ContentManager(Game.Services, "Content/Fonts");

			m_creditsFont = m_content.Load<SpriteFont>("CreditsFont");
			m_titleFont = m_content.Load<SpriteFont>("CreditsTitleFont");

			m_drawArea =
				new Rectangle(
					(int) (0.1f * Game.Window.ClientBounds.Width),
					(int) (0.1f * Game.Window.ClientBounds.Height),
					(int) (0.8f * Game.Window.ClientBounds.Width),
					(int) (0.8f * Game.Window.ClientBounds.Height));

			m_creditPositionOffset.X = m_drawArea.X;
			m_creditPositionOffset.Y = Game.Window.ClientBounds.Height;

			if (m_credits != null)
				m_credits.Clear();
			else
				m_credits = new List<Tuple<string, string>>();

			char[] splitCharacters = new char[] { ':' };

			using (StreamReader reader = new StreamReader("Credits.txt"))
			{
				string line;

				while ((line = reader.ReadLine()) != null)
				{
					var splitLine = line.Split(splitCharacters);
					if (splitLine.Length != 2)
						continue;

					m_credits.Add(new Tuple<string, string>(splitLine[0].Trim(), splitLine[1].Trim()));
				}
			}

			m_maxNameWidth = 0f;
			m_maxTitleWidth = 0f;
			foreach (var pair in m_credits)
			{
				m_maxTitleWidth = Math.Max(m_maxTitleWidth, m_creditsFont.MeasureString(pair.Item1).X);
				m_maxNameWidth = Math.Max(m_maxNameWidth, m_creditsFont.MeasureString(pair.Item2).X);
			}

			base.LoadContent();
		}

		public override void UnloadContent()
		{
			m_creditsFont = null;
			m_titleFont = null;
			if (m_credits != null)
				m_credits.Clear();
			m_credits = null;

			m_content.Unload();

			base.UnloadContent();
		}
		#endregion


		#region Methods
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, false);

			if (IsActive)
			{
				m_creditPositionOffset.Y -= 
					(float) gameTime.ElapsedGameTime.TotalSeconds * m_creditsFont.LineSpacing * 1.25f;
			}
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
			var spriteBatch			= ScreenManager.SpriteBatch;
			var canvas				= Game.WhiteTexture2D;
			var colorAlpha			= TransitionAlpha;
			var backgroundLighting	= new Color(colorAlpha * 0f, colorAlpha * 0.1f, colorAlpha * 0f);

			spriteBatch.Begin();

			spriteBatch.Draw(
				canvas,
				new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height),
				backgroundLighting);


			Color textColor = new Color(0f, 0.6f * TransitionAlpha, 0f);

			float freeSpaceX = m_drawArea.Width - (m_maxNameWidth + m_maxTitleWidth);
			float titleOffsetX = freeSpaceX / 4;
			float nameOffsetX = (freeSpaceX + m_maxTitleWidth) - titleOffsetX;

			for (int i = 0; i < m_credits.Count; i++)
			{
				Vector2 titlePosition;
				Vector2 namePosition;

				titlePosition = m_creditPositionOffset;
				titlePosition.X += titleOffsetX;
				titlePosition.Y += i * m_creditsFont.LineSpacing;

				namePosition = m_creditPositionOffset;
				namePosition.X += nameOffsetX;
				namePosition.Y = titlePosition.Y;

				spriteBatch.DrawString(
					m_creditsFont,
					m_credits[i].Item1,
					titlePosition,
					textColor);

				spriteBatch.DrawString(
					m_creditsFont,
					m_credits[i].Item2,
					namePosition,
					textColor);
			}

			spriteBatch.Draw(
				canvas,
				new Rectangle(0, 0, Game.Window.ClientBounds.Width, m_drawArea.X + m_titleFont.LineSpacing + 8),
				backgroundLighting);


			spriteBatch.DrawString(
				m_titleFont,
				"Credits",
				m_drawArea.Position() + new Vector2((m_drawArea.Width - m_titleFont.MeasureString("Credits").X) / 2, 0f),
				textColor);

			spriteBatch.End();

			base.Draw(gameTime);
		}
		#endregion
	}
}
