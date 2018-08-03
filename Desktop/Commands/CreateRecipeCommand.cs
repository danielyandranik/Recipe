using System;
using System.Threading.Tasks;
using RecipeClient;

namespace Desktop.Commands
{
    /// <summary>
    /// Create Recipe command
    /// </summary>
    class CreateRecipeCommand : AsyncCommand<Models.Recipe, ResponseMessage<string>>
    {
        /// <summary>
        /// Creates new instance of <see cref="CreateRecipeCommand"/>
        /// </summary>
        /// <param name="executeMethod">Execute Method</param>
        /// <param name="canExecuteMethod">CanExecute Method</param>
        public CreateRecipeCommand(Func<Models.Recipe, Task<ResponseMessage<string>>> executeMethod, Func<Models.Recipe, bool> canExecuteMethod) : 
            base(executeMethod, canExecuteMethod)
        {
        }

        /// <summary>
        /// Executes command operation
        /// </summary>
        /// <param name="parameter">Command parameter</param>
        public async override void Execute(object parameter)
        {
            await this.ExecuteAsync((Models.Recipe)parameter);
        }
    }
}
