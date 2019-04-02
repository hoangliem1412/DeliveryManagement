using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ManagementDelivery.App.Core;
using ManagementDelivery.App.Model;

namespace ManagementDelivery.App.ViewModel
{
    public class StockViewModel : ViewModelBase
    {
        private ObservableCollection<Stock> _list;
        public ObservableCollection<Stock> List
        {
            get => _list;
            set { _list = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Product> _listProduct;
        public ObservableCollection<Product> ListProduct
        {
            get => _listProduct;
            set { _listProduct = value; OnPropertyChanged(); }
        }

        private Stock _selectedItem;
        public Stock SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    SelectedItemProduct = SelectedItem.Product;
                    Quantity = SelectedItem.Quantity;
                }
            }
        }

        private Product _selectedItemProduct;
        public Product SelectedItemProduct
        {
            get => _selectedItemProduct;
            set
            {
                _selectedItemProduct = value;
                OnPropertyChanged();

                if (value != null) ProductId = value.Id;
            }
        }

        private int _productId;
        public int ProductId
        {
            get => _productId;
            set { _productId = value; OnPropertyChanged(); }
        }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set { _quantity = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        public StockViewModel()
        {
            List = new ObservableCollection<Stock>(DataProvider.Ins.DB.Stocks.Where(x => !x.IsDelete).OrderByDescending(x => x.UpdateAt));
            ListProduct = new ObservableCollection<Product>(DataProvider.Ins.DB.Products.Where(x => !x.IsDelete).OrderByDescending(x => x.UpdateAt));

            AddCommand = new RelayCommand<object>((p) => SelectedItemProduct != null && Quantity >= 0, (p) =>
            {
                var stock = new Stock()
                {
                    ProductId = ProductId,
                    Quantity = Quantity,
                    InsertAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };

                DataProvider.Ins.DB.Stocks.Add(stock);
                DataProvider.Ins.DB.SaveChanges();

                List.Insert(0, stock);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                return SelectedItem != null && DataProvider.Ins.DB.Stocks.Any(x => x.Id == SelectedItem.Id);
            }, (p) =>
            {
                var stock = DataProvider.Ins.DB.Stocks.FirstOrDefault(x => x.Id == SelectedItem.Id);
                if (stock != null)
                {
                    stock.Quantity = Quantity;
                    stock.UpdateAt = DateTime.Now;
                    DataProvider.Ins.DB.SaveChanges();
                }
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return SelectedItem != null && DataProvider.Ins.DB.Stocks.Any(x => x.Id == SelectedItem.Id);
            }, (p) =>
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Xác nhận", MessageBoxButton.YesNo);
                if (messageBoxResult != MessageBoxResult.Yes)
                {
                    return;
                }

                var stock = DataProvider.Ins.DB.Stocks.FirstOrDefault(x => x.Id == SelectedItem.Id);
                if (stock != null)
                {
                    DataProvider.Ins.DB.Stocks.Remove(stock);
                    DataProvider.Ins.DB.SaveChanges();
                    List.Remove(stock);
                }

                SelectedItem = null;
            });

            ClearCommand = new RelayCommand<object>((p) => SelectedItemProduct != null || Quantity >= 0, (p) =>
                {
                    SelectedItem = null;
                    SelectedItemProduct = null;
                    Quantity = 0;
                }
            );

            RefreshCommand = new RelayCommand<object>((p) => true,
                (p) =>
                {
                    List = new ObservableCollection<Stock>(DataProvider.Ins.DB.Stocks.Where(x => !x.IsDelete).OrderByDescending(x => x.UpdateAt));
                }
            );
        }
    }
}
