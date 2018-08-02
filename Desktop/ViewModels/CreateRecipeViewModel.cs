using Desktop.Commands;
using Desktop.Models;
using Desktop.Views.Windows;
using GalaSoft.MvvmLight;
using RecipeClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UserManagementConsumer.Client;

namespace Desktop.ViewModels
{
    internal class CreateRecipeViewModel : ViewModelBase
    {
        private Models.Recipe recipe;

        private Models.RecipeItem addingItem;

        public Models.Recipe Recipe
        {
            get => this.recipe;
            set => this.Set("Recipe", ref this.recipe, value);
        }

        public Models.RecipeItem AddingItem
        {
            get => this.addingItem;
            set => this.Set("AddingItem", ref this.addingItem, value);
        }

        public CreateRecipeCommand CreateRecipeCommand { get; }

        public CreateRecipeViewModel()
        {
            this.Recipe = new Models.Recipe();
            this.AddingItem = new Models.RecipeItem();
           // this.RecipeItems = new ObservableCollection<Models.RecipeItem>();
            this.CreateRecipeCommand = new CreateRecipeCommand(this.CreateRecipe, _ => true);
        }

        private async Task<ResponseMessage<string>> CreateRecipe(Models.Recipe recipeModel)
        {
            var userApiResponse = await ((App)App.Current).UserApiClient.GetUserByUsernameAsync(recipeModel.PatientUserName);

            if(userApiResponse.Status == Status.Error)
            {
                RecipeMessageBox.Show("Couldn't get patient");
                return null;
            }

            var patientId = userApiResponse.Result.Id;

            var recipeItems = new List<RecipeClient.RecipeItem>();

            foreach(var item in recipeModel.RecipeItems)
            {
                recipeItems.Add(new RecipeClient.RecipeItem
                {
                    Count = item.Count,
                    MedicineId = item.Medicine.Id,
                    UseFrequencyUnit = item.UseFrequencyUnit,
                    TimesPerUnit = item.TimesPerUnit,
                    CountPerUse =item.CountPerUse,
                });
            }

            var recipe = new RecipeClient.Recipe
            {
                CreatedOn = DateTime.Now,
                DoctorId = User.Default.Id,
                PatientId = patientId,
                RecipeItems = recipeItems
            };

            return await ((App)App.Current).RecipeClient.CreateAsync<RecipeClient.Recipe>("api/recipes", recipe);
        }
    }
}
