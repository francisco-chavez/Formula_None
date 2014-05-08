using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Unv.RaceEngineLib.Physics
{
	public class Material
	{
		public float Density		{ get; set; }
		public float Restitution	{ get; set; }

		public Material Copy()
		{
			Material copy = new Material()
			{
				Density		= this.Density,
				Restitution = this.Restitution
			};

			return copy;
		}
	}
}
