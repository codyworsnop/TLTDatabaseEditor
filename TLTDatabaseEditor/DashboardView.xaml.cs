// Decompiled with JetBrains decompiler
// Type: TLTDatabaseEditor.DashboardView
// Assembly: TLTDatabaseEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3AA8152-A60D-4A42-87BE-242C5FFCE9A0
// Assembly location: C:\Program Files (x86)\GizmoTron v1.5\TLTDatabaseEditor.exe

using MahApps.Metro.Controls.Dialogs;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace TLTDatabaseEditor
{
    public partial class DashboardView : UserControl
    {
        private DashboardViewModel _viewModel = new DashboardViewModel(DialogCoordinator.Instance);

        public DashboardView()
        {
            this.InitializeComponent();
            this.DataContext = (object)this._viewModel;
            this._viewModel.UpdateBuildingList += new DashboardViewModel.BuildingListUpdateDelegate(this.UpdateBuildingList);
            this._viewModel.UpdateRoomList += new DashboardViewModel.RoomListUpdateDelegate(this.UpdateRoomList);
            this._viewModel.UpdateDashboardLists += new DashboardViewModel.UpdateDashboardStatus(this.UpdateLists);
        }

        private void UpdateLists()
        {
            this.RoomDisabledListView.Items.Refresh();
            this.RoomEnabledListView.Items.Refresh();
            this.DisabledBuildingListView.Items.Refresh();
            this.EnabledBuildingListView.Items.Refresh();
            this.RemoveRoomTypeListView.Items.Refresh();
            this.AddRoomTypeListView.Items.Refresh();
            this.RemoveControlTypeListView.Items.Refresh();
            this.AddControlTypeListView.Items.Refresh();
            this.RemoveListView.Items.Refresh();
            this.AddListView.Items.Refresh();
        }

        private void UpdateRoomList(int itemIndex)
        {
            this.RoomDataGrid.SelectedIndex = itemIndex;
            this.RoomDataGrid.Items.Refresh();
        }

        private void UpdateBuildingList(int itemIndex)
        {
            this.BuildingsDataGrid.SelectedIndex = itemIndex;
            this.BuildingsDataGrid.Items.Refresh();
        }

        private void BuildingSelectionChangedHandler(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count <= 0)
                return;
            this._viewModel.BuildingSelected(((Building)e.AddedItems[0]).BuildingName.ToString());
            this.RoomDataGrid.Items.Refresh();
        }

        private void ClassroomSelectionChangedHandler(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count <= 0)
                return;
            this._viewModel.RoomSelected(((Classroom)e.AddedItems[0]).RoomNumber.ToString());
        }

        private void FeatureCheckedHandler(object sender, RoutedEventArgs e)
        {
            this._viewModel.FeatureChecked(((GridListItemViewModel<FeatureDesc>)((FrameworkElement)sender).DataContext).Data.Description);
            this.AddListView.Items.Refresh();
            this.RemoveListView.Items.Refresh();
        }

        private void FeatureUncheckedHandler(object sender, RoutedEventArgs e)
        {
            this._viewModel.FeatureUnchecked(((GridListItemViewModel<FeatureDesc>)((FrameworkElement)sender).DataContext).Data.Description);
            this.AddListView.Items.Refresh();
            this.RemoveListView.Items.Refresh();
        }

        private void CommitChangesHandler(object sender, RoutedEventArgs e) => this._viewModel.CommitChanges();

        private void AddBuildingHandler(object sender, RoutedEventArgs e) => this._viewModel.AddBuilding();

        private void AddRoomHandler(object sender, RoutedEventArgs e) => this._viewModel.AddClassroom();

        private void AddControlTypeHandler(object sender, RoutedEventArgs e) => this._viewModel.SetControlType(((Selector)sender).SelectedItem.ToString());

        private void AddRoomTypeHandler(object sender, RoutedEventArgs e) => this._viewModel.SetRoomType(((Selector)sender).SelectedItem.ToString());

        private void ControlTypeCheckedHandler(object sender, RoutedEventArgs e)
        {
            this._viewModel.ControlTypeChecked(((GridListItemViewModel<ControlType>)((FrameworkElement)sender).DataContext).Data.Description);
            this.RemoveControlTypeListView.Items.Refresh();
            this.AddControlTypeListView.Items.Refresh();
        }

        private void ControlTypeUncheckedHandler(object sender, RoutedEventArgs e)
        {
            this._viewModel.ControlTypeUnchecked(((GridListItemViewModel<ControlType>)((FrameworkElement)sender).DataContext).Data.Description);
            this.RemoveControlTypeListView.Items.Refresh();
            this.AddControlTypeListView.Items.Refresh();
        }

        private void TypeUncheckedHandler(object sender, RoutedEventArgs e)
        {
            this._viewModel.RoomTypeUnchecked(((GridListItemViewModel<TypeDesc>)((FrameworkElement)sender).DataContext).Data.Description);
            this.AddRoomTypeListView.Items.Refresh();
            this.RemoveRoomTypeListView.Items.Refresh();
        }

        private void TypeCheckedHandler(object sender, RoutedEventArgs e)
        {
            this._viewModel.RoomTypeChecked(((GridListItemViewModel<TypeDesc>)((FrameworkElement)sender).DataContext).Data.Description);
            this.AddRoomTypeListView.Items.Refresh();
            this.RemoveRoomTypeListView.Items.Refresh();
        }

        private void AddBuildingCodeTextChangedHandler(object sender, TextChangedEventArgs e) => this._viewModel.AddBuildingGreyed = this.AddBuildingNameText.Text != string.Empty && this.AddBuildingCodeText.Text != string.Empty;

        private void AddBuildingNameTextChangedHandler(object sender, TextChangedEventArgs e) => this._viewModel.AddBuildingGreyed = this.AddBuildingNameText.Text != string.Empty && this.AddBuildingCodeText.Text != string.Empty;

        private void AddRoomNameTextChangedHandler(object sender, TextChangedEventArgs e) => this._viewModel.AddRoomGreyed = this.AddRoomNameText.Text != string.Empty && this.AddRoomControlTypeCombo.SelectedIndex != -1 && this.AddRoomTypeCombo.SelectedIndex != -1;

        private void AddControlTypeHandler(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count <= 0)
                return;
            this._viewModel.AddRoomGreyed = this.AddRoomNameText.Text != string.Empty && this.AddRoomControlTypeCombo.SelectedIndex != -1 && this.AddRoomTypeCombo.SelectedIndex != -1;
            this._viewModel.SetControlType(e.AddedItems[0].ToString());
        }

        private void AddRoomTypeHandler(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count <= 0)
                return;
            this._viewModel.AddRoomGreyed = this.AddRoomNameText.Text != string.Empty && this.AddRoomControlTypeCombo.SelectedIndex != -1 && this.AddRoomTypeCombo.SelectedIndex != -1;
            this._viewModel.SetRoomType(e.AddedItems[0].ToString());
        }

        private void BuildingUncheckedHandler(object sender, RoutedEventArgs e)
        {
            this._viewModel.BuildingDisabled(((Building)((FrameworkElement)e.Source).DataContext).BuildingName);
            this.EnabledBuildingListView.Items.Refresh();
            this.DisabledBuildingListView.Items.Refresh();
        }

        private void BuildingCheckedHandler(object sender, RoutedEventArgs e)
        {
            this._viewModel.BuildingEnabled(((Building)((FrameworkElement)e.Source).DataContext).BuildingName);
            this.EnabledBuildingListView.Items.Refresh();
            this.DisabledBuildingListView.Items.Refresh();
        }

        private void ClassroomCheckedHandler(object sender, RoutedEventArgs e)
        {
            this._viewModel.RoomEnabled(((Classroom)((FrameworkElement)e.Source).DataContext).RoomNumber);
            this.RoomEnabledListView.Items.Refresh();
            this.RoomDisabledListView.Items.Refresh();
        }

        private void ClassroomUncheckedHandler(object sender, RoutedEventArgs e)
        {
            this._viewModel.RoomDisabled(((Classroom)((FrameworkElement)e.Source).DataContext).RoomNumber);
            this.RoomEnabledListView.Items.Refresh();
            this.RoomDisabledListView.Items.Refresh();
        }

        private void ResetChangesHandler(object sender, RoutedEventArgs e) => this._viewModel.ResetChanges();
    }
}
