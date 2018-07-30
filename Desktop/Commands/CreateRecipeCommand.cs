using RecipeClient;
using System;
using System.Threading.Tasks;

namespace Desktop.Commands
{
    class CreateRecipeCommand : AsyncCommand<Recipe, ResponseMessage<string>>
    {
        public CreateRecipeCommand(Func<Recipe, Task<ResponseMessage<string>>> executeMethod, Func<Recipe, bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
        }

        public async override void Execute(object parameter)
        {
            var recipe = await this.Map((Models.Recipe)parameter);
            var response = await this.ExecuteAsync(recipe);
        }

        private async Task<RecipeClient.Recipe> Map(Models.Recipe recipe)
        {
            //map model.recipe to client.recipe
            return new RecipeClient.Recipe();
        }
    }
}
