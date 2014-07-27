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
		private PhysicsEngine	m_physicsEngine;
		private List<Tire>		m_obstacles;
		private List<RaceCar>	m_cars;

		private float			m_currentTimeStep = 0f;
		#endregion


		#region Properties
		public Tire[]		Obstacles	{ get { return m_obstacles.ToArray(); } }
		public RaceCar[]	Car			{ get { return m_cars.ToArray(); } }
		#endregion


		#region Constructors
		public RaceEngine()
		{
			m_physicsEngine = new PhysicsEngine();
			m_obstacles		= new List<Tire>(100);
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

				Tire tire = new Tire(body);
				m_obstacles.Add(tire);
			}
		}

		public void ReplaceObstacles(ObstacleMap obstacleMap)
		{
			ClearObstacles();

			AddObstacles(obstacleMap);
		}

		public void ClearObstacles()
		{
			m_physicsEngine.ClearImmobileBodies();
			m_obstacles.Clear();
		}


		public void AddCar(RaceCar car)
		{
			if (car == null)
				throw new ArgumentNullException();

			car.Body = m_physicsEngine.AddMobileBody(RaceCar.CAR_SHAPE, 3f, Vector2.Zero, "Metal");

			m_cars.Add(car);
		}


		public void StepTime(float timeMS)
		{
			// Update the car's input based on what is seen before
			// we start to make changes to their enviroment.
			foreach (var car in m_cars)
				car.CarControls.Update(timeMS);

			m_currentTimeStep += timeMS;
			m_physicsEngine.Step(timeMS);
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
