using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

using Unv.RaceTrackEditor.Core;
using Unv.RaceTrackEditor.Core.Zip;


namespace Unv.RaceTrackEditor
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App 
		: Application
	{
		public static IProjectManager ProjectManager { get; set; }

		static App()
		{
			ProjectManager = new ProjectManagerZip(App.Current);
		}
	}
}
