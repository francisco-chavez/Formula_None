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
		public Body Body { get; private set; }


		public Tire(Body body)
		{
			Body = body;
		}
	}
}
