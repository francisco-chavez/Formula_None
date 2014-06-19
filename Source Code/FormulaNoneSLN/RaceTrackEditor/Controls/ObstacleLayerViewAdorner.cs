using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

using Unv.RaceTrackEditor.Views;


namespace Unv.RaceTrackEditor.Controls
{
	public class ObstacleLayerViewAdorner
		: Adorner
	{
		#region Attributes
		private MeasuredCanvas		m_drawArea;
		private ObstacleLayerView	m_parentView;

		private Point	m_startingPosition;
		private Line	m_line;
		private Ellipse m_circle;
		#endregion


		#region Properties
		public Brush DrawBrush
		{
			get { return m_drawBrush; }
			set
			{
				if (m_drawBrush != value)
				{
					m_drawBrush		= value;
					m_circle.Fill	= value;
					m_line.Stroke	= value;
				}
			}
		}
		private Brush m_drawBrush = Brushes.Maroon;

		public double TargetTypeSwitchDistance { get; set; }

		public Point CurrentPosition
		{
			get { return m_currentPosition; }
			set
			{
				if (m_currentPosition != value)
				{
					m_currentPosition = value;
					UpdateDrawing();
				}
			}
		}
		private Point m_currentPosition;

		public bool IsDrawing
		{
			get;
			private set;
		}

		protected override int VisualChildrenCount
		{
			get { return 1; }
		}
		#endregion


		#region Constructors
		public ObstacleLayerViewAdorner(UIElement adornedElement)
			: base(adornedElement)
		{
			if (!(adornedElement is ObstacleLayerView))
				throw new ArgumentException();

			this.IsHitTestVisible = false;

			m_parentView	= (ObstacleLayerView) adornedElement;
			m_drawArea		= new MeasuredCanvas();
			m_drawArea.MinWidth = m_parentView.MinWidth;
			m_drawArea.MinHeight = m_drawArea.MinHeight;

			TargetTypeSwitchDistance = 16;

			m_line = new Line();
			m_line.Stroke = this.DrawBrush;
			m_line.StrokeThickness = 32;
			m_line.StrokeStartLineCap = PenLineCap.Round;
			m_line.StrokeEndLineCap = PenLineCap.Round;

			m_circle = new Ellipse();
			m_circle.Fill = this.DrawBrush;
			m_circle.Width = 32;
			m_circle.Height = 32;

			Panel.SetZIndex(this, 150);

			Binding b = new Binding("Width");
			b.Source = m_parentView;
			b.Mode = BindingMode.OneWay;
			b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
			this.SetBinding(WidthProperty, b);

			b = new Binding("Height");
			b.Source = m_parentView;
			b.Mode = BindingMode.OneWay;
			b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
			this.SetBinding(HeightProperty, b);

			this.AddVisualChild(m_drawArea);
		}
		#endregion


		#region Methods
		public void StartDrawing(Point startingPoint)
		{
			m_startingPosition = startingPoint;
			CurrentPosition = startingPoint;
			IsDrawing = true;

			UpdateDrawing();
		}

		public void StopDrawing()
		{
			IsDrawing = false;
			m_drawArea.Children.Clear();
		}


		private void UpdateDrawing()
		{
			if (!IsDrawing)
				return;

			Canvas.SetLeft(m_circle, m_startingPosition.X - (m_circle.Width / 2));
			Canvas.SetTop(m_circle, m_startingPosition.Y - (m_circle.Height / 2));

			m_line.X1 = m_startingPosition.X;
			m_line.Y1 = m_startingPosition.Y;

			m_line.X2 = CurrentPosition.X;
			m_line.Y2 = CurrentPosition.Y;


			var positionDelta = CurrentPosition - m_startingPosition;
			bool drawLine = positionDelta.LengthSquared > TargetTypeSwitchDistance * TargetTypeSwitchDistance;

			if (m_drawArea.Children.Count > 1)
				m_drawArea.Children.Clear();

			if (m_drawArea.Children.Count == 1)
			{
				if (drawLine)
				{
					if (m_drawArea.Children[0] is Ellipse)
						m_drawArea.Children.Clear();
				}
				else
				{
					if (m_drawArea.Children[0] is Line)
						m_drawArea.Children.Clear();
				}
			}

			if (m_drawArea.Children.Count == 0)
				m_drawArea.Children.Add(drawLine ? (UIElement) m_line : m_circle);
		}


		protected override Visual GetVisualChild(int index)
		{
			return m_drawArea;
		}

		protected override Size MeasureOverride(Size constraint)
		{
			m_drawArea.MinHeight = m_parentView.MinHeight;
			m_drawArea.MinWidth = m_parentView.MinWidth;

			m_drawArea.Measure(AdornedElement.RenderSize);
			return m_drawArea.DesiredSize;
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			m_drawArea.Arrange(new Rect(finalSize));
			return finalSize;
		}
		#endregion
	}
}
