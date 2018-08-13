using System.Windows;
using GalaSoft.MvvmLight;

namespace Desktop.ViewModels
{
    /// <summary>
    /// Class for loadable window view model
    /// </summary>
    public class LoadableWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// Text visibility
        /// </summary>
        private Visibility _textVisibility;

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
        /// Gets or sets text visibility
        /// </summary>
        public Visibility TextVisibility
        {
            // gets  text visibility
            get => this._textVisibility;

            // sets  text visibility
            set => this.Set("TextVisibility", ref this._textVisibility, value);
        }

        /// <summary>
        /// Creates new instance of <see cref="LoadableWindowViewModel"/>
        /// </summary>
        public LoadableWindowViewModel()
        {
            this.SetVisibilities(Visibility.Collapsed, Visibility.Visible, false);
        }

        /// <summary>
        /// Sets visibilities
        /// </summary>
        /// <param name="spinnerVisibility">Spinner visibility</param>
        /// <param name="textVisibility">Text visibility</param>
        /// <param name="isSpinning">IsSpinning value</param>
        public void SetVisibilities(Visibility spinnerVisibility, Visibility textVisibility, bool isSpinning)
        {
            this.SpinnerVisibility = spinnerVisibility;
            this.TextVisibility = textVisibility;
            this.IsSpinning = isSpinning;
        }
    }
}
