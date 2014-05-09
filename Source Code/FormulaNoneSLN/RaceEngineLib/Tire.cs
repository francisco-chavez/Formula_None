using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Unv.RaceEngineLib.Physics;


namespace Unv.RaceEngineLib
{
	public class Tire
	{
		private Body m_body;


		public Vector2 Position { get { return m_body.Position; } }


		public Tire(Body body)
		{
			m_body = body;
		}
	}
}
