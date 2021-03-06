﻿using System.ComponentModel;


namespace Unv.RaceTrackEditor
{
	public abstract class INPC
		: INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public INPC() { }

		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
