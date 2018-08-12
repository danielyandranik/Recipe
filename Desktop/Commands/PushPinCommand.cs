using System;
using System.Windows.Input;
using Desktop.Services;

namespace Desktop.Commands
{
    /// <summary>
    /// Push pin command
    /// </summary>
    public class PushPinCommand : ICommand
    {
        /// <summary>
        /// Pushpin service
        /// </summary>
        private readonly PushPinService _pushPinService;

        /// <summary>
        /// Creates new instanc of <see cref="PushPinCommand"/>
        /// </summary>
        /// <param name="pushPinService">Pushin service</param>
        public PushPinCommand(PushPinService pushPinService)
        {
            this._pushPinService = pushPinService;
        }

        /// <summary>
        /// Event for CanExecute changed
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Determines whether the command can be executed.
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        /// <returns>boolean value indicating whether the command can be executed.</returns>
        public bool CanExecute(object parameter) => parameter != null;

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public async void Execute(object parameter)
        {
            await this._pushPinService.AddPushPins((string)parameter);
        }
    }
}
