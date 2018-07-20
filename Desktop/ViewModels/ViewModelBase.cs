using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Desktop.ViewModels
{
    /// <summary>
    /// View model base class
    /// </summary>
    public class ViewModelBase:INotifyPropertyChanged
    {
        /// <summary>
        /// Event for property changing
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Rises Property Changed event
        /// </summary>
        /// <param name="propertyName">Property name</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets property
        /// </summary>
        /// <typeparam name="TProperty">Type of property</typeparam>
        /// <param name="property">property</param>
        /// <param name="value">value</param>
        protected void SetProperty<TProperty>(ref TProperty property, TProperty value, [CallerMemberName]string propName = "")
        {
            // if property isn't changed
            if (property.Equals(value))
                return;

            // otherwise update and notify
            property = value;
            this.NotifyPropertyChanged(propName);
        }
    }
}
