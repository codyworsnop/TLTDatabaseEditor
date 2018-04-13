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
using MahApps.Metro.Controls.Dialogs;

namespace TLTDatabaseEditor
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        DashboardViewModel _viewModel = new DashboardViewModel(DialogCoordinator.Instance);

        public DashboardView()
        {
            InitializeComponent();
            this.DataContext = _viewModel;
        }

        private void BuildingSelectionChangedHandler(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.BuildingSelected(((Building)e.AddedItems[0]).BuildingName.ToString());
        }

        private void ClassroomSelectionChangedHandler(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                _viewModel.RoomSelected(((Classroom)e.AddedItems[0]).RoomNumber.ToString());
            }

            _viewModel.ClearFeatureChanges();
        }

        private void FeatureCheckedHandler(object sender, RoutedEventArgs e)
        {
            var updatedFeature = ((RoomFeatureIDataItemViewModel)((CheckBox)sender).DataContext).Feature.Description;
            _viewModel.FeatureChecked(updatedFeature);
            AddListView.Items.Refresh();
            RemoveListView.Items.Refresh();
        }

        private void FeatureUncheckedHandler(object sender, RoutedEventArgs e)
        {
            var updatedFeature = ((RoomFeatureIDataItemViewModel)((CheckBox)sender).DataContext).Feature.Description;
            _viewModel.FeatureUnchecked(updatedFeature);
            AddListView.Items.Refresh();
            RemoveListView.Items.Refresh();
        }

        private void CommitChangesHandler(object sender, RoutedEventArgs e)
        {
            _viewModel.CommitChanges();

            BuildingsDataGrid.Items.Refresh();
            RoomDataGrid.Items.Refresh();
        }

        private void AddBuildingHandler(object sender, RoutedEventArgs e)
        {
            _viewModel.AddBuilding();
        }

        private void AddRoomHandler(object sender, RoutedEventArgs e)
        {
            _viewModel.AddClassroom();
        }
    }
}
