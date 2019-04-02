using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ManagementDelivery.App.View
{
    /// <summary>
    /// Interaction logic for DeliveryWindow.xaml
    /// </summary>
    public partial class DeliveryWindow : Window
    {
        public DeliveryWindow()
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
