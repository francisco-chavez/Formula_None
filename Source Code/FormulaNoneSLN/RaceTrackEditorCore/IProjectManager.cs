using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Unv.RaceTrackEditor.Core.Models;


namespace Unv.RaceTrackEditor.Core
{
	public interface IProjectManager
	{
		IProjectFileReader ProjectReader { get; set; }
		IProjectFileWriter ProjectWriter { get; set; }

		ProjectModel CurrentProject { get; set; }

		NewProjectInfoModel GetNewProjectInfo();
		ProjectModel CreateNewProject(NewProjectInfoModel projectInfo);
		void SaveCurrentProject();
	}
}
