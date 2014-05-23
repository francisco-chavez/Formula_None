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
	/// Interaction logic for ObstacleLayerView.xaml
	/// </summary>
	public partial class ObstacleLayerView 
		: UserControl
	{
		#region Attributes
		private Point			m_startingPosition;
		private Point			m_currentPosition;

		private ObstacleLayerViewAdorner	m_adorner;
		private AdornerLayer				m_adornerLayer;
		#endregion


		#region Constructors
		public ObstacleLayerView()
		{
			InitializeComponent();

			m_adorner = new ObstacleLayerViewAdorner(this);
			this.MouseLeftButtonDown += new MouseButtonEventHandler(ObstacleLayerView_MouseLeftButtonDown);
			this.Loaded += new RoutedEventHandler(ObstacleLayerView_Loaded);
		}
		#endregion


		#region Event Handlers
		void ObstacleLayerView_Loaded(object sender, RoutedEventArgs e)
		{
			if (m_adornerLayer == null)
			{
				m_adornerLayer = AdornerLayer.GetAdornerLayer(this);
				m_adornerLayer.Add(m_adorner);
			}
		}

		void ObstacleLayerView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (this.DataContext == null)
				return;

			this.MouseMove += ObstacleLayerView_MouseMove;
			this.MouseLeftButtonUp += ObstacleLayerView_MouseLeftButtonUp;
			this.MouseRightButtonDown += ObstacleLayerView_MouseRightButtonDown;

			m_startingPosition = Mouse.GetPosition(this);
			m_currentPosition = m_startingPosition;

			m_adorner.StartDrawing(m_startingPosition);
		}

		void ObstacleLayerView_MouseMove(object sender, MouseEventArgs e)
		{
			m_currentPosition = Mouse.GetPosition(this);
			m_adorner.CurrentPosition = m_currentPosition;

			// If the User releases the left mouse button when it's not
			// over the layer's draw area, the relase event won't get
			// triggered. This way, we can see if it's currently relseased
			// when it comes back into view.
			if (e.LeftButton == MouseButtonState.Released)
				StopDrawingTargetSystem();
		}

		void ObstacleLayerView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			StopDrawingTargetSystem();
			InsertNewObstacles();
		}

		void ObstacleLayerView_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			StopDrawingTargetSystem();
		}
		#endregion


		#region Methods
		private void InsertNewObstacles()
		{
			ObstacleLayerViewModel layerVM = DataContext as ObstacleLayerViewModel;
			layerVM.AddObstacles(m_startingPosition, m_currentPosition, 16);
		}

		private void StopDrawingTargetSystem()
		{
			this.MouseMove -= ObstacleLayerView_MouseMove;
			this.MouseLeftButtonUp -= ObstacleLayerView_MouseLeftButtonUp;
			this.MouseRightButtonDown -= ObstacleLayerView_MouseRightButtonDown;

			m_adorner.StopDrawing();
		}
		#endregion
	}
}
