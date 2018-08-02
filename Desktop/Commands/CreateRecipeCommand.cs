using Desktop.Models;
using RecipeClient;
using System;
using System.Threading.Tasks;

namespace Desktop.Commands
{
    class CreateRecipeCommand : AsyncCommand<Models.Recipe, ResponseMessage<string>>
    {
        public CreateRecipeCommand(Func<Models.Recipe, Task<ResponseMessage<string>>> executeMethod, Func<Models.Recipe, bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
        }

        public async override void Execute(object parameter)
        {
            await this.ExecuteAsync((Models.Recipe)parameter);
        }
    }
}
