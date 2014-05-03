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


		#region Properties
		public Rectangle DrawArea { get; set; }
		#endregion


		#region Initialization
		public ControlManager()
		{
			m_controls = new List<ControlBase>();

			DrawArea = new Rectangle(50, 50, 200, 100);
		}
		#endregion


		#region Methods
		public void Draw()
		{
			foreach (var control in m_controls)
				control.Draw(DrawArea);
		}

		public void Update(GameTime gameTime)
		{
			foreach (var control in m_controls)
				control.Update(gameTime);
		}

		public void HandleInput(InputState input)
		{
			foreach (var control in m_controls)
				control.HandleInput(input);
		}

		public void AddControl(ControlBase control)
		{
			if (control.ControlManager != this)
				throw new ArgumentException("The given control is owned by a different Control Manager.");

			m_controls.Add(control);
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
