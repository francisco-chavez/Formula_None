using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Unv.FormulaNone
{
	public sealed class ControlManager
	{
		#region Attributes
		private List<ControlBase> m_controls;
		#endregion


		#region Properties
		public Rectangle	DrawArea	{ get; set; }
		public GameScreen	Screen		{ get; private set; }
		
		public ControlBase CurrentControl
		{
			get
			{
				if (SelectedIndex == -1)
					return null;

				return m_controls[SelectedIndex];
			}
		}

		public int SelectedIndex
		{
			get { return m_selectedIndex; }
			set
			{
				int oldIndex = m_selectedIndex;

				m_selectedIndex = Math.Max(0, value);
				m_selectedIndex = Math.Min(m_selectedIndex, m_controls.Count - 1);

				if (oldIndex != m_selectedIndex)
				{
					m_controls[m_selectedIndex].IsCurrentControl = true;
					if (oldIndex != -1)
						m_controls[oldIndex].IsCurrentControl = false;
				}
			}
		}
		private int m_selectedIndex = -1;
		#endregion


		#region Initialization
		public ControlManager(GameScreen owningScreen)
		{
			Screen = owningScreen;

			m_controls = new List<ControlBase>();

			DrawArea = new Rectangle(50, 50, 200, 100);
		}
		#endregion


		#region Methods
		public void Draw(SpriteBatch spriteBatch)
		{
			var drawArea = DrawArea;

			foreach (var control in m_controls)
			{
				control.Draw(spriteBatch, drawArea);

				drawArea.Y += control.MaxHeight + 4;
				drawArea.Height -= control.MaxHeight + 4;
			}
		}

		public void Update(GameTime gameTime)
		{
			foreach (var control in m_controls)
				control.Update(gameTime);
		}

		public void HandleInput(InputState input)
		{
			PlayerIndex? controllingPlayer = Screen.ControllingPlayer;
			PlayerIndex playerIndex;

			if (input.IsNewButtonPress(Buttons.DPadUp, controllingPlayer, out playerIndex) ||
				input.IsNewButtonPress(Buttons.LeftThumbstickUp, controllingPlayer, out playerIndex))
			{
				SelectedIndex--;
			}
			else if (input.IsNewButtonPress(Buttons.DPadDown, controllingPlayer, out playerIndex) ||
					 input.IsNewButtonPress(Buttons.LeftThumbstickDown, controllingPlayer, out playerIndex))
			{
				SelectedIndex++;
			}
			else
			{
				for (int i = 0; i < m_controls.Count; i++)
				{
					if (i == SelectedIndex)
						m_controls[i].HandleInput(input);
				}
			}
		}

		public void AddControl(ControlBase control)
		{
			if (control.ControlManager != this)
				throw new ArgumentException("The given control is owned by a different Control Manager.");

			m_controls.Add(control);
			if (SelectedIndex == -1)
				SelectedIndex = 0;
		}

		public void Clear()
		{
			foreach (var control in m_controls)
				control.Clear();

			m_controls.Clear();
		}
		#endregion
	}
}
