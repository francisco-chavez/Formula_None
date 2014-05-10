using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Unv.RaceTrackEditor.Core.Models;


namespace Unv.RaceTrackEditor.Core
{
	public interface IProjectManager
	{
		ProjectModel CurrentProject { get; set; }

		NewProjectInfoModel GetNewProjectInfo();
		void SaveCurrentProject();
	}
}
