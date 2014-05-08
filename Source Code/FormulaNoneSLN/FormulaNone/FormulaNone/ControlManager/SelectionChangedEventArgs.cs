using System;


namespace Unv.FormulaNone
{
	public class SelectionChangedEventArgs
		: EventArgs
	{
		public SelectionChangedEventArgs() { }
	}

	public delegate void SelectionChangedHandler(ControlBase source, SelectionChangedEventArgs e);
}
