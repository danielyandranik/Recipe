using System;
using System.Windows.Input;
using System.Windows.Controls;

namespace Desktop.Commands
{
    /// <summary>
    /// Navigate command
    /// </summary>
    /// <typeparam name="T">Type of page</typeparam>
    public class NavigateCommand<T> : ICommand
        where T : Page, new()
    {
        /// <summary>
        /// Frame where pages can be navigated
        /// </summary>
        private readonly Frame _frame;

        /// <summary>
        /// Event for CanExecute changed
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Creates new instance of <see cref="NavigateCommand{T}"/>
        /// </summary>
        /// <param name="frame">Frame where the pages must be navigated</param>
        public NavigateCommand(Frame frame)
        {
            this._frame = frame;  
        }

        /// <summary>
        /// Determines if the command can be executed.
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        /// <returns>boolean value indicating whether the command can be executed</returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public void Execute(object parameter)
        {
            var page = new T();

            this._frame.NavigationService.Navigate(page);
        }
    }
}
