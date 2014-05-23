using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

using Unv.RaceTrackEditor.Core.Models;


namespace Unv.RaceTrackEditor.ViewModels
{
	public class ObstacleLayerViewModel
		: SingleModelViewModel<ObstacleLayerModel>
	{
		#region Properties
		public bool IsVisable
		{
			get { return mn_isVisable; }
			set
			{
				if (mn_isVisable != value)
				{
					mn_isVisable = value;
					OnPropertyChanged("IsVisable");
				}
			}
		}
		private bool mn_isVisable;

		public ObservableCollection<ObstacleViewModel> Obstacles
		{
			get { return mn_obstacles; }
			private set
			{
				if (mn_obstacles != value)
				{
					mn_obstacles = value;
					OnPropertyChanged("Obstacles");
				}
			}
		}
		private ObservableCollection<ObstacleViewModel> mn_obstacles;
		#endregion


		#region Constructors
		public ObstacleLayerViewModel()
		{
			DisplayTitle	= string.Empty;
			IsVisable		= true;
			Obstacles		= new ObservableCollection<ObstacleViewModel>();
		}
		#endregion


		#region Methods
		public override void ClearOutModelData()
		{
			this.DisplayTitle = null;
			this.IsVisable = true;

			this.Obstacles.Clear();

			base.ClearOutModelData();
		}

		public override void LoadModelData()
		{
			this.DisplayTitle = Model.LayerName;
			this.IsVisable = Model.IsVisable;

			foreach (var obstacleModel in Model.Obstacles)
			{
				var obstacleVM = new ObstacleViewModel();
				obstacleVM.Model = obstacleModel;
				this.Obstacles.Add(obstacleVM);
			}

			base.LoadModelData();
		}

		public void AddObstacles(Point startingPoint, Point endingPoint, double multiItemDistance)
		{
			var positionDelta = endingPoint - startingPoint;
			double deltaLength = positionDelta.Length;
			if (deltaLength < multiItemDistance)
			{
				Obstacles.Add(new ObstacleViewModel() { X = startingPoint.X, Y = startingPoint.Y });
			}
			else
			{
				double obsRadius = 16;
				int radCount = (int) (deltaLength / obsRadius);
				if (radCount % 2 == 1)
					radCount--;

				int obsCount = (radCount / 2) + 1;

				var multVector = positionDelta / (obsCount - 1);
				var startingVector = new Vector(startingPoint.X, startingPoint.Y);
				for (int i = 0; i < obsCount; i++)
				{
					var location = startingVector + (multVector * i);
					ObstacleViewModel vm = new ObstacleViewModel()
					{
						X = location.X,
						Y = location.Y
					};

					Obstacles.Add(vm);
				}
			}
		}

		public override void RebuildModel()
		{
			var model = this.Model;
			if (model == null)
				model = new ObstacleLayerModel();

			model.Obstacles.Clear();
			foreach (var obstacleViewModel in this.Obstacles)
			{
				obstacleViewModel.RebuildModel();
				model.Obstacles.Add(obstacleViewModel.Model);
			}
			model.LayerName = this.DisplayTitle;
			model.IsVisable = this.IsVisable;

			this.Model = model;
		}
		#endregion
	}
}
