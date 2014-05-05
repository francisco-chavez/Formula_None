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
				new Color(TransitionAlpha * 0f, TransitionAlpha * 0.1f, TransitionAlpha * 0f));

			spriteBatch.End();

			base.Draw(gameTime);
		}
		#endregion
	}
}
