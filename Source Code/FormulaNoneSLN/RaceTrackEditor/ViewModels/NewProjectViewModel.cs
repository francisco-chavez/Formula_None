using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unv.RaceTrackEditor.ViewModels
{
	public class NewProjectViewModel
		: ViewModelBase
	{
		#region Properties
		public string ProjectName
		{
			get { return mn_projectName; }
			set
			{
				if (mn_projectName != value)
				{
					mn_projectName = value;
					OnPropertyChanged("ProjectName");
				}
			}
		}
		private string mn_projectName;

		public string ProjectLocation
		{
			get { return mn_projectLocation; }
			set
			{
				if (mn_projectLocation != value)
				{
					mn_projectLocation = value;
					OnPropertyChanged("ProjectLocation");
				}
			}
		}
		private string mn_projectLocation;

		public string RaceTrackImagePath
		{
			get { return mn_raceTrackImagePath; }
			set
			{
				if (mn_raceTrackImagePath != value)
				{
					mn_raceTrackImagePath = value;
					OnPropertyChanged("RaceTrackImagePath");
				}
			}
		}
		private string mn_raceTrackImagePath;
		#endregion


		#region Constructors
		public NewProjectViewModel()
			: base("Create New Race Track Project")
		{
		}
		#endregion
	}
}
