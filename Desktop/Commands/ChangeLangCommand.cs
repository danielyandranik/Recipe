using System;
using System.Windows;
using System.Windows.Input;
using Desktop.ViewModels;

namespace Desktop.Commands
{
    public class ChangeLangCommand : ICommand
    {
        /// <summary>
        /// Main window view mode
        /// </summary>
        private readonly MainWindowViewModel _vm;

        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Creates new instance of <see cref="ChangeLangCommand"/>
        /// </summary>
        /// <param name="vm">Main Window view model</param>
        public ChangeLangCommand(MainWindowViewModel vm)
        {
            this._vm = vm;
        }

        /// <summary>
        /// Determines if command can bex executed.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        /// <returns>Boolean value indicating whether the command can be executed.</returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public void Execute(object parameter)
        {
            var tag = (string)parameter;

            var merged = App.Current.Resources.MergedDictionaries;

            var uri = new Uri($"/Language/lang.{tag}.xaml",UriKind.Relative);

            var dictionary = new ResourceDictionary
            {
                Source = uri
            };

            merged.RemoveAt(4);
            merged.Add(dictionary);

            this._vm.CurrentProfile = (string)dictionary[User.Default.CurrentProfile];

            User.Default.Language = tag;
            User.Default.Save();
        }
    }
}
