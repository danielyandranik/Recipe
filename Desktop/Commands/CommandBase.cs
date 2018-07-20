using System;
using System.Windows.Input;

namespace Desktop.Commands
{
    /// <summary>
    /// Base class for command
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        /// <summary>
        /// Event  for CanExecute changed
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            // adds new subscriber
            add => CommandManager.RequerySuggested += value;

            // removes subsciber
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Determines if command can be executed.
        /// </summary>
        /// <param name="parameter">Parameter</param>
        /// <returns>boolean value indicating if the command can be executed</returns>
        public abstract bool CanExecute(object parameter);

        /// <summary>
        /// Executes command
        /// </summary>
        /// <param name="parameter">parameter</param>
        public abstract void Execute(object parameter);
    }
}
