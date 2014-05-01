using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unv.FormulaNone
{
	public class ControlManager
	{
		#region Attributes
		private List<ControlBase> m_controls;
		#endregion


		#region Initialization
		public ControlManager()
		{
			m_controls = new List<ControlBase>();
		}
		#endregion

		public void Clear()
		{
			throw new NotImplementedException();
		}
	}
}
