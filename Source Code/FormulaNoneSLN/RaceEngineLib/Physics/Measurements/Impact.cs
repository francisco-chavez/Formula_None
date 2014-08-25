
using Microsoft.Xna.Framework;


namespace Unv.RaceEngineLib.Physics.Measurements
{
	/// <summary>
	/// This struct stores impact information that will be applied to a body.
	/// </summary>
	public struct Impact
	{
		/// <summary>
		/// This is the Body that recieved the impact. This is not to be confused with the 
		/// Body that cause the impact.
		/// </summary>
		public Body Body;

		/// <summary>
		/// This is where the Body recieved the impact. This is relative to the Body.
		/// </summary>
		public Vector2 Location;

		/// <summary>
		/// This is the velocity of the impact relative to the Body's velocity.
		/// </summary>
		public Vector2 RelativeVelocity;
	}
}
