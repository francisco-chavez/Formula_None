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
		#region Attributes
		#endregion


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

		public BitmapImage RaceTrackImage
		{
			get { return mn_raceTrackImage; }
			private set
			{
				if (mn_raceTrackImage != value)
				{
					mn_raceTrackImage = value;
					OnPropertyChanged("RaceTrackImage");
				}
			}
		}
		private BitmapImage mn_raceTrackImage;
		#endregion


		#region Constructors
		public ProjectViewModel()
			: this(null)
		{ }

		public ProjectViewModel(ProjectModel data)
		{
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
		#endregion
	}
}
