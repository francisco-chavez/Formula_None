using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Unv.FormulaNone
{
	public class ControlManager
	{
		#region Attributes
		private List<ControlBase> m_controls;
		#endregion


		#region Initialization
		public ControlManager()
		{
			m_controls = new List<ControlBase>();
		}
		#endregion


		#region Methods
		public void Draw()
		{
			throw new NotImplementedException();
		}

		public void Update(GameTime gameTime)
		{
		}

		public void HandleInput(InputState input)
		{
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
