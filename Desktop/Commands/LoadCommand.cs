using System;
using System.Windows.Input;
using Desktop.Interfaces;

namespace Desktop.Commands
{
    /// <summary>
    /// Load command
    /// </summary>
    public class LoadCommand : ICommand
    {
        /// <summary>
        /// Load service
        /// </summary>
        private readonly ILoadService _loadService;

        /// <summary>
        /// Event for CanExecute
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Creates new instance of <see cref="LoadCommand"/>
        /// </summary>
        /// <param name="loadService">Load service</param>
        public LoadCommand(ILoadService loadService)
        {
            this._loadService = loadService;
        }

        /// <summary>
        /// Determines if the command can be executed.
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        /// <returns>boolean value indicating wheter the command can be executed</returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public async void Execute(object parameter)
        {
            await this._loadService.Load();
        }
    }
}
