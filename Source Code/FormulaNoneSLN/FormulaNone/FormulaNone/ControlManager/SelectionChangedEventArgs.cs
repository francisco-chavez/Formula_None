using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unv.FormulaNone
{
	public class SelectionChangedEventArgs
		: EventArgs
	{
		public SelectionChangedEventArgs()
		{
		}
	}

	public delegate void SelectionChangedHandler(ControlBase source, SelectionChangedEventArgs e);
}
