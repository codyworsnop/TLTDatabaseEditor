using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TLTDatabaseEditor
{
    /// <summary>
    /// Interaction logic for NavigationViewModel
    /// </summary>
    public class NavigationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand RoomCommand { get; set; }

        public ICommand SearchCommand { get; set; }

        public object SelectedViewModel
        {
            get
            {
                return _selectedViewModel;
            }

            set
            {
                _selectedViewModel = value;
                OnPropertyChanged("SelectedViewModel");
            }
        }

        private object _selectedViewModel;

        private void OpenRoom(object obj)
        {
            SelectedViewModel = new RoomViewModel();
        }

        private void OpenSearch(object obj)
        {
            SelectedViewModel = new SearchViewModel();
        }

        public NavigationViewModel()
        {

            SearchCommand = new RelayCommand(x =>
            {
                OpenSearch(x);

            }, x => true);

            RoomCommand = new RelayCommand(x =>
            {
                OpenRoom(x);

            }, x => true);
        }

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
