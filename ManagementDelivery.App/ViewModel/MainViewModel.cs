using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private Delivery _selectedItem;
        public Delivery SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();

                if (SelectedItem != null)
                {
                    
                }
            }
        }

        private int _deliveryId;
        public int DeliveryId
        {
            get => _deliveryId;
            set
            {
                _deliveryId = value;
                OnPropertyChanged();
            }
        }

        private int _inputQuantity { get; set; }
        public int InputQuantity
        {
            get { return _inputQuantity; }
            set { _inputQuantity = value; OnPropertyChanged(); }
        }

        private int _outputQuantity { get; set; }
        public int OutputQuantity
        {
            get { return _outputQuantity; }
            set { _outputQuantity = value; OnPropertyChanged(); }
        }

        private int _inventory { get; set; }
        public int Inventory
        {
            get { return _inventory; }
            set { _inventory = value; OnPropertyChanged(); }
        }

        public ICommand AddDeliveryCommand { get; set; }
        public ICommand EditDeliveryCommand { get; set; }
        public ICommand StockCommand { get; set; }
        public ICommand ProductCommand { get; set; }
        public ICommand ProductCategoryCommand { get; set; }
        public ICommand SupplierCommand { get; set; }
        public ICommand CustomerCommand { get; set; }
        public ICommand DriverCommand { get; set; }

        public MainViewModel()
        {
            ListDelivery = new ObservableCollection<Delivery>(DataProvider.Ins.DB.Deliveries.Where(x => !x.IsDelete).OrderByDescending(x => x.UpdateAt));

            OutputQuantity = DataProvider.Ins.DB.DeliveryDetails.Where(x => x.Delivery.DeliveryDate.Month == DateTime.Now.Month).Select(x => x.Quantity).DefaultIfEmpty(0).Sum();

            InputQuantity = DataProvider.Ins.DB.GoodsReceipts.Where(x => x.DateReceipt.Month == DateTime.Now.Month).Select(x => x.Quantity).DefaultIfEmpty(0).Sum();

            Inventory = DataProvider.Ins.DB.Stocks.Where(x => !x.IsDelete).Select(x => x.Quantity).DefaultIfEmpty(0).Sum();

            CustomerCommand = new RelayCommand<object>((p) => true, (p) => { CustomerWindow wd = new CustomerWindow(); wd.ShowDialog(); });
            ProductCategoryCommand = new RelayCommand<object>((p) => true, (p) => { ProductCategoryWindow wd = new ProductCategoryWindow(); wd.ShowDialog(); });
            DriverCommand = new RelayCommand<object>((p) => true, (p) => { DriverWindow wd = new DriverWindow(); wd.ShowDialog(); });
            SupplierCommand = new RelayCommand<object>((p) => true, (p) => { SupplierWindow wd = new SupplierWindow(); wd.ShowDialog(); });
            ProductCommand = new RelayCommand<object>((p) => true, (p) => { ProductWindow wd = new ProductWindow(); wd.ShowDialog(); });
            StockCommand = new RelayCommand<object>((p) => true, (p) => { StockWindow wd = new StockWindow(); wd.ShowDialog(); });
            AddDeliveryCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                DeliveryWindow wd = new DeliveryWindow();
                DeliveryViewModel deliveryViewModel = new DeliveryViewModel();
                wd.DataContext = deliveryViewModel;
                wd.ShowDialog();

                ListDelivery = new ObservableCollection<Delivery>(DataProvider.Ins.DB.Deliveries.Where(x => !x.IsDelete).OrderByDescending(x => x.UpdateAt));
            });
            EditDeliveryCommand = new RelayCommand<object>((p) => true, (p) => 
            {
                DeliveryWindow wd = new DeliveryWindow();
                DeliveryViewModel deliveryViewModel = new DeliveryViewModel();
                deliveryViewModel.Delivery = p as Delivery;
                wd.DataContext = deliveryViewModel;
                //((DeliveryViewModel) wd.DataContext).Delivery = p as Delivery;
                wd.ShowDialog();

                ListDelivery = new ObservableCollection<Delivery>(DataProvider.Ins.DB.Deliveries.Where(x => !x.IsDelete).OrderByDescending(x => x.UpdateAt));
            });
        }
    }
}
