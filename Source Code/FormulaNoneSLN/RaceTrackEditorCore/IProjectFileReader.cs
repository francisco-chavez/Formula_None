﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unv.RaceTrackEditor.Core
{
	public interface IProjectFileReader
	{
		string FileExtension		{ get; }
		string ExtensionDescription { get; }
	}
}