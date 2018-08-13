using System.Windows;
using GalaSoft.MvvmLight;

namespace Desktop.ViewModels
{
    /// <summary>
    /// Class for loadable page viewmodel
    /// </summary>
    public class LoadablePageViewModel : ViewModelBase
    {
        /// <summary>
        /// Spinner visibility
        /// </summary>
        private Visibility _spinnerVisibility;

        /// <summary>
        /// Is spinning value
        /// </summary>
        private bool _isSpinning;

        /// <summary>
        /// Gets or sets IsSpinning
        /// </summary>
        public bool IsSpinning
        {
            // gets IsSpinning
            get => this._isSpinning;

            // sets IsSpinning
            set => this.Set("IsSpinning", ref this._isSpinning, value);
        }

        /// <summary>
        /// Gets or sets spinner visibility
        /// </summary>
        public Visibility SpinnerVisibility
        {
            // gets spinner visibility
            get => this._spinnerVisibility;

            // sets spinner visibility
            set => this.Set("SpinnerVisibility", ref this._spinnerVisibility, value);
        }

        /// <summary>
        /// Creates new instance of <see cref="LoadablePageViewModel"/>
        /// </summary>
        public LoadablePageViewModel()
        {
            this.SetVisibilities(Visibility.Collapsed, false);
        }

        /// <summary>
        /// Sets visibilities
        /// </summary>
        /// <param name="spinnerVisibility">Spinner visibility</param>
        /// <param name="isSpinning">IsSpinning value</param>
        public void SetVisibilities(Visibility spinnerVisibility, bool isSpinning)
        {
            this.SpinnerVisibility = spinnerVisibility;
            this.IsSpinning = isSpinning;
        }
    }
}
