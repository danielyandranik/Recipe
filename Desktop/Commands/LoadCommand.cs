using System;
using System.Windows.Input;
using Desktop.Interfaces;

namespace Desktop.Commands
{
    public class LoadCommand : ICommand
    {
        private readonly ILoadService _loadService;

        public event EventHandler CanExecuteChanged;

        public LoadCommand(ILoadService loadService)
        {
            this._loadService = loadService;
        }

        public bool CanExecute(object parameter) => true;

        public async void Execute(object parameter)
        {
            await this._loadService.Load();
        }
    }
}
