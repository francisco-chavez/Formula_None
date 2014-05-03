using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Unv.FormulaNone.Controls
{
	public class FilmStripSelector
		: ControlBase
	{
		#region Events
		public event SelectionChangedHandler SelectionChanged;
		#endregion


		#region Attributes
		private List<ListItem> m_contentItems;
		#endregion


		#region Properties
		public int		Margins					{ get; set; }
		public int		Paddings				{ get; set; }
		public int		BorderThickness			{ get; set; }
		public Color	ItemBackground			{ get; set; }

		public Color	BorderColor				{ get; set; }
		public Color	SelectedBorderColor		{ get; set; }
		public int		ItemWidth				{ get; set; }
		public int		ItemHeight				{ get; set; }

		public override int MaxHeight
		{
			get { return ItemHeight; }
			set { ItemHeight = value; }
		}

		public int SelectedIndex
		{
			get { return m_selectedIndex; }
			set
			{
				int startingValue = m_selectedIndex;

				m_selectedIndex = Math.Max(value, -1);
				m_selectedIndex = Math.Min(m_selectedIndex, m_contentItems.Count - 1);

				if (startingValue != m_selectedIndex)
					OnSelectionChanged(startingValue, m_selectedIndex);
			}
		}
		private int m_selectedIndex = -1;

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
			m_contentItems	= new List<ListItem>();

			Margins				= 4;
			Paddings			= 4;
			BorderThickness		= 8;
			ItemBackground		= Color.DarkGreen;

			BorderColor			= Color.Green;
			SelectedBorderColor = Color.Gold;
			ItemWidth			= 100;
		}
		#endregion


		#region Methods
		public override void Draw(SpriteBatch spriteBatch, Rectangle drawArea)
		{
			float colorAlpha		= this.ControlManager.Screen.TransitionAlpha;
			Texture2D blank			= RaceGame.Instance.WhiteTexture2D;
			Vector2 position		= drawArea.Position();

			Color borderColor		= new Color(BorderColor.ToVector3() * colorAlpha);
			Color displayAreaColor	= new Color(ItemBackground.ToVector3() * colorAlpha);
			Color whiteLighting		= new Color(colorAlpha, colorAlpha, colorAlpha);
			
			foreach(var container in m_contentItems)
			{
				Rectangle borderRec = new Rectangle((int) position.X, (int) position.Y, ItemWidth, ItemHeight);
				Rectangle displayBackgroundRect = 
					new Rectangle(
						borderRec.X + BorderThickness,
						borderRec.Y + BorderThickness, 
						borderRec.Width - BorderThickness * 2,
						borderRec.Height - BorderThickness * 2);
				Rectangle displayRect =
					new Rectangle(
						displayBackgroundRect.X + Paddings,
						displayBackgroundRect.Y + Paddings,
						displayBackgroundRect.Width - Paddings * 2,
						displayBackgroundRect.Height - Paddings * 2);

				spriteBatch.Draw(blank, borderRec, borderColor);
				spriteBatch.Draw(blank, displayBackgroundRect, displayAreaColor);
				container.Draw(spriteBatch, displayRect, whiteLighting);

				position.X += ItemWidth + Margins;
			}
		}

		public override void Update(GameTime gameTime)
		{
			//throw new NotImplementedException();
		}

		public override void HandleInput(InputState input)
		{
			//throw new NotImplementedException();
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

			m_contentItems.Add(container);
		}

		public override void Clear()
		{
			foreach (var containter in m_contentItems)
				containter.Clear();

			m_contentItems.Clear();
		}

		public void OnSelectionChanged(int oldIndex, int newIndex)
		{
			if (SelectionChanged != null)
			{
				string oldValue = oldIndex != -1 ? m_contentItems[oldIndex].Value : null;
				string newValue = newIndex != -1 ? m_contentItems[newIndex].Value : null;

				SelectionChanged(this, new SelectionChangedEventArgs(oldValue, newValue));
			}
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

			public abstract void Draw(SpriteBatch spriteBatch, Rectangle drawArea, Color lighting);
			public abstract void Clear();
		}

		private class TextListItem
			: ListItem
		{
			public string DisplayItem { get; set; }

			public override void Draw(SpriteBatch spriteBatch, Rectangle drawArea, Color lighting)
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


			public override void Draw(SpriteBatch spriteBatch, Rectangle drawArea, Color lighting)
			{
				Vector2 center = drawArea.Position() + drawArea.Size() / 2;

				spriteBatch.Draw(DisplayItem, center, null, lighting, DisplayRotation, new Vector2(DisplayItem.Width, DisplayItem.Height) / 2, 1f, SpriteEffects.None, 0f);
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
