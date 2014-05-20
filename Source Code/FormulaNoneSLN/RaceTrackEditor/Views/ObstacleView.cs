﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;

using Unv.RaceTrackEditor.ViewModels;


namespace Unv.RaceTrackEditor.Views
{
	public class ObstacleView
		: Thumb
	{
		#region Attributes
		public static readonly DependencyProperty ObstacleImageProperty;
		public static readonly DependencyProperty ViewModelProperty;
		#endregion


		#region Properties
		public BitmapImage ObstacleImage
		{
			get { return (BitmapImage) GetValue(ObstacleImageProperty); }
			set { SetValue(ObstacleImageProperty, value); }
		}

		public ObstacleViewModel ViewModel
		{
			get { return (ObstacleViewModel) GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		#endregion


		#region Constructors
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

			ViewModelProperty = DependencyProperty.Register(
				"ViewModel",
				typeof(ObstacleViewModel),
				typeof(ObstacleView),
				new FrameworkPropertyMetadata(
					null, 
					FrameworkPropertyMetadataOptions.AffectsParentArrange | FrameworkPropertyMetadataOptions.AffectsParentMeasure));
		}

		public ObstacleView()
		{
			this.DragDelta += new DragDeltaEventHandler(ObstacleView_DragDelta);
		}
		#endregion


		#region Event Handlers
		void ObstacleView_DragDelta(object sender, DragDeltaEventArgs e)
		{
			ViewModel.X += e.HorizontalChange;
			ViewModel.Y += e.VerticalChange;
		}
		#endregion
	}
}