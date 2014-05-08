using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Unv.FormulaNone
{
	public abstract class ControlBase
	{
		public virtual		int				MaxHeight			{ get; set; }
		protected internal	ControlManager	ControlManager		{ get; set; }
		public virtual		bool			IsCurrentControl	{ get; set; }


		public ControlBase(ControlManager controlManager)
		{
			if (controlManager == null)
				throw new ArgumentNullException("All controls require a contorl manager.");

			ControlManager	= controlManager;
			MaxHeight		= 150;
		}


		public abstract void Draw(SpriteBatch spriteBatch, Rectangle drawArea);
		public abstract void Update(GameTime gameTime);
		public abstract void HandleInput(InputState input);
		public abstract void Clear();
	}
}
