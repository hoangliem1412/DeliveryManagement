using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ManagementDelivery.Model;

namespace ManagementDelivery.App.ViewModel
{
    public class ProductViewModel : ViewModelBase
    {
        private ObservableCollection<Product> _list;
        public ObservableCollection<Product> List { get => _list; set { _list = value; OnPropertyChanged(); } }

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
                    CategoryName = SelectedItem.Category.Name;
                    SelectedItemCategory = SelectedItem.Category;
                    Price = SelectedItem.Price;
                    PurchasePrice = SelectedItem.PurchasePrice;
                    Description = SelectedItem.Description;
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
        
        public int CategoryId { get; set; }

        private string _name;
        public string Name { get; set; }

        private string _categoryName;
        public string CategoryName { get; set; }

        private decimal _price;
        public decimal? Price { get; set; }

        private decimal _purchasePrice;
        public decimal? PurchasePrice { get; set; }

        private string _description;
        public string Description { get; set; }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ProductViewModel()
        {
            List = new ObservableCollection<Product>(DataProvider.Ins.DB.Products.Where(x => !x.IsDelete));
            ListCategory = new ObservableCollection<ProductCategory>(DataProvider.Ins.DB.Categories.Where(x => !x.IsDelete));

            AddCommand = new RelayCommand<object>((p) => true, (p) =>
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

                List.Add(product);
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
                var product = DataProvider.Ins.DB.Products.FirstOrDefault(x => x.Id == SelectedItem.Id);
                if (product != null)
                {
                    product.IsDelete = true;
                    DataProvider.Ins.DB.SaveChanges();
                    List.Remove(product);
                }

                SelectedItem = null;
            });
        }
    }
}
