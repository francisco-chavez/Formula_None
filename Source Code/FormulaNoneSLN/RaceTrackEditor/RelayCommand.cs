using System;
using System.Diagnostics;
using System.Windows.Input;


namespace Unv.RaceTrackEditor
{
	/// <summary>
	/// This is the same code as http://msdn.microsoft.com/en-us/magazine/dd419663.aspx#id0090030, I've
	/// just reformated it a bit to match my coding style.
	/// </summary>
	public class RelayCommand
		: ICommand
	{
		#region Events
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		#endregion


		#region Attributes
		private readonly Action<object>		m_execute;
		private readonly Predicate<object>	m_canExecute;
		#endregion


		#region Constructors
		public RelayCommand(Action<object> execute)
			: this(execute, null)
		{
		}

		public RelayCommand(Action<object> execute, Predicate<object> canExecute)
		{
			if (execute == null)
				throw new ArgumentNullException("The Relay Command must be given something to execute.");

			m_execute		= execute;
			m_canExecute	= canExecute;
		}
		#endregion


		#region Methods
		[DebuggerStepThrough]
		public void Execute(object parameter)
		{
			m_execute(parameter);
		}

		public bool CanExecute(object parameter)
		{
			return m_canExecute == null ? true : m_canExecute(parameter);
		}
		#endregion
	}
}
