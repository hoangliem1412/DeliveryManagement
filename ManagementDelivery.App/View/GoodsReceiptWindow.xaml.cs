using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ManagementDelivery.App.View
{
    /// <summary>
    /// Interaction logic for GoodsReceiptWindow.xaml
    /// </summary>
    public partial class GoodsReceiptWindow : Window
    {
        public GoodsReceiptWindow()
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
