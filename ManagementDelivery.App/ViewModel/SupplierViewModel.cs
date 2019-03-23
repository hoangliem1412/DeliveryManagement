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
    public class SupplierViewModel : ViewModelBase
    {
        private ObservableCollection<Supplier> _list;
        public ObservableCollection<Supplier> List { get => _list; set { _list = value; OnPropertyChanged(); } }

        private Supplier _selectedItem;
        public Supplier SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    Name = SelectedItem.Name;
                    Phone = SelectedItem.Phone;
                    Address = SelectedItem.Address;
                    MoreInfo = SelectedItem.MoreInfo;
                    Note = SelectedItem.Note;
                }
            }
        }

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

        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }

        private string _moreInfo;
        public string MoreInfo
        {
            get => _moreInfo;
            set
            {
                _moreInfo = value;
                OnPropertyChanged();
            }
        }

        private string _note;
        public string Note
        {
            get => _note;
            set
            {
                _note = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        public SupplierViewModel()
        {
            List = new ObservableCollection<Supplier>(DataProvider.Ins.DB.Suppliers.Where(x => !x.IsDelete).OrderByDescending(x => x.UpdateAt));

            AddCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                var supplier = new Supplier()
                {
                    Name = Name,
                    Phone = Phone,
                    Address = Address,
                    Note = Note,
                    MoreInfo = MoreInfo,
                    InsertAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };

                DataProvider.Ins.DB.Suppliers.Add(supplier);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(supplier);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                return SelectedItem != null && DataProvider.Ins.DB.Suppliers.Any(x => x.Id == SelectedItem.Id);
            }, (p) =>
            {
                var supplier = DataProvider.Ins.DB.Suppliers.FirstOrDefault(x => x.Id == SelectedItem.Id);
                if (supplier != null)
                {
                    supplier.Name = Name;
                    supplier.Phone = Phone;
                    supplier.Address = Address;
                    supplier.MoreInfo = MoreInfo;
                    supplier.Note = Note;
                    supplier.UpdateAt = DateTime.Now;
                    DataProvider.Ins.DB.SaveChanges();
                }
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return SelectedItem != null && DataProvider.Ins.DB.Categories.Any(x => x.Id == SelectedItem.Id);
            }, (p) =>
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Xác nhận", MessageBoxButton.YesNo);
                if (messageBoxResult != MessageBoxResult.Yes)
                {
                    return;
                }

                var supplier = DataProvider.Ins.DB.Suppliers.FirstOrDefault(x => x.Id == SelectedItem.Id);
                if (supplier != null)
                {
                    DataProvider.Ins.DB.Suppliers.Remove(supplier);
                    DataProvider.Ins.DB.SaveChanges();
                    List.Remove(supplier);
                }

                SelectedItem = null;
            });


            ClearCommand = new RelayCommand<object>((p) => !string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(Phone) || !string.IsNullOrEmpty(Address) || !string.IsNullOrEmpty(MoreInfo) || !string.IsNullOrEmpty(Note), (p) =>
                {
                    SelectedItem = null;
                    Name = null;
                    Phone = null;
                    Address = null;
                    MoreInfo = null;
                    Note = null;
                }
            );

            RefreshCommand = new RelayCommand<object>((p) => true,
                (p) =>
                {
                    List = new ObservableCollection<Supplier>(DataProvider.Ins.DB.Suppliers.Where(x => !x.IsDelete).OrderByDescending(x => x.UpdateAt));
                }
            );
        }
    }
}
