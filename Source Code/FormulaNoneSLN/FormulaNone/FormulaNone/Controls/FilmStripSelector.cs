using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace Unv.FormulaNone.Controls
{
	public class FilmStripSelector
		: ControlBase
	{
		#region Events
		public	event SelectionChangedHandler SelectionChanged;
		#endregion


		#region Attributes
		private	List<ListItem>	m_contentItems;
		private int				m_msPerThisCycle = 0;
		#endregion


		#region Properties
		public	int			Margin					{ get; set; }
		public	int			Padding					{ get; set; }
		public	int			BorderThickness			{ get; set; }
		public	Color		ItemBackground			{ get; set; }

		public	Color		BorderColor				{ get; set; }
		public	Color		SelectedBorderColor		{ get; set; }
		public	int			ItemWidth				{ get; set; }
		public	int			ItemHeight				{ get; set; }

		public	int			MSSecondsPerGlowCycle	{ get; set; }
		public	Texture2D	ShiftLeftIndicator		{ get; set; }

		public	override int MaxHeight
		{
			get { return ItemHeight; }
			set { ItemHeight = value; }
		}

		public	int SelectedIndex
		{
			get { return m_selectedIndex; }
			set
			{
				if (m_selectedIndex == value)
					return;

				if (m_contentItems.Count == 0)
					return;

				m_msPerThisCycle = 0;
				int startingValue = m_selectedIndex;

				m_selectedIndex = Math.Max(value, MustHaveItemSelected ? 0 : -1);
				m_selectedIndex = Math.Min(m_selectedIndex, m_contentItems.Count - 1);

				if (startingValue != m_selectedIndex)
					OnSelectionChanged();
			}
		}
		private int m_selectedIndex = -1;

		public	bool MustHaveItemSelected
		{
			get { return m_mustHaveItemSelected; }
			set
			{
				if (m_mustHaveItemSelected == value)
					return;

				m_mustHaveItemSelected = value;

				if (m_contentItems.Count == 0)
					return;

				SelectedIndex = Math.Max(SelectedIndex, value ? 0 : -1);
			}
		}
		private bool m_mustHaveItemSelected = false;

		public string SelectedValue
		{
			get
			{
				return SelectedIndex != -1 ? m_contentItems[SelectedIndex].Value : null;
			}
		}
		#endregion


		#region Initialization
		public FilmStripSelector(ControlManager manager)
			: base(manager)
		{
			m_contentItems		= new List<ListItem>();

			Margin					= 5;
			Padding					= 5;
			BorderThickness			= 10;
			ItemBackground			= Color.DarkGreen;

			BorderColor				= Color.Green;
			SelectedBorderColor		= Color.Gold;
			ItemWidth				= 100;
			ItemHeight				= 100;

			MSSecondsPerGlowCycle	= 1350;
		}
		#endregion


		#region Methods
		public override void Draw(SpriteBatch spriteBatch, Rectangle drawArea)
		{
			const int margingMult = 3;

			Vector2		position			= drawArea.Position();

			Vector2		leftPointerPosition = position;
						leftPointerPosition.Y += (ItemHeight - ShiftLeftIndicator.Height) / 2f;
			Vector2		rightPointerPosition = leftPointerPosition;

						rightPointerPosition.X += drawArea.Width - ShiftLeftIndicator.Width;

			
			spriteBatch.Draw(ShiftLeftIndicator, leftPointerPosition, SelectedBorderColor);
			spriteBatch.Draw(ShiftLeftIndicator, rightPointerPosition, null, SelectedBorderColor, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);


			Rectangle	safeDrawArea		= drawArea;
						safeDrawArea.Width	-= ShiftLeftIndicator.Width * 2;
						safeDrawArea.Width	-= Margin * margingMult * 2;

						position.X			+= ShiftLeftIndicator.Width + Margin * margingMult;
			float		colorAlpha			= this.ControlManager.Screen.TransitionAlpha;
			Texture2D	blank				= RaceGame.Instance.WhiteTexture2D;

			Color		borderColor;
			Color		displayAreaColor	= new Color(ItemBackground.ToVector3() * colorAlpha);
			Color		whiteLighting		= new Color(colorAlpha, colorAlpha, colorAlpha);

			
			for (int i = 0; i < m_contentItems.Count; i++)
			{
				var container = m_contentItems[i];

				if (SelectedIndex == i)
				{
					float x = MathHelper.TwoPi / MSSecondsPerGlowCycle;
					float tx = m_msPerThisCycle * x;

					float glowPercent =  ((float) Math.Cos(tx) + 1f) / 2f;

					Vector3 colorV = 
						(SelectedBorderColor.ToVector3() * glowPercent) + 
						(BorderColor.ToVector3() * (1f - glowPercent));

					borderColor = new Color(colorV * colorAlpha);
				}
				else
				{
					borderColor = new Color(BorderColor.ToVector3() * colorAlpha);
				}

				Rectangle borderRec = new Rectangle((int) position.X, (int) position.Y, ItemWidth, ItemHeight);
				Rectangle displayBackgroundRect = 
					new Rectangle(
						borderRec.X + BorderThickness,
						borderRec.Y + BorderThickness, 
						borderRec.Width - BorderThickness * 2,
						borderRec.Height - BorderThickness * 2);
				Rectangle displayRect =
					new Rectangle(
						displayBackgroundRect.X + Padding,
						displayBackgroundRect.Y + Padding,
						displayBackgroundRect.Width - Padding * 2,
						displayBackgroundRect.Height - Padding * 2);

				spriteBatch.Draw(blank, borderRec, borderColor);
				spriteBatch.Draw(blank, displayBackgroundRect, displayAreaColor);
				container.DrawDisplayItem(spriteBatch, displayRect, whiteLighting);

				position.X += ItemWidth + Margin;
			}
		}

		public override void Update(GameTime gameTime)
		{
			m_msPerThisCycle += gameTime.ElapsedGameTime.Milliseconds;

			if (m_msPerThisCycle > MSSecondsPerGlowCycle)
				m_msPerThisCycle -= MSSecondsPerGlowCycle;
		}

		public override void HandleInput(InputState input)
		{
			PlayerIndex? controllingPlayer = this.ControlManager.Screen.ControllingPlayer;
			PlayerIndex playerIndex;

			if (input.IsNewButtonPress(Buttons.LeftThumbstickLeft, controllingPlayer, out playerIndex) ||
				input.IsNewButtonPress(Buttons.DPadLeft, controllingPlayer, out playerIndex))
				SelectedIndex--;
			else if (input.IsNewButtonPress(Buttons.LeftThumbstickRight, controllingPlayer, out playerIndex) ||
					 input.IsNewButtonPress(Buttons.DPadRight, controllingPlayer, out playerIndex))
				SelectedIndex++;
		}

		public void AddItem(string value, Texture2D display, float displayRotation)
		{
			if (string.IsNullOrWhiteSpace(value) || display == null)
				throw new ArgumentNullException();

			var container = new ImageListItem()
			{
				DisplayItem		= display,
				Value			= value,
				DisplayRotation = displayRotation
			};

			if (MustHaveItemSelected && SelectedIndex == -1)
				SelectedIndex = 0;

			m_contentItems.Add(container);
		}

		public void AddItem(string value, string display)
		{
			if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(display))
				throw new ArgumentNullException();

			var container = new TextListItem()
			{
				DisplayItem = display,
				Value = value
			};

			if (MustHaveItemSelected && SelectedIndex == -1)
				SelectedIndex = 0;

			m_contentItems.Add(container);
		}

		public override void Clear()
		{
			foreach (var containter in m_contentItems)
				containter.Clear();

			m_contentItems.Clear();
			int oldIndex = m_selectedIndex;
			m_selectedIndex = -1;
			if (oldIndex != m_selectedIndex)
				OnSelectionChanged();
		}

		public void OnSelectionChanged()
		{
			if (SelectionChanged != null)
				SelectionChanged(this, new SelectionChangedEventArgs());
		}
		#endregion


		#region Helper Classes
		/// 
		/// I know, I could have used generics to get rid of some of the redundent 
		/// code, but generics has some limitiations I don't feel like working 
		/// around. If I had used generics, I would have still needed a non-generic 
		/// base container class for the generic container class. Creating a 
		/// List&lt;Container&lt;Object&gt;&gt; of type object wouldn't have worked 
		/// unless I did a hard caste to Container&lt;Object&gt;, to add it to the list.
		/// I don't know if the caste would have worked. Using a non-generic base container
		/// would have let me create a list of items of the base type, but that's just
		/// the first thing to get around. I would also have made the entire film strip
		/// generic and get round this, but that still wouldn't solve the next issue.
		/// A String and a Texture2D don't have a common base class (or shared interface) 
		/// that would let them be drawn with the same command, and they don't even use 
		/// the same command to get drawn. I could get around this by using a delegate
		/// to define a commom method signiture and have a seperate class provide the
		/// correct draw method based on the display type. At this point, we're just
		/// throwing the reason for generics out the window.
		/// 
		/// I could also create a base selector class, and have a selector for text
		/// and a selector for images inherit from it. But, we only have two types
		/// to select from, there there's no point to add that much extra clutter for
		/// just two selection types. If I had three or more selection types, then this
		/// would be worth it, but I don't have three selection types. As it stands,
		/// it's just simpiler have a single, non-generic selector.
		/// 
		/// -FCT
		///

		private abstract class ListItem
		{
			public string Value	{ get; set; }

			public abstract void DrawDisplayItem(SpriteBatch spriteBatch, Rectangle drawArea, Color lighting);
			public abstract void Clear();
		}

		private class TextListItem
			: ListItem
		{
			public string DisplayItem { get; set; }

			public override void DrawDisplayItem(SpriteBatch spriteBatch, Rectangle drawArea, Color lighting)
			{
				throw new NotImplementedException();
			}

			public override void Clear()
			{
				throw new NotImplementedException();
			}
		}

		private class ImageListItem
			: ListItem
		{
			public Texture2D	DisplayItem		{ get; set; }
			public float		DisplayRotation { get; set; }


			public override void DrawDisplayItem(SpriteBatch spriteBatch, Rectangle drawArea, Color lighting)
			{
				Vector2 center = drawArea.Position() + drawArea.Size() / 2;

				var drawSize = drawArea.Size();
				var itemSize = DisplayItem.Size();
				float scaleX = drawSize.X / itemSize.X;
				float scaleY = drawSize.Y / itemSize.Y;

				// We want the image to be as large as posible, while keeping it 
				// within the draw area. So, we start with the larger of the two 
				// scale/aspect ratios, and if it turns out to be to large, we 
				// use the smaller one.
				float scaleToUse = Math.Max(scaleX, scaleY);
				if (itemSize.X * scaleToUse > drawSize.X || itemSize.Y * scaleToUse > drawSize.Y)
					scaleToUse = Math.Min(scaleX, scaleY);

				spriteBatch.Draw(
					DisplayItem, 
					center, 
					null, 
					lighting, 
					DisplayRotation, 
					itemSize / 2, 
					scaleToUse, 
					SpriteEffects.None, 
					0f);
			}

			public override void Clear()
			{
				this.DisplayItem	= null;
				this.Value			= null;
			}
		}
		#endregion
	}
}
