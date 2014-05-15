using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;


namespace Unv.RaceTrackEditor.Controls
{
	public class MeasuredCanvas
		: Canvas
	{
		protected override Size MeasureOverride(Size constraint)
		{
			Size size = new Size();

			foreach (UIElement element in InternalChildren)
			{
				double left = Canvas.GetLeft(element);
				double top = Canvas.GetTop(element);

				left = double.IsNaN(left) ? 0 : left;
				top = double.IsNaN(top) ? 0 : top;

				element.Measure(constraint);

				Size desiredSize = element.DesiredSize;

				if (!double.IsNaN(desiredSize.Width) && !double.IsNaN(desiredSize.Height))
				{
					size.Width = Math.Max(size.Width, left + desiredSize.Width);
					size.Height = Math.Max(size.Height, top + desiredSize.Height);
				}
			}

			Thickness margin = this.Margin;
			if (margin != null)
			{
				size.Width += margin.Left + margin.Right;
				size.Height += margin.Top + margin.Bottom;
			}

			if (double.IsNaN(MaxHeight) || MaxHeight <= 0)
				size.Height = Math.Min(size.Height, MaxHeight);

			if (double.IsNaN(MaxWidth) || MaxWidth <= 0)
				size.Width = Math.Min(size.Width, MaxWidth);

			return size;
		}
	}
}
