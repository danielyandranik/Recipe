using System.Windows;

namespace Desktop.Views.Windows
{
    /// <summary>
    /// Interaction logic for RecipeMessageBox.xaml
    /// </summary>
    public partial class RecipeMessageBox : Window
    {
        /// <summary>
        /// Creates new instance of <see cref="RecipeMessageBox"/>
        /// </summary>
        public RecipeMessageBox()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// Shows new message box
        /// </summary>
        /// <param name="message">message</param>
        public static void Show(string message)
        {
            var msg = new RecipeMessageBox();
            msg.message.Text = message;
            msg.Show();
        }

        /// <summary>
        /// Closes the message box
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event argument</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
