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
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Shapes;

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
            var updatedFeature = ((RoomFeatureDataItemViewModel)((CheckBox)sender).DataContext).Feature.Description;
            _viewModel.FeatureChecked(updatedFeature);
            AddListView.Items.Refresh();
            RemoveListView.Items.Refresh();
        }

        private void FeatureUncheckedHandler(object sender, RoutedEventArgs e)
        {
            var updatedFeature = ((RoomFeatureDataItemViewModel)((CheckBox)sender).DataContext).Feature.Description;
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

        private void AddControlTypeHandler(object sender, RoutedEventArgs e)
        {
            _viewModel.SetControlType(((ComboBox)sender).SelectedItem.ToString());
        }

        private void AddRoomTypeHandler(object sender, RoutedEventArgs e)
        {
            _viewModel.SetRoomType(((ComboBox)sender).SelectedItem.ToString());
        }

        private void ControlTypeCheckedHandler(object sender, RoutedEventArgs e)
        {
            var updatedFeature = ((ControlTypeViewModel)((RadioButton)sender).DataContext).Type.Description;
            _viewModel.ControlTypeChecked(updatedFeature);
            RemoveControlTypeListView.Items.Refresh();
            AddControlTypeListView.Items.Refresh();
        }

        private void ControlTypeUncheckedHandler(object sender, RoutedEventArgs e)
        {
            var updatedFeature = ((ControlTypeViewModel)((RadioButton)sender).DataContext).Type.Description;
            _viewModel.ControlTypeUnchecked(updatedFeature);
            RemoveControlTypeListView.Items.Refresh();
            AddControlTypeListView.Items.Refresh();
        }

        private void TypeUncheckedHandler(object sender, RoutedEventArgs e)
        {
            var updatedFeature = ((RoomTypeViewModel)((RadioButton)sender).DataContext).Type.Description;
            _viewModel.RoomTypeUnchecked(updatedFeature);
            AddRoomTypeListView.Items.Refresh();
            RemoveRoomTypeListView.Items.Refresh();
        }

        private void TypeCheckedHandler(object sender, RoutedEventArgs e)
        {
            var updatedFeature = ((RoomTypeViewModel)((RadioButton)sender).DataContext).Type.Description;
            _viewModel.RoomTypeChecked(updatedFeature);
            AddRoomTypeListView.Items.Refresh();
            RemoveRoomTypeListView.Items.Refresh();
        }
    }
}
