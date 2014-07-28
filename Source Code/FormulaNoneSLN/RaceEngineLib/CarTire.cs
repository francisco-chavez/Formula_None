using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;


namespace Unv.RaceEngineLib
{
	public class CarTire
	{
		public Vector2	CarCenterOffset { get; set; }
		public float	CarRotationOffset { get; set; }
		public float	Densitity		{ get; set; }
		public float	Radius			{ get; set; }
		public float	RuberThickness	{ get; set; }
		public float	Width			{ get; set; }
	}
}
