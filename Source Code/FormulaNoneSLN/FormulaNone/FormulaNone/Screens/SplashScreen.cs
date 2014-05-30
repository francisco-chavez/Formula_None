using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Unv.FormulaNone.Screens;


namespace Unv.FormulaNone.Screens
{
	/// <summary>
	/// The Splash Screen will show a series of images representing the
	/// makers of this game when the game first starts up.
	/// </summary>
	public sealed class SplashScreen
		: GameScreen
	{
		#region Attributes
		private const int		MILLISECONDS_PER_IMAGE	= 2250;

		private Color			m_imageLighting			= Color.Black;
		private int				m_currentImageIndex		= 0;
		private int				m_currentTimeOnImageMS	= 0;
		private bool			m_moveToNextScreen		= false;

		private ContentManager	m_content;
		#endregion


		#region Properties
		public List<Texture2D> SplashImages { get; private set; }
		#endregion


		#region Initialization
		public SplashScreen()
		{
			SplashImages = new List<Texture2D>();

			TransitionOffTime	= TimeSpan.FromSeconds(0.1);
			TransitionOnTime	= TimeSpan.FromSeconds(0.1);
		}

		public override void LoadContent()
		{
			// The images displayed on this screen will only be displayed
			// once. There's no point holding them in memory so we will
			// be using our own instance of the ContentManager to unload
			// them once we're done.
			if (m_content == null)
				m_content = new ContentManager(Game.Services, "Content");


			// Find the splash images used for this screen. We will be searching
			// for xnb files because that's what png files get converted to. Then 
			// load each image into the SplashImages property from the content manager.
			string[] files = 
				Directory.GetFiles(
					"./Content/Images/Backgrounds/SplashImages", 
					"*.xnb", 
					SearchOption.TopDirectoryOnly);

			foreach (var filepath in files)
			{
				string imageName = Path.GetFileNameWithoutExtension(filepath);
				Texture2D splashImage = 
					m_content.Load<Texture2D>(
						string.Format(
							"Images/Backgrounds/SplashImages/{0}", 
							imageName));
				SplashImages.Add(splashImage);
			}

			base.LoadContent();
		}

		public override void UnloadContent()
		{
			SplashImages.Clear();

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
			if (this.IsActive)
			{
				if (!m_moveToNextScreen)
				{
					m_currentTimeOnImageMS += (int) gameTime.ElapsedGameTime.TotalMilliseconds;

					if (m_currentTimeOnImageMS < MILLISECONDS_PER_IMAGE)
					{
						UpdateImageLighting();
					}
					else
					{
						m_currentImageIndex++;
						m_currentTimeOnImageMS = 0;
						m_imageLighting = Color.Black;

						m_moveToNextScreen = m_currentImageIndex >= SplashImages.Count;
					}
				}
				else
				{
					LoadingScreen.Load(
						ScreenManager,
						false,
						null,
						new TitleScreen());
				}
			}

			base.Update(gameTime, otherScreenHasFocus, false);
		}

		public override void Draw(GameTime gameTime)
		{
			if (m_currentImageIndex < SplashImages.Count)
			{
				var spriteBatch		= ScreenManager.SpriteBatch;
				var clientBounds	= Game.Window.ClientBounds;

				spriteBatch.Begin();
				spriteBatch.Draw(
					SplashImages[m_currentImageIndex],
					new Rectangle(0, 0, clientBounds.Width, clientBounds.Height),
					m_imageLighting);
				spriteBatch.End();
			}

			base.Draw(gameTime);
		}

		/// <summary>
		/// This method sets the amount of lighting to use on each splash image
		/// based on the amount of time we have spent on each image.
		/// </summary>
		private void UpdateImageLighting()
		{
			// We are spliting the time up into 5 event parts.
			// The first two parts will act as one part to increase
			// increase the light from Black to White. Parts three
			// and four will also act as one part, but they will hold
			// a steady white light. The last part will decrease the
			// light from White to Black.
			int timeUnit = MILLISECONDS_PER_IMAGE / 5;

			float alpha;

			if (m_currentTimeOnImageMS < 2 * timeUnit)
			{
				alpha = (float) m_currentTimeOnImageMS / (2 * timeUnit);
			}
			else if (m_currentTimeOnImageMS < 4 * timeUnit)
			{
				// I could set Color.White in this area, but using an alpha
				// value in all three blocks and pulling out the color 
				// creation reduces the amount of code. This comment more 
				// than makes up for the amount of code I saved.
				// -FCT
				alpha = 1f;
			}
			else
			{
				alpha = (float) (MILLISECONDS_PER_IMAGE - m_currentTimeOnImageMS) / timeUnit;
			}

			m_imageLighting = new Color(alpha, alpha, alpha);
		}
		#endregion	
	}
}
