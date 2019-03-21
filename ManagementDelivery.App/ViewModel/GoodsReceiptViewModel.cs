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
    public class GoodsReceiptViewModel : ViewModelBase
    {
        private ObservableCollection<GoodsReceipt> _list;
        public ObservableCollection<GoodsReceipt> List { get => _list; set { _list = value; OnPropertyChanged(); } }

        private ObservableCollection<Product> _listProduct;
        public ObservableCollection<Product> ListProduct { get => _listProduct; set { _listProduct = value; OnPropertyChanged(); } }

        private ObservableCollection<Supplier> _listSupplier;
        public ObservableCollection<Supplier> ListSupplier { get => _listSupplier; set { _listSupplier = value; OnPropertyChanged(); } }

        private GoodsReceipt _selectedItem;
        public GoodsReceipt SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    PurchasePrice = SelectedItem.PurchasePrice;
                    Quantity = SelectedItem.Quantity;
                    DateReceipt = SelectedItem.DateReceipt;
                    SelectedItemProduct = SelectedItem.Product;
                    SelectedItemSupplier = SelectedItem.Supplier;
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

        private Supplier _selectedItemSupplier;
        public Supplier SelectedItemSupplier
        {
            get => _selectedItemSupplier;
            set
            {
                _selectedItemSupplier = value;
                OnPropertyChanged();

                if (value != null) SupplierId = value.Id;
            }
        }

        private int ProductId { get; set; }
        private int SupplierId { get; set; }

        private decimal _purchasePrice;
        public decimal PurchasePrice
        {
            get => _purchasePrice;
            set
            {
                _purchasePrice = value;
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


        private DateTime _dateReceipt;
        public DateTime DateReceipt
        {
            get => _dateReceipt;
            set
            {
                _dateReceipt = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        public GoodsReceiptViewModel()
        {
            List = new ObservableCollection<GoodsReceipt>(DataProvider.Ins.DB.GoodsReceipts.OrderByDescending(x => x.UpdateAt));

            ListProduct = new ObservableCollection<Product>(DataProvider.Ins.DB.Products.Where(x => !x.IsDelete).OrderByDescending(x => x.UpdateAt));

            ListSupplier = new ObservableCollection<Supplier>(DataProvider.Ins.DB.Suppliers.Where(x => !x.IsDelete).OrderByDescending(x => x.UpdateAt));

            DateReceipt = DateTime.Now;

            AddCommand = new RelayCommand<object>((p) => SelectedItemProduct != null && PurchasePrice > 0 && Quantity > 0, (p) =>
            {
                var goodsReceipt = new GoodsReceipt()
                {
                    ProductId = ProductId,
                    SupplierId = SupplierId,
                    PurchasePrice = PurchasePrice,
                    Quantity = Quantity,
                    DateReceipt = DateReceipt,
                    InsertAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };

                DataProvider.Ins.DB.GoodsReceipts.Add(goodsReceipt);
                DataProvider.Ins.DB.SaveChanges();

                List.Insert(0, goodsReceipt);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                return SelectedItem != null && DataProvider.Ins.DB.GoodsReceipts.Any(x => x.Id == SelectedItem.Id);
            }, (p) =>
            {
                var goodsReceipt = DataProvider.Ins.DB.GoodsReceipts.FirstOrDefault(x => x.Id == SelectedItem.Id);
                if (goodsReceipt != null)
                {
                    goodsReceipt.PurchasePrice = PurchasePrice;
                    goodsReceipt.Quantity = Quantity;
                    goodsReceipt.DateReceipt = DateReceipt;

                    goodsReceipt.UpdateAt = DateTime.Now;

                    DataProvider.Ins.DB.SaveChanges();
                }
            });

            ClearCommand = new RelayCommand<object>((p) => SelectedItemProduct != null || SelectedItemSupplier != null || PurchasePrice != 0 || Quantity != 0, (p) =>
            {
                SelectedItem = null;
                SelectedItemProduct = null;
                SelectedItemSupplier = null;
                PurchasePrice = 0;
                Quantity = 0;
                DateReceipt = DateTime.Now;
            }
            );

            RefreshCommand = new RelayCommand<object>((p) => true,
                (p) =>
                {
                    List = new ObservableCollection<GoodsReceipt>(DataProvider.Ins.DB.GoodsReceipts.OrderByDescending(x => x.UpdateAt));
                }
            );
        }
    }
}
