using System.Windows;
using Desktop.ViewModels;

namespace Desktop.Commands
{
    /// <summary>
    /// Filter command
    /// </summary>
    public class FilterCommand : CommandBase
    {
        /// <summary>
        /// Filterable page view mode
        /// </summary>
        private readonly FilterablePageViewModel _vm;

        /// <summary>
        /// Creates new instance of <see cref="FilterCommand"/>
        /// </summary>
        /// <param name="filterablePageViewModel">Filterable page viewmodel</param>
        public FilterCommand(FilterablePageViewModel filterablePageViewModel)
        {
            // setting fields
            this._vm = filterablePageViewModel;
        }

        /// <summary>
        /// Determines if the command can be executed
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        /// <returns>boolean value indicating whether the command can be executed.</returns>
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public async override void Execute(object parameter)
        {
            this._vm.SetVisibilities(Visibility.Visible, true);

            await this._vm.Filter();

            this._vm.SetVisibilities(Visibility.Collapsed, false);
        }
    }
}
