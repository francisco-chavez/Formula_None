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
		/// <summary>
		/// Gets or sets the tire's position relative to the car's center of mass.
		/// </summary>
		public Vector2 CarCenterOffset	{ get; set; }

		/// <summary>
		/// Gets or sets the amount the tire is rotated from the car's rotation. If this
		/// is zero, then the tire pushes the car in a straight direction.
		/// </summary>
		public float CarRotationOffset{ get; set; }

		/// <summary>
		/// Gets or sets the radians per second at which the tire is turning.
		/// </summary>
		public float RotationSpeed { get; set; }

		
		/// <summary>
		/// Gets or sets the density of the tire's rubber.
		/// </summary>
		public float Density
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

		/// <summary>
		/// Gets or sets the tire's radius size.
		/// </summary>
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

		/// <summary>
		/// Gets or sets the thickness of the rubber in the tire.
		/// </summary>
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

		/// <summary>
		/// Gets or sets the width of the tire.
		/// </summary>
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


		#region Constructors
		public CarTire()
		{
			CarCenterOffset		= Vector2.Zero;
			CarRotationOffset	= 0f;
			RotationSpeed		= 0f;
		}
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
			if (Radius <= 0f || Width <= 0f || Density <= 0f || RubberThickness <= 0f)
				return;

			float outterMass		= FindCylinderMass(Radius, Width, Density);
			float innnerMass		= FindCylinderMass(Radius - RubberThickness, Width, Density);

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
