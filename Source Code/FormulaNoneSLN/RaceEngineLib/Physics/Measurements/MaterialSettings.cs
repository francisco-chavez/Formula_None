using System.Collections.Generic;


namespace Unv.RaceEngineLib.Physics.Measurements
{
	public class MaterialSettings
	{
		#region Attributes
		// These material values were taken from 
		// http://gamedevelopment.tutsplus.com/tutorials/how-to-create-a-custom-2d-physics-engine-the-core-engine--gamedev-7493
		public readonly Material ROCK			= new Material() { Density = 0.6f, Restitution = 0.1f };
		public readonly Material WOOD			= new Material() { Density = 0.3f, Restitution = 0.2f };
		public readonly Material METAL			= new Material() { Density = 1.2f, Restitution = 0.05f };
		public readonly Material BOUNCY_BALL	= new Material() { Density = 0.3f, Restitution = 0.8f };

		public readonly Material SUPPER_BALL	= new Material() { Density = 0.3f, Restitution = 0.95f };
		public readonly Material PILLOW			= new Material() { Density = 0.1f, Restitution = 0.2f };
		//public readonly Material STATIC			= new Material() { Density = 0f, Restitution = 0.4f };
		#endregion


		#region Properties
		public static MaterialSettings Instance { get; private set; }

		public Dictionary<string, Material> Materials { get; private set; }
		#endregion


		#region Constructors
		private MaterialSettings()
		{
			this.Materials = new Dictionary<string, Material>();

			this.Materials.Add("Rock", ROCK.Copy());
			this.Materials.Add("Wood", WOOD.Copy());
			this.Materials.Add("Metal", METAL.Copy());
			this.Materials.Add("BouncyBall", BOUNCY_BALL.Copy());
			this.Materials.Add("SupperBall", SUPPER_BALL.Copy());
			this.Materials.Add("Pillow", PILLOW.Copy());
			//this.Materials.Add("Static", STATIC.Copy());
		}

		static MaterialSettings()
		{
			Instance = new MaterialSettings();
		}
		#endregion
	}
}
