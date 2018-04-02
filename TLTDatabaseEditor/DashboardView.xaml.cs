using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        DashboardViewModel _viewModel = new DashboardViewModel();

        public DashboardView()
        {
            InitializeComponent();
            this.DataContext = _viewModel;
        }

        private void BuildingSelectionChangedHandler(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.BuildingSelected(e.AddedItems[0].ToString());
        }

        private void ClassroomSelectionChangedHandler(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                _viewModel.RoomSelected(e.AddedItems[0].ToString());
            }
        }
    }
}
