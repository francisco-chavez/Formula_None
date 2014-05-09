using System.ComponentModel;
using System.Windows;


namespace Unv.RaceTrackEditor.ViewModels
{
	public abstract class ViewModelBase
		: INPC
	{
		#region Properties
		public string DisplayTitle
		{
			get { return mn_displayTitle; }
			set
			{
				if (mn_displayTitle != value)
				{
					mn_displayTitle = value;
					OnPropertyChanged("DisplayTitle");
				}
			}
		}
		private string mn_displayTitle;

		public bool IsInDesignMode
		{
			get
			{
				var prop = DesignerProperties.IsInDesignModeProperty;
				return (bool) DependencyPropertyDescriptor
					.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
			}
		}
		#endregion


		#region Constructors
		public ViewModelBase()
			: this(null)
		{
		}

		public ViewModelBase(string displayTitle)
		{
			DisplayTitle = displayTitle;
		}
		#endregion
	}
}
