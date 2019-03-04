using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ManagementDelivery.App.ScreenView;

namespace ManagementDelivery.App.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand CustomerCommand { get; set; }

        public MainViewModel()
        {
            CustomerCommand = new RelayCommand<object>((p) => { return true; }, (p) => { CustomerWindow wd = new CustomerWindow(); wd.ShowDialog(); });
        }
    }
}
