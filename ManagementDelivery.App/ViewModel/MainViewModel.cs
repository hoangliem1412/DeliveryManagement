using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ManagementDelivery.App.ScreenView;
using ManagementDelivery.Model;

namespace ManagementDelivery.App.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Delivery> _listDelivery;

        public ObservableCollection<Delivery> ListDelivery
        {
            get { return _listDelivery; }
            set { _listDelivery = value; OnPropertyChanged(); }
        }

        public ICommand CustomerCommand { get; set; }
        public ICommand CategoryCommand { get; set; }
        public ICommand DriverCommand { get; set; }
        public ICommand SupplierCommand { get; set; }
        public ICommand ProductCommand { get; set; }

        public MainViewModel()
        {
            ListDelivery = new ObservableCollection<Delivery>(DataProvider.Ins.DB.Deliveries.Where(x => !x.IsDelete));

            CustomerCommand = new RelayCommand<object>((p) => true, (p) => { CustomerWindow wd = new CustomerWindow(); wd.ShowDialog(); });
            CategoryCommand = new RelayCommand<object>((p) => true, (p) => { CategoryWindow wd = new CategoryWindow(); wd.ShowDialog(); });
            DriverCommand = new RelayCommand<object>((p) => true, (p) => { DriverWindow wd = new DriverWindow(); wd.ShowDialog(); });
            SupplierCommand = new RelayCommand<object>((p) => true, (p) => { SupplierWindow wd = new SupplierWindow(); wd.ShowDialog(); });
            ProductCommand = new RelayCommand<object>((p) => true, (p) => { ProductWindow wd = new ProductWindow(); wd.ShowDialog(); });

        }
    }
}
