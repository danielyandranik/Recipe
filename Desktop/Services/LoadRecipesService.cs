using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UserManagementConsumer.Client;
using Desktop.Models;
using Desktop.ViewModels;
using Desktop.Views.Windows;

namespace Desktop.Services
{
    class LoadRecipesService
    {
        private readonly RecipesViewModel recipesViewModel;

        private readonly RecipeClient.RecipeClient recipeClient;

        private readonly MedicineApiClient.Client medicineClient;

        private readonly UserManagementConsumer.Client.UserManagementApiClient userApiClient;

        public LoadRecipesService(RecipesViewModel recipesViewModel) : this()
        {
            this.recipesViewModel = recipesViewModel;
        }

        public LoadRecipesService()
        {
            this.recipeClient = ((App)App.Current).RecipeClient;
            this.medicineClient = ((App)App.Current).MedicineClient;
            this.userApiClient = ((App)App.Current).UserApiClient;
        }

        public async Task Load()
        {
            var response = await this.recipeClient.GetAllAsync<RecipeClient.Recipe>($"api/recipes?patientId={User.Default.Id}");

            if (!response.IsSuccessStatusCode)
            {
                RecipeMessageBox.Show((string)App.Current.Resources["recipe_load_fail"]);
                return;
            }

            var recipes = new ObservableCollection<Recipe>();

            foreach (var recipeFromApi in response.Content)
            {
                recipes.Add(await this.Map(recipeFromApi));
            }

            this.recipesViewModel.Recipes = recipes;

        }

        public async Task<Recipe> Map(RecipeClient.Recipe recipeFromApi)
        {
            var recipeItems = new ObservableCollection<RecipeItem>();

            var dictionary = App.Current.Resources;

            foreach (var recipeItemFromApi in recipeFromApi.RecipeItems)
            {
                var medicineApiResponse = await this.medicineClient.GetMedicineAsync($"api/medicines/{recipeItemFromApi.MedicineId}");

                if (!medicineApiResponse.IsSuccessStatusCode)
                {
                    RecipeMessageBox.Show((string)dictionary["med_name_fail"]);
                    return null;
                }

                var recipeItem = new RecipeItem
                {
                    Medicine = medicineApiResponse.Result,
                    Count = recipeItemFromApi.Count,
                    CountPerUse = recipeItemFromApi.CountPerUse,
                    TimesPerUnit = recipeItemFromApi.TimesPerUnit,
                    UseFrequencyUnit = recipeItemFromApi.UseFrequencyUnit
                };

                recipeItems.Add(recipeItem);
            }

            var gettingHospitalNameResponse = await this.userApiClient.GetDoctorByIdAsync(recipeFromApi.DoctorId);

            if (gettingHospitalNameResponse.Status == Status.Error)
            {
                RecipeMessageBox.Show((string)dictionary["hospital_name_fail"]);
                return null;
            }

            var gettingDoctorFullNameResponse = await this.userApiClient.GetUserAsync(recipeFromApi.DoctorId);

            if (gettingDoctorFullNameResponse.Status == Status.Error)
            {
                RecipeMessageBox.Show((string)dictionary["doc_fail"]);
                return null;
            }

            return new Recipe
            {
                CreatedOn = recipeFromApi.CreatedOn,
                HospitalName = gettingHospitalNameResponse.Result.HospitalName,
                Id = recipeFromApi.Id,
                DoctorName = gettingDoctorFullNameResponse.Result.FullName,
                RecipeItems = recipeItems
            };
        }
    }
}
