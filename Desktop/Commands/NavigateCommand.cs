using System;
using System.Windows.Input;
using System.Windows.Controls;

namespace Desktop.Commands
{
    public class NavigateCommand<T> : ICommand
        where T : Page, new()
    {
        private readonly Frame _frame;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public NavigateCommand(Frame frame)
        {
            this._frame = frame;  
        }

        public void Execute(object parameter)
        {
            var page = new T();

            this._frame.NavigationService.Navigate(page);
        }
    }
}
