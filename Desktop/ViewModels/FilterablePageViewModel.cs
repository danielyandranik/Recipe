using System.Threading.Tasks;

namespace Desktop.ViewModels
{
    /// <summary>
    /// View model for pages that are loadable and have filterable content
    /// </summary>
    public abstract class FilterablePageViewModel : LoadablePageViewModel
    {
        /// <summary>
        /// Name filter text
        /// </summary>
        private string _name;

        /// <summary>
        /// Address filter text
        /// </summary>
        private string _address;

        /// <summary>
        /// Gets or sets name
        /// </summary>
        public string Name
        {
            // gets name
            get => this._name;

            // sets name
            set => this.Set("Name", ref this._name, value);
        }

        /// <summary>
        /// Gets or sets address
        /// </summary>
        public string Address
        {
            // gets address
            get => this._address;

            // sets address
            set => this.Set("Address", ref this._address, value);
        }

        /// <summary>
        /// Filters content
        /// </summary>
        /// <returns>nothing</returns>
        public abstract Task Filter();
    }
}
