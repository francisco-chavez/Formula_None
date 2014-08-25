using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using Unv.RaceEngineLib.Physics;
using Unv.RaceEngineLib.Physics.Shapes;
using Unv.RaceEngineLib.Storage;


namespace Unv.RaceEngineLib
{
	public class RaceEngine
	{
		#region Attributes
		private PhysicsEngine		m_physicsEngine;
		private CollisionEngine		_collisionEngine;
		private List<TireObstacle>		m_obstacles;
		private List<RaceCar>	m_cars;

		/// <summary>
		/// Some of the things that happen to the cars are nothing more than
		/// physics going about their way. But, the cars have breaks, tires,
		/// and engines, which are abble to apply forces of their own to the
		/// car. When the time comes to calculate these additional forces,
		/// we'll need to know the amount of time that has passed in order
		/// to make those calculations.
		/// </summary>
		private float			m_currentTimeStep = 0f;
		#endregion


		#region Properties
		public TireObstacle[]	Obstacles	{ get { return m_obstacles.ToArray(); } }
		public RaceCar[]		Car			{ get { return m_cars.ToArray(); } }
		#endregion


		#region Constructors
		public RaceEngine()
		{
			_collisionEngine = new CollisionEngine(4, 250);
			m_physicsEngine = new PhysicsEngine();
			m_obstacles		= new List<TireObstacle>(100);
			m_cars			= new List<RaceCar>(4);

			m_physicsEngine.UpdateForcesEvent += PhysicsEngine_UpdateForcesEvent;
		}
		#endregion


		#region Event Handlers
		void PhysicsEngine_UpdateForcesEvent(object sender, EventArgs e)
		{
			ApplyForces();
		}
		#endregion


		#region Methods
		public void AddObstacles(ObstacleMap obstacleMap)
		{
			if (obstacleMap == null)
				throw new ArgumentNullException();

			foreach (var obstacle in obstacleMap.Obstacles)
			{
				var shape = new Circular(obstacle.Radius);

				// Bouncy balls are made from rubber.
				Body body = m_physicsEngine.AddImmobileBody(shape, obstacle.Position, "BouncyBall");
				body.Position = obstacle.Position;

				TireObstacle tire = new TireObstacle(body);
				m_obstacles.Add(tire);
				_collisionEngine.AddImmobileBody(body);
			}
		}

		public void ReplaceObstacles(ObstacleMap obstacleMap)
		{
			ClearObstacles();

			AddObstacles(obstacleMap);
		}

		public void ClearObstacles()
		{
			_collisionEngine.ClearImmobileBodies();
			m_physicsEngine.ClearImmobileBodies();
			m_obstacles.Clear();
		}


		public void AddCar(RaceCar car)
		{
			if (car == null)
				throw new ArgumentNullException();

			car.Body = m_physicsEngine.AddMobileBody(RaceCar.CAR_SHAPE, 3f, Vector2.Zero, "Metal");

			m_cars.Add(car);
			_collisionEngine.AddMobileBody(car.Body);
		}


		public void StepTime(float timeMS)
		{
			// Update the car's input based on what is seen before
			// we start to make changes to their enviroment.
			foreach (var car in m_cars)
				car.CarControls.Update(timeMS);

			m_currentTimeStep = timeMS;
			m_physicsEngine.Step(timeMS);

			_collisionEngine.GenerateInitialCollisionData();
		}

		private void ApplyForces()
		{
			foreach (var car in m_cars)
			{
				UpdateCar(car);
			}
		}

		private void UpdateCar(RaceCar car)
		{
			float breaks	= 0f;
			float gas		= 0f;
			float steering	= 0f;
			bool  isReverse	= false;

			if (car.CarControls != null)
			{
				breaks		= car.CarControls.BreakPedalPosition;
				gas			= car.CarControls.GasPedalPosition;
				steering	= car.CarControls.SteeringPosition;
				isReverse	= car.CarControls.IsInReverse;
			}
		}
		#endregion
	}
}
