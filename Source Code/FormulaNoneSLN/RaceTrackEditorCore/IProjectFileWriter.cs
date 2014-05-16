using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

using Unv.RaceTrackEditor.Core.Models;


namespace Unv.RaceTrackEditor.Core
{
	public interface IProjectFileWriter
	{
		string FileExtension		{ get; }
		string ExtensionDescription { get; }

		ProjectModel CreateNewProject(NewProjectInfoModel projectInformation);
		void SaveProject(ProjectModel projectModel);

		BitmapImage SaveRaceTrackImage(ProjectModel projectModel, string imagePath);
	}
}
