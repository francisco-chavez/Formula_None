using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;


namespace Unv.RaceTrackEditor.Core.Models
{
	public class RaceTrackModel
	{
		public IProjectManager ProjectManager { get; set; }

		public BitmapImage RaceTrackImage
		{
			get 
			{
				if (m_raceTrackImage == null)
					LoadRaceTrackImage();

				return m_raceTrackImage; 
			}
		}
		private BitmapImage m_raceTrackImage;

		public void SetRaceTrackImage(string imagePath)
		{
			m_raceTrackImage = null;

			throw new NotImplementedException();
		}

		private void LoadRaceTrackImage()
		{
			throw new NotImplementedException();
		}
	}
}
