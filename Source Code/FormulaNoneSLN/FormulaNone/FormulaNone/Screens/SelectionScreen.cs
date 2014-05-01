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
	public class SelectionScreen
		: GameScreen
	{
		#region Attributes
		private List<Texture2D> m_raceTracks;
		#endregion


		#region Properties
		#endregion


		#region Initialization
		public SelectionScreen()
		{
			m_raceTracks = new List<Texture2D>(4);
		}
		#endregion


		#region Draw & Update
		public override void Draw(GameTime gameTime)
		{
			var spriteBatch		= ScreenManager.SpriteBatch;
			var drawColor		= new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha);
			var clientBounds	= Game.Window.ClientBounds;

			spriteBatch.Begin();

			spriteBatch.Draw(
				Game.DefaultBackground,
				new Rectangle(0, 0, clientBounds.Width, clientBounds.Height),
				drawColor);

			spriteBatch.End();
			base.Draw(gameTime);
		}
		#endregion
	}
}
