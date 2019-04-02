using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ManagementDelivery.App.Core;
using ManagementDelivery.App.Model;

namespace ManagementDelivery.App.ViewModel
{
    public class CustomerViewModel : ViewModelBase
    {
        private ObservableCollection<Customer> _listCustomer;
        public ObservableCollection<Customer> ListCustomer { get => _listCustomer; set { _listCustomer = value; OnPropertyChanged(); } }

        private Customer _selectedItem;
        public Customer SelectedItem
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
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }


        private string _address;
        public string Address { get => _address; set { _address = value; OnPropertyChanged(); } }

        private string _phone;
        public string Phone { get => _phone; set { _phone = value; OnPropertyChanged(); } }


        private string _moreInfo;
        public string MoreInfo { get => _moreInfo; set { _moreInfo = value; OnPropertyChanged(); } }

        private string _note;
        public string Note { get => _note; set { _note = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        public CustomerViewModel()
        {
            ListCustomer = new ObservableCollection<Customer>(DataProvider.Ins.DB.Customers.Where(x => !x.IsDelete).OrderByDescending(x => x.UpdateAt));

            AddCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                var customer = new Customer()
                {
                    Name = Name,
                    Phone = Phone,
                    Address = Address,
                    MoreInfo = MoreInfo,
                    InsertAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };

                DataProvider.Ins.DB.Customers.Add(customer);
                DataProvider.Ins.DB.SaveChanges();

                ListCustomer.Add(customer);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                return SelectedItem != null && DataProvider.Ins.DB.Customers.Any(x => x.Id == SelectedItem.Id && !x.IsDelete);
            }, (p) =>
            {
                var customer = DataProvider.Ins.DB.Customers.FirstOrDefault(x => x.Id == SelectedItem.Id && !x.IsDelete);
                if (customer != null)
                {
                    customer.Name = Name;
                    customer.Phone = Phone;
                    customer.Address = Address;
                    customer.MoreInfo = MoreInfo;
                    customer.Note = Note;
                    customer.UpdateAt = DateTime.Now;

                    DataProvider.Ins.DB.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Not Found. Please again");
                }
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return SelectedItem != null && DataProvider.Ins.DB.Customers.Any(x => x.Id == SelectedItem.Id && !x.IsDelete);
            }, (p) =>
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Xác nhận", MessageBoxButton.YesNo);
                if (messageBoxResult != MessageBoxResult.Yes)
                {
                    return;
                }
                    var customer = DataProvider.Ins.DB.Customers.FirstOrDefault(x => x.Id == SelectedItem.Id && !x.IsDelete);
                if (customer != null)
                {
                    DataProvider.Ins.DB.Customers.Remove(customer);
                    DataProvider.Ins.DB.SaveChanges();
                    ListCustomer.Remove(customer);
                }
                else
                {
                    MessageBox.Show("Not Found. Please again");
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
                    ListCustomer = new ObservableCollection<Customer>(DataProvider.Ins.DB.Customers.Where(x => !x.IsDelete).OrderByDescending(x => x.UpdateAt));
                }
            );
        }
    }
}
