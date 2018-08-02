using Desktop.Commands;
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
    class SellMedicinesViewModel : ViewModelBase
    {
        private ObservableCollection<KeyValuePair<string, int>> sellingItems;

        private KeyValuePair<string, int> addingItem;

        private readonly RecipeClient.RecipeClient client;

        public ObservableCollection<KeyValuePair<string, int>> SellingItems
        {
            get => this.sellingItems;

            set => this.Set("SellingItems", ref this.sellingItems, value);
        }

        public KeyValuePair<string, int> AddingItem
        {
            get => this.addingItem;

            set => this.Set("AddingItem", ref this.addingItem, value);
        }

        public AddRecipeHistoryCommand AddRecipeHistoryCommand { get; private set; }

        public SellMedicinesViewModel()
        {
            this.client = ((App)App.Current).RecipeClient;
            this.AddRecipeHistoryCommand = new AddRecipeHistoryCommand(this, this.CreateRecipeHistory, _ => true);
        }

        private async Task<ResponseMessage<string>> CreateRecipeHistory(RecipeHistory recipeHistory)
        {
            return await this.client.CreateAsync<RecipeHistory>("/api/recipeHistories", recipeHistory);
        }
    }
}
