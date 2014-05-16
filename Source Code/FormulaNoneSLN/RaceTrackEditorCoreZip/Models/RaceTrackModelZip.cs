using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Unv.RaceTrackEditor.Core.Models;


namespace Unv.RaceTrackEditor.Core.Zip.Models
{
	public class RaceTrackModelZip
		: RaceTrackModel
	{
		public override ObstacleDataModel Obstacles
		{
			get { return base.Obstacles; }
			set
			{
				if (!(value is ObstacleDataModelZip))
					throw new TypeAccessException("RaceTrackModelZip only accepts an ObstacleDataModel of type ObstacleDataModelZip");
				
				base.Obstacles = value;
			}
		}
	}
}
