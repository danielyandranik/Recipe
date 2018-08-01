using Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Services
{
    class LoadRecipesService
    {
        private readonly RecipesViewModel recipesViewModel;

        private readonly RecipeClient.RecipeClient client;

        public LoadRecipesService(RecipesViewModel recipesViewModel)
        {
            this.recipesViewModel = recipesViewModel;
            this.client = ((App)App.Current).RecipeClient;
        }

        public async Task Load()
        {
            var response = await this.client.GetAllAsync<RecipeClient.Recipe>($"api/medicines?patientId={User.Default.Id}");
        }
    }
}
