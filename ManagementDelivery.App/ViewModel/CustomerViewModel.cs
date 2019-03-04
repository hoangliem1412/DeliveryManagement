using ManagementDelivery.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ManagementDelivery.App.ViewModel
{
    public class CustomerViewModel : ViewModelBase
    {
        private ObservableCollection<Customer> _list;
        public ObservableCollection<Customer> List { get => _list; set { _list = value; OnPropertyChanged(); } }

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
                }
            }
        }

        private string _name;
        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }


        private string _phone;
        public string Phone { get => _phone; set { _phone = value; OnPropertyChanged(); } }


        private string _address;
        public string Address { get => _address; set { _address = value; OnPropertyChanged(); } }


        private string _moreInfo;
        public string MoreInfo { get => _moreInfo; set { _moreInfo = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public CustomerViewModel()
        {
            List = new ObservableCollection<Customer>(DataProvider.Ins.DB.Customers);
            List.CollectionChanged += List_CollectionChanged;

            AddCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                var customer = new Customer() { Name = Name, Phone = Phone, Address = Address, MoreInfo = MoreInfo };

                DataProvider.Ins.DB.Customers.Add(customer);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(customer);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                return SelectedItem != null && DataProvider.Ins.DB.Customers.Any(x => x.Id == SelectedItem.Id);
            }, (p) =>
            {
                var customer = DataProvider.Ins.DB.Customers.FirstOrDefault(x => x.Id == SelectedItem.Id);
                if (customer != null)
                {
                    customer.Name = Name;
                    customer.Phone = Phone;
                    customer.Address = Address;
                    customer.MoreInfo = MoreInfo;

                    DataProvider.Ins.DB.SaveChanges();

                    OnPropertyChanged("List");
                }
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return SelectedItem != null && DataProvider.Ins.DB.Customers.Any(x => x.Id == SelectedItem.Id);
            }, (p) =>
            {
                var customer = DataProvider.Ins.DB.Customers.FirstOrDefault(x => x.Id == SelectedItem.Id);
                if (customer != null)
                {
                    DataProvider.Ins.DB.Customers.Remove(customer);
                    DataProvider.Ins.DB.SaveChanges();
                    List.Remove(customer);
                }

                SelectedItem = null;
            });
        }

        private void List_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
