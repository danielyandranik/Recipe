using System;
using System.Windows.Input;
using Desktop.Views;

namespace Desktop.Commands
{
    /// <summary>
    /// Command for hyper link
    /// </summary>
    public class HyperLinkCommand : ICommand
    {
        /// <summary>
        /// Event handler for CanExecuteChanged
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Determines if command can be executed.
        /// </summary>
        /// <param name="parameter">Parameter</param>
        /// <returns>boolean value indicating if the command can be executed</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter">parameter</param>
        public void Execute(object parameter)
        {
            if(parameter is RegisterWindow)
            {
                new SignIn().Show();
                ((RegisterWindow)parameter).Close();
            }
            else
            {
                new RegisterWindow().Show();
                ((SignIn)parameter).Close();
            }
        }
    }
}
