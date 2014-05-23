using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;

using Microsoft.Win32;

using Unv.RaceTrackEditor.Core.Models;


namespace Unv.RaceTrackEditor.ViewModels
{
	public class ProjectViewModel
		: ViewModelBase
	{
		#region Properties
		public ProjectModel ProjectModel
		{
			get { return mn_projectModel; }
			set
			{
				if (mn_projectModel != value)
				{
					mn_projectModel = value;
					OnPropertyChanged("ProjectModel");

					LoadProjectModelData();
				}
			}
		}
		private ProjectModel mn_projectModel;

		public ObstacleDataViewModel ObstacleDataViewModel
		{
			get { return mn_obstacleDataViewModel; }
			private set
			{
				if (value == null)
					throw new ArgumentNullException();

				if (mn_obstacleDataViewModel != value)
				{
					mn_obstacleDataViewModel = value;
					OnPropertyChanged("ObstacleDataViewModel");

					if (value != null)
						value.RaceTrackImage = this.RaceTrackImage;
				}
			}
		}
		private ObstacleDataViewModel mn_obstacleDataViewModel;

		public BitmapImage RaceTrackImage
		{
			get { return mn_raceTrackImage; }
			private set
			{
				if (mn_raceTrackImage != value)
				{
					mn_raceTrackImage = value;
					OnPropertyChanged("RaceTrackImage");

					ObstacleDataViewModel.RaceTrackImage = value;

					RaceTrackWidth  = (value == null) ? 0 : value.PixelWidth;
					RaceTrackHeight = (value == null) ? 0 : value.PixelHeight;
				}
			}
		}
		private BitmapImage mn_raceTrackImage;

		public int RaceTrackWidth
		{
			get { return mn_raceTrackWidth; }
			set
			{
				if (mn_raceTrackWidth != value)
				{
					mn_raceTrackWidth = value;
					OnPropertyChanged("RaceTrackWidth");
				}
			}
		}
		private int mn_raceTrackWidth;

		public int RaceTrackHeight
		{
			get { return mn_raceTrackHeight; }
			set
			{
				if (mn_raceTrackHeight != value)
				{
					mn_raceTrackHeight = value;
					OnPropertyChanged("RaceTrackHeight");
				}
			}
		}
		private int mn_raceTrackHeight;
		#endregion


		#region Constructors
		public ProjectViewModel()
			: this(null)
		{ }

		public ProjectViewModel(ProjectModel data)
		{
			this.ObstacleDataViewModel = new ObstacleDataViewModel();
			this.ProjectModel = data;
		}
		#endregion


		#region Methods
		private void LoadProjectModelData()
		{
			RaceTrackImage	= null;
			DisplayTitle	= null;

			if (ProjectModel == null)
				return;

			this.DisplayTitle = Path.GetFileNameWithoutExtension(ProjectModel.ProjectFilePath);

			if (ProjectModel.RaceTrackModel != null)
			{
				this.RaceTrackImage = ProjectModel.RaceTrackModel.RaceTrackImage;
				this.ObstacleDataViewModel.Model = ProjectModel.RaceTrackModel.Obstacles;
			}
		}

		public void SelectRaceTrackImage()
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Image (*.png)|*.png";
			dlg.Multiselect = false;
			dlg.Title = "Select Race Track Image";

			bool keepGoing = dlg.ShowDialog(App.Current.MainWindow) == true;
			if (!keepGoing)
				return;

			string imagePath = dlg.FileName;

			App.ProjectManager.SetRaceTrackImage(imagePath);

			RaceTrackImage = ProjectModel.RaceTrackModel.RaceTrackImage;
		}

		public override void RebuildModel()
		{
			this.ObstacleDataViewModel.RebuildModel();
			base.RebuildModel();
		}
		#endregion
	}
}
