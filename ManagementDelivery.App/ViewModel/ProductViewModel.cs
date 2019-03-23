using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ManagementDelivery.Model;

namespace ManagementDelivery.App.ViewModel
{
    public class ProductViewModel : ViewModelBase
    {
        private ObservableCollection<Product> _listProduct;
        public ObservableCollection<Product> ListProduct { get => _listProduct; set { _listProduct = value; OnPropertyChanged(); } }

        private ObservableCollection<ProductCategory> _listCategories;
        public ObservableCollection<ProductCategory> ListCategory { get => _listCategories; set { _listCategories = value; OnPropertyChanged(); } }

        private Product _selectedItem;
        public Product SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    Name = SelectedItem.Name;
                    Price = SelectedItem.Price;
                    PurchasePrice = SelectedItem.PurchasePrice;
                    Description = SelectedItem.Description;
                    SelectedItemCategory = SelectedItem.Category;
                }
            }
        }

        private ProductCategory _selectedItemCategory;
        public ProductCategory SelectedItemCategory
        {
            get => _selectedItemCategory;
            set
            {
                _selectedItemCategory = value;
                OnPropertyChanged();

                if (value != null) CategoryId = value.Id;
            }
        }
        
        private int CategoryId { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
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

        private decimal? _purchasePrice;
        public decimal? PurchasePrice
        {
            get => _purchasePrice;
            set
            {
                _purchasePrice = value;
                OnPropertyChanged();
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        public ProductViewModel()
        {
            ListProduct = new ObservableCollection<Product>(DataProvider.Ins.DB.Products.Where(x => !x.IsDelete).OrderByDescending(x => x.UpdateAt));
            ListCategory = new ObservableCollection<ProductCategory>(DataProvider.Ins.DB.Categories.Where(x => !x.IsDelete).OrderByDescending(x => x.UpdateAt));

            AddCommand = new RelayCommand<object>((p) => !string.IsNullOrEmpty(Name), (p) =>
            {
                var product = new Product()
                {
                    Name = Name,
                    CategoryId = CategoryId,
                    Price = Price,
                    PurchasePrice = PurchasePrice,
                    Description = Description,
                    InsertAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };

                DataProvider.Ins.DB.Products.Add(product);
                DataProvider.Ins.DB.SaveChanges();

                ListProduct.Insert(0, product);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                return SelectedItem != null && DataProvider.Ins.DB.Products.Any(x => x.Id == SelectedItem.Id);
            }, (p) =>
            {
                var product = DataProvider.Ins.DB.Products.FirstOrDefault(x => x.Id == SelectedItem.Id);
                if (product != null)
                {
                    product.Name = Name;
                    product.CategoryId = CategoryId;
                    product.Price = Price;
                    product.PurchasePrice = PurchasePrice;
                    product.Description = Description;
                    product.UpdateAt = DateTime.Now;

                    DataProvider.Ins.DB.SaveChanges();
                }
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return SelectedItem != null && DataProvider.Ins.DB.Products.Any(x => x.Id == SelectedItem.Id);
            }, (p) =>
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Xác nhận", MessageBoxButton.YesNo);
                if (messageBoxResult != MessageBoxResult.Yes)
                {
                    return;
                }

                var product = DataProvider.Ins.DB.Products.FirstOrDefault(x => x.Id == SelectedItem.Id);
                if (product != null)
                {
                    product.IsDelete = true;
                    DataProvider.Ins.DB.SaveChanges();
                    ListProduct.Remove(product);
                }

                SelectedItem = null;
            });

            ClearCommand = new RelayCommand<object> ((p) => SelectedItemCategory != null || !string.IsNullOrEmpty(Name) || Price <= 0 || PurchasePrice.HasValue || !string.IsNullOrEmpty(Description), (p) =>
                {
                    SelectedItem = null;
                    SelectedItemCategory = null;
                    Name = string.Empty;
                    Price = 0;
                    PurchasePrice = null;
                    Description = string.Empty;
                }
            );

            RefreshCommand = new RelayCommand<object> ((p) => true, 
                (p) =>
                {
                    ListProduct = new ObservableCollection<Product>(DataProvider.Ins.DB.Products.Where(x => !x.IsDelete).OrderByDescending(x => x.UpdateAt));
                }
            );
        }
    }
}
