using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;


namespace Unv.RaceTrackEditor.Views
{
	public class ObstacleView
		: Thumb
	{
		public static readonly DependencyProperty ObstacleImageProperty;


		public BitmapImage ObstacleImage
		{
			get { return (BitmapImage) GetValue(ObstacleImageProperty); }
			set { SetValue(ObstacleImageProperty, value); }
		}


		static ObstacleView()
		{
			DefaultStyleKeyProperty.OverrideMetadata(
				typeof(ObstacleView),
				new FrameworkPropertyMetadata(typeof(ObstacleView)));

			ObstacleImageProperty = DependencyProperty.Register(
				"ObstacleImage",
				typeof(BitmapImage),
				typeof(ObstacleView),
				new FrameworkPropertyMetadata(null));
		}
	}
}
