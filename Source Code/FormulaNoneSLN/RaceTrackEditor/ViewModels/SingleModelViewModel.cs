using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Unv.RaceTrackEditor.Core.Models;


namespace Unv.RaceTrackEditor.ViewModels
{
	public abstract class SingleModelViewModel<T>
		: ViewModelBase
		where T : IModel
	{
		#region Properties
		public virtual T Model
		{
			get { return m_model; }
			set
			{
				// If the model that was passed in is the same model
				// that's already here, just exit the function. 
				// 
				// I used the EqualityComparer<T> function below because
				// I can't use the == operator on generic types.
				// -FCT
				if (EqualityComparer<T>.Default.Equals(m_model, value))
					return;

				if (m_model != null)
					ClearOutModelData();

				m_model = value;

				if (m_model != null)
					LoadModelData();
			}
		}
		private T m_model;
		#endregion


		#region Constructors
		public SingleModelViewModel()
			: this(null) { }

		public SingleModelViewModel(string displayTitle)
			: base(displayTitle) { }
		#endregion


		#region Methods
		public virtual void ClearOutModelData() { }
		public virtual void LoadModelData() { }
		#endregion
	}
}
