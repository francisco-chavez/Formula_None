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

using Unv.RaceTrackEditor.Controls;
using Unv.RaceTrackEditor.ViewModels;


namespace Unv.RaceTrackEditor.Views
{
	/// <summary>
	/// Interaction logic for ObstacleView.xaml
	/// </summary>
	public partial class ObstacleView 
		: UserControl
	{
		#region Constructors
		public ObstacleView()
		{
			InitializeComponent();
		}
		#endregion


		#region Event Handlers
		private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			bool captured = false;
			if(IsMouseOver)
				captured = CaptureMouse();

			if (captured)
			{
				this.MouseMove += new MouseEventHandler(ObstacleView_MouseMove);
				this.LostMouseCapture += new MouseEventHandler(ObstacleView_LostMouseCapture);
			}
		}

		void ObstacleView_LostMouseCapture(object sender, MouseEventArgs e)
		{
			if (!this.IsMouseCaptured)
				return;
			this.MouseMove -= ObstacleView_MouseMove;
			this.LostMouseCapture -= ObstacleView_LostMouseCapture;
		}

		void ObstacleView_MouseMove(object sender, MouseEventArgs e)
		{
			if (!this.IsMouseCaptured)
				return;

			DependencyObject	currentParent	= VisualTreeHelper.GetParent(this);
			ObstacleLayerView	parent			= currentParent as ObstacleLayerView;

			while (parent == null && currentParent != null)
			{
				currentParent	= VisualTreeHelper.GetParent(currentParent);
				parent			= currentParent as ObstacleLayerView;
			}

			if (parent == null)
				return;

			ObstacleViewModel vm = this.DataContext as ObstacleViewModel;


			Point position = Mouse.GetPosition(parent);
			vm.X = position.X;
			vm.Y = position.Y;
		}
		#endregion
	}
}
