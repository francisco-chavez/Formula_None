﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Microsoft.Win32;

using Unv.RaceTrackEditor.Core;
using Unv.RaceTrackEditor.Core.Models;


namespace Unv.RaceTrackEditor.Core.Zip
{
	public class ProjectManagerZip
		: IProjectManager
	{
		#region Properties
		public ProjectModel CurrentProject
		{
			get { return mn_currentProject; }
			set
			{
				if (mn_currentProject != null && mn_currentProject != value)
				{
					var userInput =
						MessageBox.Show(
						"Do you wish to save the current project progress before switching projects?",
						"Save Project",
						MessageBoxButton.YesNo,
						MessageBoxImage.Question);

					if (userInput == MessageBoxResult.Yes)
						SaveCurrentProject();
				}

				mn_currentProject = value;
			}
		}
		private ProjectModel mn_currentProject;

		public IProjectFileReader	ProjectReader	{ get; set; }
		public IProjectFileWriter	ProjectWriter	{ get; set; }

		public Application			Application		{ get; set; }
		#endregion


		#region Constructors
		public ProjectManagerZip()
			: this(null)
		{
		}

		public ProjectManagerZip(Application application)
		{
			var readerWriter = new ZipProjectReaderWriter(this);
			this.ProjectWriter = readerWriter;
			this.ProjectReader = readerWriter;
		}
		#endregion


		#region Methods
		public NewProjectInfoModel GetNewProjectInfo()
		{
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.CreatePrompt = false;
			dlg.Title = "Select New Project File";
			dlg.Filter = string.Format("{0} (*.{1})|*.{1}", ProjectWriter.ExtensionDescription, ProjectWriter.FileExtension);

			bool keepGoing = dlg.ShowDialog(Application.Current.MainWindow) == true;

			NewProjectInfoModel info = new NewProjectInfoModel();
			info.CreateProject = keepGoing;
			info.FilePath = dlg.FileName;

			return info;
		}

		public ProjectModel CreateNewProject(NewProjectInfoModel projectInfo)
		{
			if (!projectInfo.CreateProject)
				throw new Exception("Can not create a project that is flagged as do not create.");

			var newProject = this.ProjectWriter.CreateNewProject(projectInfo);
			CurrentProject = newProject;

			return newProject;
		}

		public void SaveCurrentProject()
		{
			if (CurrentProject == null)
				throw new InvalidOperationException("There is no project to save.");

			ProjectWriter.SaveProject(CurrentProject);
		}

		public ProjectModel OpenProject()
		{
			OpenFileDialog dlg = new OpenFileDialog();

			dlg.Filter =
				string.Format(
				"{0} (*.{1})|*.{1}",
				ProjectWriter.ExtensionDescription,
				ProjectWriter.FileExtension);

			dlg.Multiselect = false;
			dlg.Title = "Open Race Track Project";

			var keepGoing = dlg.ShowDialog(Application.Current.MainWindow) == true;

			if (!keepGoing)
				return null;

			var result = ProjectReader.OpenProject(dlg.FileName);
			CurrentProject = result;
			return result;
		}

		public void SetRaceTrackImage(string imagePath)
		{
			if (CurrentProject == null)
				throw new InvalidOperationException("There is no current project to set the race track image to.");

			ProjectWriter.SaveRaceTrackImage(CurrentProject, imagePath);
		}
		#endregion
	}
}
