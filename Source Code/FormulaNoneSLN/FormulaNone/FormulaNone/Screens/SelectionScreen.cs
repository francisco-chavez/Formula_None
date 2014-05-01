﻿using System;
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
	public class SelectionScreen
		: GameScreen
	{
		#region Attributes
		private List<Texture2D>					m_raceTracks;
		private Dictionary<string, Texture2D>	m_carImages;
		private ContentManager					m_content;
		private ControlManager					m_uiControls;
		#endregion


		#region Properties
		public static string SelectedCar { get; private set; }
		#endregion


		#region Initialization
		public SelectionScreen()
		{
			m_carImages  = new Dictionary<string, Texture2D>(6);
			m_raceTracks = new List<Texture2D>(4);
		}

		public override void LoadContent()
		{
			if (m_content == null)
				m_content = new ContentManager(Game.Services, "Content/Images");

			if (m_uiControls == null)
				m_uiControls = new ControlManager();
			else
				m_uiControls.Clear();



			string[] filepaths = null;
			
			// Load car images
			filepaths = Directory.GetFiles("./Content/Images/Cars/", "*.xnb", SearchOption.TopDirectoryOnly);
			foreach (var path in filepaths)
			{
				string imageName	= Path.GetFileNameWithoutExtension(path);
				Texture2D image		= m_content.Load<Texture2D>(string.Format("Cars/{0}", imageName));
				image.Tag = imageName;

				if (m_carImages.ContainsKey(imageName))
					m_carImages[imageName] = image;
				else
					m_carImages.Add(imageName, image);
			}

			base.LoadContent();
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
