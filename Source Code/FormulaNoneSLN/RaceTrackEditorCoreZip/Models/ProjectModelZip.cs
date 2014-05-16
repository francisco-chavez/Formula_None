using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Unv.RaceTrackEditor.Core.Models;

namespace Unv.RaceTrackEditor.Core.Zip.Models
{
	public class ProjectModelZip
		: ProjectModel
	{
		#region Properties
		public override RaceTrackModel RaceTrackModel
		{
			set
			{
				if (!(value is RaceTrackModelZip))
					throw new Exception();

				base.RaceTrackModel = value;
			}
		}
		#endregion


		#region Constructors
		public ProjectModelZip()
			: this(null) { }

		public ProjectModelZip(ProjectManagerZip projectManager)
			: base(projectManager)
		{
		}
		#endregion


		#region Methods
		protected override void LoadRaceTrackModel()
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
