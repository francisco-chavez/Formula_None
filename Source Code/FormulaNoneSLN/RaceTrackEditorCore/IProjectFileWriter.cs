using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Unv.RaceTrackEditor.Core.Models;


namespace Unv.RaceTrackEditor.Core
{
	public interface IProjectFileWriter
	{
		string FileExtension		{ get; }
		string ExtensionDescription { get; }

		ProjectModel CreateNewProject(NewProjectInfoModel projectInformation);
	}
}
