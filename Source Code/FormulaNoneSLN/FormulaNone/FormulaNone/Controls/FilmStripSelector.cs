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
		#region Attributes
		private List<ListItem> m_contentItems;
		private ControlManager m_manager;
		#endregion


		#region Properties
		public int SelectedIndex { get; set; }
		public string SelectedValue { get; set; }
		#endregion


		#region Initialization
		public FilmStripSelector(ControlManager manager)
		{
			m_manager		= manager;
			m_contentItems	= new List<ListItem>();
		}
		#endregion


		#region Methods
		public override void Clear()
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Helper Classes
		private abstract class ListItem
		{
			public string Value		{ get; set; }

			public abstract void Draw();
		}

		private class ImageListItem
			: ListItem
		{
			public Texture2D DisplayItem { get; set; }

			public override void Draw()
			{
				throw new NotImplementedException();
			}
		}

		private class TextListItem
			: ListItem
		{
			public string DisplayItem { get; set; }

			public override void Draw()
			{
				throw new NotImplementedException();
			}
		}
		#endregion
	}
}
