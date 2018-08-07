using Desktop.Commands;
using Desktop.Models;
using GalaSoft.MvvmLight;
using RecipeClient;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
    public class SellMedicinesViewModel : ViewModelBase
    {
        private readonly RecipeClient.RecipeClient client;

        private string recipeId;

        private ObservableCollection<Models.Recipe> recipe;

        private ObservableCollection<RecipeHistoryItem> historyItems;

        public ObservableCollection<Models.Recipe> Recipe
        {
            get => this.recipe;

            set => this.Set("Recipe", ref this.recipe, value);
        }

        public ObservableCollection<RecipeHistoryItem> HistoryItems
        {
            get => this.historyItems;

            set => this.Set("HistoryItems", ref this.historyItems, value);
        }

        public string RecipeId
        {
            get => this.recipeId;

            set => this.Set("RecipeId", ref this.recipeId, value);
        }

        public AddRecipeHistoryCommand AddRecipeHistoryCommand { get; private set; }

        public FindRecipeCommand FindRecipeCommand { get; private set; }

        public SellMedicinesViewModel()
        {
            this.client = ((App)App.Current).RecipeClient;
            this.HistoryItems = new ObservableCollection<RecipeHistoryItem>();
            this.FindRecipeCommand = new FindRecipeCommand(this, this.GetRecipe, _ => true);
            this.AddRecipeHistoryCommand = new AddRecipeHistoryCommand(this, this.CreateRecipeHistory, _ => true);
        }

        private async Task<ResponseMessage<RecipeClient.Recipe>> GetRecipe(string id)
        {
            return await this.client.GetAsync<RecipeClient.Recipe>($"api/recipes/{id}");
        }

        private async Task<ResponseMessage<string>> CreateRecipeHistory(RecipeHistory recipeHistory)
        {
            return await this.client.CreateAsync<RecipeHistory>("/api/recipeHistories", recipeHistory);
        }
    }
}
