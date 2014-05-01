using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unv.FormulaNone
{
	public class SelectionChangedEventArgs
		: EventArgs
	{
		public object OldValue { get; private set; }
		public object NewValue { get; private set; }

		public SelectionChangedEventArgs(object oldValue, object newValue)
		{
			OldValue = oldValue;
			NewValue = NewValue;
		}
	}

	public delegate void SelectionChangedHandler(ControlBase source, SelectionChangedEventArgs e);
}
