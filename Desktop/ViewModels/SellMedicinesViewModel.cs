using Desktop.Commands;
using Desktop.Models;
using Desktop.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RecipeClient;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Desktop.ViewModels
{
    public class SellMedicinesViewModel : ViewModelBase
    {
        private readonly RecipeClient.RecipeClient client;

        private readonly QrDecoderService _qrDecoder;

        private readonly RelayCommand _loadedCommand;

        private readonly RelayCommand _unloadedCommand;

        private string recipeId;

        private ObservableCollection<Models.Recipe> recipe;

        private ObservableCollection<RecipeHistoryItem> historyItems;

        private ImageSource _qrDecoderSource;

        private Visibility _qrDecoderVisibility;

        private Visibility _itemsVisibility;

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

        public Visibility QrDecoderVisibility
        {
            get => this._qrDecoderVisibility;

            set => this.Set("QrDecoderVisibility", ref this._qrDecoderVisibility, value);
        }

        public Visibility ItemsVisibility
        {
            get => this._itemsVisibility;

            set => this.Set("ItemsVisibility", ref this._itemsVisibility, value);
        }

        public ImageSource QrDecoderSource
        {
            get => this._qrDecoderSource;

            set => this.Set("QrDecoderSource", ref this._qrDecoderSource, value);
        }

        public AddRecipeHistoryCommand AddRecipeHistoryCommand { get; private set; }

        public FindRecipeCommand FindRecipeCommand { get; private set; }

        public ICommand LoadedCommand => this._loadedCommand;

        public ICommand UnloadedCommand => this._unloadedCommand;

        public SellMedicinesViewModel(Dispatcher dispatcher)
        {
            this.client = ((App)App.Current).RecipeClient;
            this.HistoryItems = new ObservableCollection<RecipeHistoryItem>();

            this.FindRecipeCommand = new FindRecipeCommand(this, this.GetRecipe, _ => true);
            this.AddRecipeHistoryCommand = new AddRecipeHistoryCommand(this, this.CreateRecipeHistory, _ => true);
            this._loadedCommand = new RelayCommand(this.Start, () => true);
            this._unloadedCommand = new RelayCommand(this.Finish, () => true);

            this._qrDecoder = new QrDecoderService(this,dispatcher);

            ((App)App.Current).QrDecoderService = this._qrDecoder;

            this._qrDecoderVisibility = Visibility.Visible;
            this._itemsVisibility = Visibility.Hidden;
        }

        public void Start()
        {
            this.QrDecoderVisibility = Visibility.Visible;
            this.ItemsVisibility = Visibility.Collapsed;
            this._qrDecoder.Start();
        }

        public void Finish()
        {
            this._qrDecoder.Stop();
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
