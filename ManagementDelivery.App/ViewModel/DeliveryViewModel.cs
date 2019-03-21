using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ManagementDelivery.Model;

namespace ManagementDelivery.App.ViewModel
{
    public class DeliveryViewModel : ViewModelBase
    {
        private Delivery _delivery;

        public Delivery Delivery
        {
            get => _delivery;
            set
            {
                _delivery = value;
                OnPropertyChanged();
                if (value != null)
                {
                    ListDeliveryDetail = new ObservableCollection<DeliveryDetail>(DataProvider.Ins.DB.DeliveryDetails.Where(x => x.DeliveryId == value.Id).OrderByDescending(x => x.UpdateAt));
                    SelectedItemCustomer = value.Customer;
                    DeliveryDate = value.DeliveryDate;
                    IsEdit = true;
                    OnPropertyChanged("ListDeliveryDetail");
                }
            }
        }

        private ObservableCollection<DeliveryDetail> _listDeliveryDetail;

        public ObservableCollection<DeliveryDetail> ListDeliveryDetail
        {
            get => _listDeliveryDetail;
            set
            {
                _listDeliveryDetail = value;
                OnPropertyChanged();
                OnPropertyChanged("TotalPrice");
            }
        }

        private ObservableCollection<Customer> _listCustomer;
        public ObservableCollection<Customer> ListCustomer { get => _listCustomer; set { _listCustomer = value; OnPropertyChanged(); } }

        private ObservableCollection<Product> _listProductr;
        public ObservableCollection<Product> ListProduct { get => _listProductr; set { _listProductr = value; OnPropertyChanged(); } }

        private ObservableCollection<Driver> _listDriver;
        public ObservableCollection<Driver> ListDriver { get => _listDriver; set { _listDriver = value; OnPropertyChanged(); } }

        private DeliveryDetail _selectedItem;
        public DeliveryDetail SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    SelectedItemProduct = SelectedItem.Product;
                    SelectedItemDriver = SelectedItem.Driver;
                    Quantity = SelectedItem.Quantity;
                    Price = SelectedItem.Price;
                    Status = SelectedItem.Status;
                }
            }
        }

        private Customer _selectedItemCustomer;
        public Customer SelectedItemCustomer
        {
            get => _selectedItemCustomer;
            set
            {
                _selectedItemCustomer = value;
                OnPropertyChanged();

                if (value != null) CustomerId = value.Id;
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

                if (value != null)
                {
                    ProductId = value.Id;
                    Price = value.Price;
                }
            }
        }

        private Driver _selectedItemDriver;
        public Driver SelectedItemDriver
        {
            get => _selectedItemDriver;
            set
            {
                _selectedItemDriver = value;
                OnPropertyChanged();

                if (value != null)
                {
                    DriverId = value.Id;
                }
            }
        }

        private int CustomerId { get; set; }
        private int ProductId { get; set; }
        private int DriverId { get; set; }
        private bool IsEdit { get; set; }

        private DateTime _deliveryDate;
        public DateTime DeliveryDate
        {
            get => _deliveryDate;
            set
            {
                _deliveryDate = value;
                OnPropertyChanged();
            }
        }
        
        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged();
            }
        }

        private decimal _price;
        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        private int _status;
        public int Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalPrice;

        public decimal TotalPrice
        {
            get => ListDeliveryDetail?.Sum(x => x.Quantity * x.Price) ?? 0;
        }

        public ICommand AddCommand { get; set; }
        public ICommand AddDetailCommand { get; set; }
        public ICommand EditDetailCommand { get; set; }
        public ICommand DeleteDetailCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand ClearDetailCommand { get; set; }

        public DeliveryViewModel()
        {
            if (ListDeliveryDetail == null) ListDeliveryDetail = new ObservableCollection<DeliveryDetail>();

            ListProduct = new ObservableCollection<Product>(DataProvider.Ins.DB.Products.Where(x => !x.IsDelete).OrderByDescending(x => x.UpdateAt));
            ListCustomer = new ObservableCollection<Customer>(DataProvider.Ins.DB.Customers.Where(x => !x.IsDelete).OrderByDescending(x => x.UpdateAt));
            ListDriver = new ObservableCollection<Driver>(DataProvider.Ins.DB.Drivers.Where(x => !x.IsDelete).OrderByDescending(x => x.UpdateAt));
            DeliveryDate = DateTime.Now;

            AddCommand = new RelayCommand<Window>((p) => SelectedItemCustomer != null && ListDeliveryDetail.Any(), (p) =>
            {
                var delivery = new Delivery()
                {
                    CustomerId = CustomerId,
                    DeliveryDate = DeliveryDate,
                    DeliveryDetails = ListDeliveryDetail.ToList(),
                    TotalPrice = TotalPrice,
                    InsertAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };

                MessageBoxResult messageBoxResult = MessageBox.Show("Chắc chắn muốn tạo đơn hàng?", "Xác nhận", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    DataProvider.Ins.DB.Deliveries.Add(delivery);
                    foreach (var deliveryDetail in ListDeliveryDetail)
                    {
                        deliveryDetail.DeliveryId = delivery.Id;
                        DataProvider.Ins.DB.DeliveryDetails.Add(deliveryDetail);
                    }
                    DataProvider.Ins.DB.SaveChanges();

                    FrameworkElement window = GetWindowParent(p);
                    var w = window as Window;
                    if (w != null)
                    {
                        w.Close();
                    }
                }
            });

            AddDetailCommand = new RelayCommand<object>((p) => SelectedItemProduct != null && SelectedItemDriver != null && Price > 0 && Quantity > 0, (p) =>
            {
                var delivery = new DeliveryDetail()
                {
                    ProductId = ProductId,
                    Product = SelectedItemProduct,
                    DriverId = DriverId,
                    Driver = SelectedItemDriver,
                    Quantity = Quantity,
                    Price = Price,
                    InsertAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };


                ListDeliveryDetail.Add(delivery);
                OnPropertyChanged("TotalPrice");
                OnPropertyChanged("ListDeliveryDetail");
            });

            EditDetailCommand = new RelayCommand<object>((p) => SelectedItem != null && SelectedItemProduct != null && SelectedItemDriver != null && Price > 0 && Quantity > 0, (p) =>
            {
                var delivery = ListDeliveryDetail.FirstOrDefault(x => x.Id == SelectedItem.Id);
                if (delivery != null)
                {
                    delivery.ProductId = ProductId;
                    delivery.DriverId = DriverId;
                    delivery.Quantity = Quantity;
                    delivery.Price = Price;
                    delivery.UpdateAt = DateTime.Now;
                };

                OnPropertyChanged("ListDeliveryDetail"); OnPropertyChanged("TotalPrice");
            });

            DeleteDetailCommand = new RelayCommand<object>((p) => SelectedItem != null && SelectedItemProduct != null && SelectedItemDriver != null && Price > 0 && Quantity > 0, (p) =>
            {
                var delivery = ListDeliveryDetail.FirstOrDefault(x => x.Id == SelectedItem.Id);
                ListDeliveryDetail.Remove(delivery);

                OnPropertyChanged("ListDeliveryDetail"); OnPropertyChanged("TotalPrice");
            });

            ClearCommand = new RelayCommand<object>((p) => SelectedItemCustomer != null, (p) =>
                {
                    SelectedItemCustomer = null;
                    DeliveryDate = DateTime.Now;
                }
            );

            ClearDetailCommand = new RelayCommand<object>((p) => SelectedItemProduct != null
                                                           || SelectedItemDriver != null
                                                           || Quantity != 0
                                                           || Price != 0
                                                           || Status != 0, (p) =>
                {
                    SelectedItem = null;
                    SelectedItemProduct = null;
                    SelectedItemDriver = null;
                    Quantity = 0;
                    Price = 0;
                    Status = 0;
                }
            );

            RefreshCommand = new RelayCommand<object>((p) => true,
                (p) =>
                {
                    OnPropertyChanged("TotalPrice");
                }
            );
        }

        FrameworkElement GetWindowParent(Window p)
        {
            FrameworkElement parent = p;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }
    }
}
