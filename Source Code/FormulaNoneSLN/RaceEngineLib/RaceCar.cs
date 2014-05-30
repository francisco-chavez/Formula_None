using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Unv.RaceEngineLib.Physics;
using Unv.RaceEngineLib.Physics.Shapes;


namespace Unv.RaceEngineLib
{
	public class RaceCar
	{
		/// <summary>
		/// These points form a convex polynormal shape that is based
		/// on the images of the race cars.
		/// </summary>
		private static readonly Vector2[] BORDER_POINTS_BASE = 
			new Vector2[] 
			{ 
				new Vector2(00f, 31f), new Vector2(36f, 31f), new Vector2(47f, 20f), 
				new Vector2(48f, 12f), new Vector2(36f, 01f), new Vector2(00f, 00f)
			};

		/// <summary>
		/// All cars have the same shape, so they'll be sharing the same 
		/// shape in memory.
		/// </summary>
		public static readonly Shape CAR_SHAPE = new ConvexPolygon(BORDER_POINTS_BASE);


		public Body Body { get; internal set; }

		public RaceCar()
		{
		}
	}
}
