using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Unv.RaceEngineLib;


namespace Unv.FormulaNone.Sprites
{
	public class CarSprite
	{
		public Texture2D	Image			{ get; set; }
		public RaceCar		Data			{ get; set; }
		public float		RotationOffset	{ get; set; }
		public Vector2		ImageOffset		{ get; set; }


		public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Color lighting)
		{
			var drawPosition = ImageOffset + Data.Position;
			Rectangle rect = new Rectangle(
				(int) drawPosition.X,
				(int) drawPosition.Y,
				Image.Width,
				Image.Height);

			spriteBatch.Draw(
				Image, 
				rect, 
				null, 
				lighting, 
				RotationOffset + Data.Rotation, 
				-Data.CenterOfMassShift, 
				SpriteEffects.None, 0f);
		}
	}
}
