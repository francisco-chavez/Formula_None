using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Unv.RaceTrackEditor.Core.Models;


namespace Unv.RaceTrackEditor.ViewModels
{
	public class ProjectViewModel
		: ViewModelBase
	{
		#region Attributes
		private ProjectModel m_data;
		#endregion


		#region Constructors
		public ProjectViewModel()
			: this(null)
		{ }

		public ProjectViewModel(ProjectModel data)
		{
			m_data = data;
		}
		#endregion


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

		public string ParentDirectory
		{
			get { return mn_parentDirectory; }
			set
			{
				if (mn_parentDirectory != value)
				{
					mn_parentDirectory = value;
					OnPropertyChanged("ParentDirectory");
				}
			}
		}
		private string mn_parentDirectory;
		#endregion
	}
}
