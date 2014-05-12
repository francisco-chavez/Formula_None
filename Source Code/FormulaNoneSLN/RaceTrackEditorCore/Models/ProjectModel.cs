using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unv.RaceTrackEditor.Core.Models
{
	public class ProjectModel
	{
		#region Properties
		public string			ProjectFilePath { get; set; }
		public IProjectManager	ProjectManager	{ get; set; }
		#endregion


		#region Constructors
		public ProjectModel()
			: this(null)
		{
		}

		public ProjectModel(IProjectManager projectManager)
		{
			ProjectManager = projectManager;
		}
		#endregion


		#region Methods
		#endregion
	}
}
