using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;


namespace Unv.RaceEngineLib
{
	public class CarTire
	{
		#region Properties
		public Vector2	CarCenterOffset		{ get; set; }
		public float	CarRotationOffset	{ get; set; }
		public float	RotationSpeed		{ get; set; }


		public float Densitity
		{
			get { return mn_density; }
			set 
			{
				if (mn_density != value)
				{
					mn_density = value;
					SetMassAndInertia();
				}
			}
		}
		private float mn_density = 0f;

		public float Radius
		{
			get { return mn_radius; }
			set
			{
				if (mn_radius != value)
				{
					mn_radius = value;
					SetMassAndInertia();
				}
			}
		}
		private float mn_radius = 0f;

		public float RubberThickness
		{
			get { return mn_rubberThickness; }
			set
			{
				if (mn_rubberThickness != value)
				{
					mn_rubberThickness = value;
					SetMassAndInertia();
				}
			}
		}
		private float mn_rubberThickness = 0f;

		public float Width
		{
			get { return mn_width; }
			set
			{
				if (mn_width != value)
				{
					mn_width = value;
					SetMassAndInertia();
				}
			}
		}
		private float mn_width = 0f;


		public float Mass		{ get; private set; }
		public float InertiaYY	{ get; private set; }
		public float InertiaZZ	{ get; private set; }
		#endregion


		#region Methods
		private float FindCylinderMass(float radius, float width, float density)
		{
			float sideWallArea = MathHelper.Pi * radius * radius;
			float volumn = sideWallArea * width;
			float mass = volumn * density;

			return mass;
		}

		private float FindCylinderInertiaYY(float radius, float width, float mass)
		{
			return 0.5f * mass * radius * radius;
		}

		private float FindCylinderInertiaZZ(float radius, float width, float mass)
		{
			return (0.25f * mass * radius * radius) + ((1f / 12f) * mass * width * width);
		}

		private void SetMassAndInertia()
		{
			if (Radius <= 0f || Width <= 0f || Densitity <= 0f || RubberThickness <= 0f)
				return;

			float outterMass		= FindCylinderMass(Radius, Width, Densitity);
			float innnerMass		= FindCylinderMass(Radius - RubberThickness, Width, Densitity);

			float inertiaYYOutter	= FindCylinderInertiaYY(Radius, Width, outterMass);
			float inertiaYYInner	= FindCylinderInertiaYY(Radius - RubberThickness, Width, innnerMass);

			float inertiaZZOuter	= FindCylinderInertiaZZ(Radius, Width, outterMass);
			float inertiaZZInner	= FindCylinderInertiaZZ(Radius - RubberThickness, Width, innnerMass);


			Mass		= outterMass - innnerMass;
			InertiaYY	= inertiaYYOutter - inertiaYYInner;
			InertiaZZ	= inertiaZZOuter - inertiaZZInner;
		}
		#endregion
	}
}
