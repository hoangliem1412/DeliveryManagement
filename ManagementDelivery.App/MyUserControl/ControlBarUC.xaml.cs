using System.Windows.Controls;
using ManagementDelivery.App.ViewModel;

namespace ManagementDelivery.App.MyUserControl
{
    /// <summary>
    /// Interaction logic for ControlBarUC.xaml
    /// </summary>
    public partial class ControlBarUC : UserControl
    {
        public ControlBarViewModel Viewmodel { get; set; }

        public ControlBarUC()
        {
            InitializeComponent();
            this.DataContext = Viewmodel = new ControlBarViewModel();
        }
    }
}
