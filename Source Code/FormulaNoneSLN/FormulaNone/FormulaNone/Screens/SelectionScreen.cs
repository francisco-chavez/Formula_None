using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Unv.FormulaNone.Controls;


namespace Unv.FormulaNone.Screens
{
	public class SelectionScreen
		: GameScreen
	{
		#region Attributes
		private List<Texture2D>					m_raceTracks;
		private ListDictionary					m_carImages;
		private ContentManager					m_content;
		private ControlManager					m_uiControlManager;

		private Texture2D						m_leftPointerImage;

		private FilmStripSelector				m_raceCarSelector;
		private FilmStripSelector				m_raceTrackSelector;
		private FilmStripSelector				m_actionSelector;

		private SpriteFont						m_uiFont;

		private SpriteFont						m_titleFont;
		private string							m_titleText				= "Make Your Selections";
		private Vector2							m_titleLocation;
		#endregion


		#region Properties
		public static string		SelectedCar		{ get; private set; }
		public static string		SelectedTrack	{ get; private set; }
		public static List<string>	CarTypes		{ get; private set; }
		#endregion


		#region Initialization
		public SelectionScreen()
		{
			m_carImages = new ListDictionary();
			m_raceTracks = new List<Texture2D>(4);
		}

		public override void LoadContent()
		{
			if (m_content == null)
				m_content = new ContentManager(Game.Services, "Content/Images");

			m_raceTracks.Clear();
			m_leftPointerImage	= m_content.Load<Texture2D>("UI/LeftPointer01_40x40");
			m_titleFont			= m_content.Load<SpriteFont>("../Fonts/PageTitleFont");
			m_uiFont			= m_content.Load<SpriteFont>("../Fonts/UIFont");


			string[] filepaths = null;
			
			// Load car images
			filepaths = Directory.GetFiles("./Content/Images/Cars/", "*.xnb", SearchOption.TopDirectoryOnly);
			CarTypes = new List<string>(filepaths.Length);

			foreach (var path in filepaths)
			{
				string imageName	= Path.GetFileNameWithoutExtension(path);
				Texture2D image		= m_content.Load<Texture2D>(string.Format("Cars/{0}", imageName));
				image.Tag = imageName;
				CarTypes.Add(imageName);

				if (m_carImages.Contains(imageName))
					m_carImages[imageName] = image;
				else
					m_carImages.Add(imageName, image);
			}

			// Load race track images
			filepaths = Directory.GetFiles("./Content/Images/RaceTracks/", "*.xnb", SearchOption.TopDirectoryOnly);
			foreach (var path in filepaths)
			{
				string imageName = Path.GetFileNameWithoutExtension(path);
				Texture2D image = m_content.Load<Texture2D>(string.Format("RaceTracks/{0}", imageName));
				image.Tag = imageName;

				m_raceTracks.Add(image);
			}

			SetUpUIControls();

			base.LoadContent();
		}

		public override void UnloadContent()
		{
			// We don't want the clear out to change our selections.
			m_raceCarSelector.SelectionChanged -= RaceCarSelector_SelectionChanged;
			m_raceTrackSelector.SelectionChanged -= RaceTrackSelector_SelectionChanged;

			m_raceTracks.Clear();
			if (m_uiControlManager != null)
				m_uiControlManager.Clear();
			if (m_content != null)
				m_content.Unload();

			base.UnloadContent();
		}

		private void SetUpUIControls()
		{
			// Manager Setup
			if (m_uiControlManager == null)
				m_uiControlManager = new ControlManager(this);
			else
				m_uiControlManager.Clear();

			Vector2		viewArea		= Game.Window.ClientBounds.Size(); 
			Vector2		safeViewStart	= viewArea * 0.1f;
			Vector2		safeViewEnd		= viewArea * 0.9f;
			Vector2		safeViewSize	= safeViewEnd - safeViewStart;
			Rectangle	safeViewArea	= new Rectangle(
											(int) safeViewStart.X,
											(int) safeViewStart.Y,
											(int) safeViewSize.X,
											(int) safeViewSize.Y);
			Vector2		titleSize		= m_titleFont.MeasureString(m_titleText);
			m_titleLocation = safeViewArea.Position();
			m_titleLocation.X += (safeViewArea.Width - titleSize.X) / 2f;

			m_uiControlManager.DrawArea =
				new Rectangle(
					(int) safeViewStart.X,
					(int) (safeViewStart.Y + titleSize.Y + 10),
					(int) safeViewSize.X,
					(int) (safeViewSize.Y - (titleSize.Y + 10)));


			// Set up race track selector
			m_raceTrackSelector = new FilmStripSelector(m_uiControlManager);
			m_raceTrackSelector.SelectionChanged	+= RaceTrackSelector_SelectionChanged;
			m_raceTrackSelector.ItemWidth			= 125;
			m_raceTrackSelector.ItemHeight			= 125;
			m_raceTrackSelector.Padding				= 13;
			m_raceTrackSelector.MustHaveItemSelected = true;
			m_raceTrackSelector.ShiftLeftIndicator	= m_leftPointerImage;
			foreach (var trackImage in m_raceTracks)
				m_raceTrackSelector.AddItem(trackImage.Tag.ToString(), trackImage);
			m_uiControlManager.AddControl(m_raceTrackSelector);


			// Set up the race cars selector
			m_raceCarSelector = new FilmStripSelector(m_uiControlManager);
			m_raceCarSelector.SelectionChanged		+= RaceCarSelector_SelectionChanged;
			m_raceCarSelector.ItemWidth				= 125;
			m_raceCarSelector.ItemHeight			= 125;
			m_raceCarSelector.Padding				= 13;
			m_raceCarSelector.MustHaveItemSelected	= true;
			m_raceCarSelector.ShiftLeftIndicator	= m_leftPointerImage;
			foreach (var carType in CarTypes)
				m_raceCarSelector.AddItem(carType, m_carImages[carType] as Texture2D, -MathHelper.PiOver2);
			m_uiControlManager.AddControl(m_raceCarSelector);


			// Set up player action selector
			m_actionSelector = new FilmStripSelector(m_uiControlManager);
			m_actionSelector.ItemWidth				= 125;
			m_actionSelector.ItemHeight				= 50;
			m_actionSelector.Padding				= 13;
			m_actionSelector.MustHaveItemSelected	= true;
			m_actionSelector.ShiftLeftIndicator		= m_leftPointerImage;

			m_actionSelector.AddItem("Play", "Start Racing");
			m_actionSelector.AddItem("Exit", "Exit Game");
			m_actionSelector.AddItem("Credits", "Roll Credits");

			m_uiControlManager.AddControl(m_actionSelector);
		}
		#endregion


		#region Event Handlers
		void RaceCarSelector_SelectionChanged(ControlBase source, SelectionChangedEventArgs e)
		{
			SelectedCar = m_raceCarSelector.SelectedValue;
		}

		void RaceTrackSelector_SelectionChanged(ControlBase source, SelectionChangedEventArgs e)
		{
			SelectedTrack = m_raceTrackSelector.SelectedValue;
		}
		#endregion


		#region Methods
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

			m_uiControlManager.Draw(spriteBatch);


			for (int i = 0; i < 5; i++)
			{
				Vector2 shadowPosition = m_titleLocation + i * new Vector2(1, 1);

				spriteBatch.DrawString(
					m_titleFont,
					m_titleText,
					shadowPosition,
					new Color(0f * TransitionAlpha, 0.4f * TransitionAlpha, 0f * TransitionAlpha));
			}
			spriteBatch.DrawString(
				m_titleFont, 
				m_titleText, 
				m_titleLocation, 
				new Color(0f * TransitionAlpha, 0.8f * TransitionAlpha, 0f * TransitionAlpha));

			spriteBatch.End();

			base.Draw(gameTime);
		}

		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			if (this.IsActive)
				m_uiControlManager.Update(gameTime);

			base.Update(gameTime, otherScreenHasFocus, false);
		}

		public override void HandleInput(InputState input)
		{
			if (this.IsActive)
				m_uiControlManager.HandleInput(input);

			base.HandleInput(input);
		}
		#endregion
	}
}
