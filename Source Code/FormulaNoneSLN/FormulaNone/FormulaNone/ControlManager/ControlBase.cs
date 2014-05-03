using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unv.FormulaNone
{
	public abstract class ControlBase
	{
		protected ControlManager ControlManager { get; private set; }

		public ControlBase(ControlManager controlManager)
		{
			ControlManager = controlManager;
		}

		public abstract void Clear();
	}
}
