using System;
using System.Windows;
using System.Windows.Input;

namespace Desktop.Commands
{
    public class ChangeLangCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            var tag = (string)parameter;

            var app = ((App)App.Current);

            var uri = new Uri($"/Language/lang.{tag}.xaml",UriKind.Relative);

            var dictionary = new ResourceDictionary
            {
                Source = uri
            };

            app.Resources.MergedDictionaries.RemoveAt(4);
            app.Resources.MergedDictionaries.Add(dictionary);

            User.Default.Language = tag;
            User.Default.Save();
        }


    }
}
