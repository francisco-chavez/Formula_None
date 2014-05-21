using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

using Unv.RaceTrackEditor.Views;


namespace Unv.RaceTrackEditor.Controls
{
	public class ObstacleLayerViewAdorner
		: Adorner
	{
		#region Attributes
		private MeasuredCanvas		m_drawArea;
		private ObstacleLayerView	m_parentView;
		#endregion


		#region Properties
		protected override int VisualChildrenCount
		{
			get { return 1; }
		}
		#endregion


		#region Constructors
		public ObstacleLayerViewAdorner(UIElement adornerElement)
			: base(adornerElement)
		{
			if (!(adornerElement is ObstacleLayerView))
				throw new ArgumentException();

			m_parentView	= (ObstacleLayerView) adornerElement;
			m_drawArea		= new MeasuredCanvas();
		}
		#endregion


		#region Methods
		protected override Visual GetVisualChild(int index)
		{
			return m_drawArea;
		}

		protected override Size MeasureOverride(Size constraint)
		{
			m_drawArea.Measure(constraint);
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
