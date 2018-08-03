using Desktop.Commands;
using Desktop.Models;
using GalaSoft.MvvmLight;
using RecipeClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
    public class SellMedicinesViewModel : ViewModelBase
    {
        private readonly RecipeClient.RecipeClient client;

        private Models.Recipe recipe;

        public Models.Recipe Recipe
        {
            get => this.recipe;

            set => this.Set("Recipe", ref this.recipe, value);
        }

        public AddRecipeHistoryCommand AddRecipeHistoryCommand { get; private set; }

        public AsyncCommand<string, ResponseMessage<RecipeClient.Recipe>> FindRecipeCommand { get; private set; }

        public SellMedicinesViewModel()
        {
            this.client = ((App)App.Current).RecipeClient;
            this.FindRecipeCommand = new AsyncCommand<string, ResponseMessage<RecipeClient.Recipe>>(this.GetRecipe, _ => true);
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
