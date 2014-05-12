using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Unv.RaceTrackEditor.Core.Models;


namespace Unv.RaceTrackEditor.Core
{
	public interface IProjectFileReader
	{
		string FileExtension		{ get; }
		string ExtensionDescription { get; }

		ProjectModel OpenProject(string filePath);
	}
}
