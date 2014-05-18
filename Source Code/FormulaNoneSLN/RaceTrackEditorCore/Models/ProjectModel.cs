using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Unv.RaceTrackEditor.Core.Models
{
	public class ProjectModel
		: IModel
	{
		#region Properties
		public virtual string			ProjectFilePath { get; set; }
		public virtual IProjectManager	ProjectManager	{ get; set; }

		public virtual RaceTrackModel	RaceTrackModel	{ get; set; }
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
		protected virtual void LoadRaceTrackModel() { }
		#endregion
	}
}
