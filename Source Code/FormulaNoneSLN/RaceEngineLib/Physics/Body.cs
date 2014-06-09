using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;


namespace Unv.RaceEngineLib.Physics
{
	public class Body
	{
		#region Properties
		public Shape	CollisionDetectionShape { get; set; }
		public float	Resitution			{ get; set; }


		//
		// Linear Motion Properties
		//
		public Vector2	Position	{ get; set; }
		public Vector2	Velocity	{ get; set; }

		public float Mass
		{
			get { return mn_mass; }
			set
			{
				mn_mass = value;
				MassInverse = 1f / value;
			}
		}
		private float mn_mass;

		public float MassInverse { get; private set; }


		//
		// Rotation Motion Properties
		//
		public float Rotation		{ get; set; }
		public float RotationSpeed	{ get; set; }

		public float Inertia
		{
			get { return mn_inertia; }
			set
			{
				mn_inertia = value;
				InertiaInverse = 1f / value;
			}
		}
		private float mn_inertia;

		public float InertiaInverse { get; private set; }
		#endregion


		#region Constructor
		public Body()
		{
		}
		#endregion
	}
}
