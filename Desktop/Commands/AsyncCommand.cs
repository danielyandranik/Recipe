using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop.Commands
{
    /// <summary>
    /// Async base command
    /// </summary>
    /// <typeparam name="TIn">Input type</typeparam>
    /// <typeparam name="TOut">Output type</typeparam>
    public class AsyncCommand<TIn,TOut>:ICommand
    {
        /// <summary>
        /// Execute delegate
        /// </summary>
        private readonly Func<TIn, Task<TOut>> _executeMethod;

        /// <summary>
        /// Can execute delegate
        /// </summary>
        private readonly Func<TIn, bool> _canExecuteMethod;

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
        /// Creates new instance of <see cref="AsyncCommand{TIn, TOut}"/>
        /// </summary>
        /// <param name="executeMethod">Execute method</param>
        /// <param name="canExecuteMethod">Can execute method</param>
        public AsyncCommand(Func<TIn,Task<TOut>> executeMethod,Func<TIn,bool> canExecuteMethod)
        {
            // setting fields
            this._executeMethod = executeMethod;
            this._canExecuteMethod = canExecuteMethod;
        }

        /// <summary>
        /// Determines if command can be executed
        /// </summary>
        /// <param name="parameter">parameter</param>
        /// <returns>Boolean value indicating the availability of command</returns>
        public  bool CanExecute(object parameter)
        {
            return this._canExecuteMethod((TIn)parameter);
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public virtual async void  Execute(object parameter)
        {
            await this.ExecuteAsync((TIn)parameter);
        }

        /// <summary>
        /// Executes command asynchronously
        /// </summary>
        /// <param name="obj">Commmand parameter</param>
        /// <returns></returns>
        public async Task<TOut> ExecuteAsync(TIn obj)
        {
            return await this._executeMethod(obj);
        }
    }
}
