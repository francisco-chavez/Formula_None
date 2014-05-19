using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Unv.RaceTrackEditor.ViewModels;


namespace Unv.RaceTrackEditor.Views
{
	/// <summary>
	/// Interaction logic for ObstacleDataView.xaml
	/// </summary>
	public partial class ObstacleDataView 
		: UserControl
	{
		public ObstacleDataView()
		{
			InitializeComponent();
		}

		private void ListBox_SizeChanged(object sender, SizeChangedEventArgs e)
		{
		}

		private void GUI_LayerContainer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{

		}
	}
}
