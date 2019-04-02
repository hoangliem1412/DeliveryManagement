using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ManagementDelivery.App.View
{
    /// <summary>
    /// Interaction logic for StockWindow.xaml
    /// </summary>
    public partial class StockWindow : Window
    {
        public StockWindow()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
