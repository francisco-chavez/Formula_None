using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Unv.FormulaNone
{
	public abstract class ControlBase
	{
		protected internal ControlManager ControlManager { get; private set; }

		public ControlBase(ControlManager controlManager)
		{
			if (controlManager == null)
				throw new ArgumentNullException("All controls require a contorl manager.");

			ControlManager = controlManager;
		}

		public abstract void Draw(SpriteBatch spriteBatch, Rectangle drawArea);
		public abstract void Update(GameTime gameTime);
		public abstract void HandleInput(InputState input);
		public abstract void Clear();
	}
}
