using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Unv.RaceTrackEditor.Core.Models;


namespace Unv.RaceTrackEditor.Core
{
	public interface IProjectFileWriter
	{
		bool CanCreateNewProject(string directory, string projectName);

		ProjectModel CreateNewProject(NewProjectInfoModel projectInformation);
	}
}
