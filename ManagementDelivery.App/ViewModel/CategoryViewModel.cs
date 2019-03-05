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
    public class CategoryViewModel : ViewModelBase
    {
        private ObservableCollection<Category> _list;
        public ObservableCollection<Category> List { get => _list; set { _list = value; OnPropertyChanged(); } }

        private Category _selectedItem;
        public Category SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    Name = SelectedItem.Name;
                    Note = SelectedItem.Note;
                }
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        private string _note;
        public string Note
        {
            get => _note;
            set { _note = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public CategoryViewModel()
        {
            List = new ObservableCollection<Category>(DataProvider.Ins.DB.Categories.Where(x => !x.IsDelete));

            AddCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                var category = new Category() { Name = Name, Note = Note };

                DataProvider.Ins.DB.Categories.Add(category);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(category);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                return SelectedItem != null && DataProvider.Ins.DB.Categories.Any(x => x.Id == SelectedItem.Id);
            }, (p) =>
            {
                var category = DataProvider.Ins.DB.Categories.FirstOrDefault(x => x.Id == SelectedItem.Id);
                if (category != null)
                {
                    category.Name = Name;
                    category.Note = Note;

                    DataProvider.Ins.DB.SaveChanges();
                }
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                return SelectedItem != null && DataProvider.Ins.DB.Categories.Any(x => x.Id == SelectedItem.Id);
            }, (p) =>
            {
                var category = DataProvider.Ins.DB.Categories.FirstOrDefault(x => x.Id == SelectedItem.Id);
                if (category != null)
                {
                    category.IsDelete = true;
                    DataProvider.Ins.DB.SaveChanges();
                    List.Remove(category);
                }

                SelectedItem = null;
            });
        }
    }
}
