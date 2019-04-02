using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ManagementDelivery.App.Core;
using ManagementDelivery.App.Model;

namespace ManagementDelivery.App.ViewModel
{
    public class DriverViewModel : ViewModelBase
    {
        private ObservableCollection<Driver> _list;
        public ObservableCollection<Driver> List { get => _list; set { _list = value; OnPropertyChanged(); } }

        private Driver _selectedItem;
        public Driver SelectedItem
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

        private string _phone;
        public string Phone { get => _phone; set { _phone = value; OnPropertyChanged(); } }

        private string _address;
        public string Address { get => _address; set { _address = value; OnPropertyChanged(); } }

        private string _moreInfo;
        public string MoreInfo { get => _moreInfo; set { _moreInfo = value; OnPropertyChanged(); } }

        private string _note;
        public string Note { get => _note; set { _note = value; OnPropertyChanged(); } }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        public DriverViewModel()
        {
            List = new ObservableCollection<Driver>(DataProvider.Ins.DB.Drivers.Where(x => !x.IsDelete).OrderByDescending(x => x.UpdateAt));

            AddCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                var driver = new Driver()
                {
                    Name = Name,
                    Phone = Phone,
                    Address = Address,
                    MoreInfo = MoreInfo,
                    InsertAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };

                DataProvider.Ins.DB.Drivers.Add(driver);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(driver);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                return SelectedItem != null && DataProvider.Ins.DB.Drivers.Any(x => x.Id == SelectedItem.Id);
            }, (p) =>
            {
                var driver = DataProvider.Ins.DB.Drivers.FirstOrDefault(x => x.Id == SelectedItem.Id);
                if (driver != null)
                {
                    driver.Name = Name;
                    driver.Phone = Phone;
                    driver.Address = Address;
                    driver.MoreInfo = MoreInfo;
                    driver.Note = Note;
                    driver.UpdateAt = DateTime.Now;

                    DataProvider.Ins.DB.SaveChanges();
                }
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return SelectedItem != null && DataProvider.Ins.DB.Drivers.Any(x => x.Id == SelectedItem.Id);
            }, (p) =>
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Xác nhận", MessageBoxButton.YesNo);
                if (messageBoxResult != MessageBoxResult.Yes)
                {
                    return;
                }

                var driver = DataProvider.Ins.DB.Drivers.FirstOrDefault(x => x.Id == SelectedItem.Id);
                if (driver != null)
                {
                    DataProvider.Ins.DB.Drivers.Remove(driver);
                    DataProvider.Ins.DB.SaveChanges();
                    List.Remove(driver);
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
                    List = new ObservableCollection<Driver>(DataProvider.Ins.DB.Drivers.Where(x => !x.IsDelete).OrderByDescending(x => x.UpdateAt));
                }
            );
        }
    }
}
