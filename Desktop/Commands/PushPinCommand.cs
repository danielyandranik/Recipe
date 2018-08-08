using System;
using System.Windows.Input;
using Desktop.Services;

namespace Desktop.Commands
{
    public class PushPinCommand : ICommand
    {
        private readonly PushPinService _pushPinService;

        public PushPinCommand(PushPinService pushPinService)
        {
            this._pushPinService = pushPinService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => parameter != null;

        public async void Execute(object parameter)
        {
            await this._pushPinService.AddPushPins((string)parameter);
        }
    }
}
