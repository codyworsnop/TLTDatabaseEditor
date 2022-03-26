// Decompiled with JetBrains decompiler
// Type: TLTDatabaseEditor.DashboardViewModel
// Assembly: TLTDatabaseEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3AA8152-A60D-4A42-87BE-242C5FFCE9A0
// Assembly location: C:\Program Files (x86)\GizmoTron v1.5\TLTDatabaseEditor.exe

using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TLTDatabaseEditor
{
    public class DashboardViewModel : ItemViewModel
    {
        private DashboardModel _model = new DashboardModel();
        private bool _addRoomGreyed;
        private bool _addBuildingGreyed;
        private List<Building> _dbBuildings;
        private List<Classroom> _dbClassrooms;
        private List<GridListItemViewModel<FeatureDesc>> _roomFeatures;
        private List<string> _roomDescriptionAdds;
        private List<string> _roomDescriptionRemoves;
        private List<string> _controlTypeAdds;
        private List<string> _controlTypeRemoves;
        private List<string> _roomTypeAdds;
        private List<string> _roomTypeRemoves;
        private List<string> _roomEnables;
        private List<string> _roomDisables;
        private List<string> _buildingEnables;
        private List<string> _buildingDisables;
        private List<string> _roomTypes;
        private List<string> _roomControlTypes;
        private List<GridListItemViewModel<ControlType>> _gridControlTypes;
        private List<GridListItemViewModel<TypeDesc>> _gridTypeDescs;
        private IDialogCoordinator _dialogCoordinator;
        private string _selectedBuilding;
        private string _roomNumberSelected;
        private string _addBuildingName;
        private string _addBuildingCode;
        private string _addRoomName;
        private string _selectedAddRoomType;
        private string _selectedAddControlType;
        private bool _addBuildingIsEnabled;
        private bool _addRoomIsEnabled;

        public event DashboardViewModel.BuildingListUpdateDelegate UpdateBuildingList;

        public event DashboardViewModel.RoomListUpdateDelegate UpdateRoomList;

        public event DashboardViewModel.UpdateDashboardStatus UpdateDashboardLists;

        public DashboardViewModel(string tabName)
          : base(tabName)
        {
        }

        public DashboardViewModel(IDialogCoordinator instance)
          : base("Empty")
        {
            this._dialogCoordinator = instance;
            try
            {
                this.DBBuildings = this._model.GetBuildingNames();
            }
            finally
            {
                this.RoomDescriptionAdds = new List<string>();
                this.RoomDescriptionRemoves = new List<string>();
                this.ControlTypeAdds = new List<string>();
                this.ControlTypeRemoves = new List<string>();
                this.RoomTypeAdds = new List<string>();
                this.RoomTypeRemoves = new List<string>();
                this.BuildingEnables = new List<string>();
                this.BuildingDisables = new List<string>();
                this.RoomEnables = new List<string>();
                this.RoomDisables = new List<string>();
                try
                {
                    this.RoomControlTypes = this._model.GetRoomControlTypes();
                    this.RoomTypes = this._model.GetRoomTypes();
                }
                finally
                {
                    this.AddRoomGreyed = false;
                    this.AddBuildingGreyed = false;
                    this._model.sqlExceptionThrown += new DashboardModel.SqlExceptionDelegate(this.HandleSqlException);
                }
            }
        }

        public bool AddRoomGreyed
        {
            get => this._addRoomGreyed;
            set => this.SetProperty<bool>(ref this._addRoomGreyed, value, nameof(AddRoomGreyed));
        }

        public bool AddBuildingGreyed
        {
            get => this._addBuildingGreyed;
            set => this.SetProperty<bool>(ref this._addBuildingGreyed, value, nameof(AddBuildingGreyed));
        }

        public List<GridListItemViewModel<ControlType>> GridControlTypes
        {
            get => this._gridControlTypes;
            set => this.SetProperty<List<GridListItemViewModel<ControlType>>>(ref this._gridControlTypes, value, nameof(GridControlTypes));
        }

        public List<GridListItemViewModel<TypeDesc>> GridTypeDescs
        {
            get => this._gridTypeDescs;
            set => this.SetProperty<List<GridListItemViewModel<TypeDesc>>>(ref this._gridTypeDescs, value, nameof(GridTypeDescs));
        }

        public List<string> RoomControlTypes
        {
            get => this._roomControlTypes;
            set => this.SetProperty<List<string>>(ref this._roomControlTypes, value, nameof(RoomControlTypes));
        }

        public List<string> RoomTypes
        {
            get => this._roomTypes;
            set => this.SetProperty<List<string>>(ref this._roomTypes, value, nameof(RoomTypes));
        }

        public bool AddBuildingIsEnabled
        {
            get => this._addBuildingIsEnabled;
            set => this.SetProperty<bool>(ref this._addBuildingIsEnabled, value, nameof(AddBuildingIsEnabled));
        }

        public bool AddRoomIsEnabled
        {
            get => this._addRoomIsEnabled;
            set => this.SetProperty<bool>(ref this._addRoomIsEnabled, value, nameof(AddRoomIsEnabled));
        }

        public string AddRoomName
        {
            get => this._addRoomName;
            set => this.SetProperty<string>(ref this._addRoomName, value, nameof(AddRoomName));
        }

        public string AddBuildingName
        {
            get => this._addBuildingName;
            set => this.SetProperty<string>(ref this._addBuildingName, value, nameof(AddBuildingName));
        }

        public string AddBuildingCode
        {
            get => this._addBuildingCode;
            set => this.SetProperty<string>(ref this._addBuildingCode, value, nameof(AddBuildingCode));
        }

        public List<string> RoomTypeAdds
        {
            get => this._roomTypeAdds;
            set => this.SetProperty<List<string>>(ref this._roomTypeAdds, value, nameof(RoomTypeAdds));
        }

        public List<string> RoomTypeRemoves
        {
            get => this._roomTypeRemoves;
            set => this.SetProperty<List<string>>(ref this._roomTypeRemoves, value, nameof(RoomTypeRemoves));
        }

        public List<string> ControlTypeAdds
        {
            get => this._controlTypeAdds;
            set => this.SetProperty<List<string>>(ref this._controlTypeAdds, value, nameof(ControlTypeAdds));
        }

        public List<string> BuildingDisables
        {
            get => this._buildingDisables;
            set => this.SetProperty<List<string>>(ref this._buildingDisables, value, nameof(BuildingDisables));
        }

        public List<string> BuildingEnables
        {
            get => this._buildingEnables;
            set => this.SetProperty<List<string>>(ref this._buildingEnables, value, nameof(BuildingEnables));
        }

        public List<string> RoomDisables
        {
            get => this._roomDisables;
            set => this.SetProperty<List<string>>(ref this._roomDisables, value, nameof(RoomDisables));
        }

        public List<string> RoomEnables
        {
            get => this._roomEnables;
            set => this.SetProperty<List<string>>(ref this._roomEnables, value, nameof(RoomEnables));
        }

        public List<string> ControlTypeRemoves
        {
            get => this._controlTypeRemoves;
            set => this.SetProperty<List<string>>(ref this._controlTypeRemoves, value, nameof(ControlTypeRemoves));
        }

        public List<string> RoomDescriptionAdds
        {
            get => this._roomDescriptionAdds;
            set => this.SetProperty<List<string>>(ref this._roomDescriptionAdds, value, nameof(RoomDescriptionAdds));
        }

        public List<string> RoomDescriptionRemoves
        {
            get => this._roomDescriptionRemoves;
            set => this.SetProperty<List<string>>(ref this._roomDescriptionRemoves, value, nameof(RoomDescriptionRemoves));
        }

        public List<GridListItemViewModel<FeatureDesc>> RoomFeatures
        {
            get => this._roomFeatures;
            set => this.SetProperty<List<GridListItemViewModel<FeatureDesc>>>(ref this._roomFeatures, value, nameof(RoomFeatures));
        }

        public List<Classroom> DBClassrooms
        {
            get => this._dbClassrooms;
            set => this.SetProperty<List<Classroom>>(ref this._dbClassrooms, value, nameof(DBClassrooms));
        }

        public List<Building> DBBuildings
        {
            get => this._dbBuildings;
            set => this.SetProperty<List<Building>>(ref this._dbBuildings, value, nameof(DBBuildings));
        }

        private async void HandleSqlException(string message)
        {
            MessageDialogResult result = await this._dialogCoordinator.ShowMessageAsync((object)this, "Error has occured", message, settings: new MetroDialogSettings()
            {
                AffirmativeButtonText = "Dismiss"
            });
        }

        public void SetControlType(string item) => this._selectedAddControlType = item;

        public void SetRoomType(string item) => this._selectedAddRoomType = item;

        public void FeatureChecked(string updatedFeature)
        {
            if (this._roomFeatures.Where<GridListItemViewModel<FeatureDesc>>((Func<GridListItemViewModel<FeatureDesc>, bool>)(x => x.Data.Description == updatedFeature)).First<GridListItemViewModel<FeatureDesc>>().IsEnabled)
                this.RoomDescriptionRemoves.Remove(updatedFeature);
            else
                this.RoomDescriptionAdds.Add(updatedFeature);
        }

        public async void AddBuilding()
        {
            try
            {
                MessageDialogResult result = await this._dialogCoordinator.ShowMessageAsync((object)this, "Commit Changes to Database", "Are you sure? This action cannot be undone", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings()
                {
                    AffirmativeButtonText = "Confirm",
                    NegativeButtonText = "Decline"
                });
                if (result != MessageDialogResult.Affirmative)
                    return;
                this._model.AddBuilding(this.AddBuildingName, this.AddBuildingCode, this.AddBuildingIsEnabled);
                this.DBBuildings.Clear();
                this.DBBuildings = this._model.GetBuildingNames().OrderBy<Building, string>((Func<Building, string>)(x => x.BuildingName)).ToList<Building>();
                int index = this.DBBuildings.IndexOf(this.DBBuildings.Where<Building>((Func<Building, bool>)(x => x.BuildingName == this.AddBuildingName)).First<Building>());
                this.UpdateBuildingList(index);
                this.ClearAddBuildingText();
            }
            catch
            {
            }
        }

        public void ClearAddBuildingText()
        {
            this.AddBuildingName = string.Empty;
            this.AddBuildingCode = string.Empty;
            this.AddBuildingIsEnabled = false;
        }

        public void ClearAddClassroomText()
        {
            this.AddRoomName = string.Empty;
            this.AddRoomIsEnabled = false;
        }

        public async void AddClassroom()
        {
            int? buildingId = new int?();
            try
            {
                buildingId = this._model.GetBuildingCodeForBuildingName(this._selectedBuilding);
                if (!buildingId.HasValue)
                    return;
                MessageDialogResult result = await this._dialogCoordinator.ShowMessageAsync((object)this, "Commit Changes to Database", "Are you sure? This action cannot be undone", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings()
                {
                    AffirmativeButtonText = "Confirm",
                    NegativeButtonText = "Decline"
                });
                if (result == MessageDialogResult.Affirmative)
                {
                    this._model.AddClassroom(buildingId.Value, this.AddRoomName, this._selectedAddRoomType, this._selectedAddControlType, 50, this.AddRoomIsEnabled);
                    this.DBClassrooms.Clear();
                    this.DBClassrooms = this._model.GetClassroomNames(this._selectedBuilding).OrderBy<Classroom, string>((Func<Classroom, string>)(x => x.RoomNumber)).ToList<Classroom>();
                    if (this.DBClassrooms.Count > 0)
                    {
                        int index = this.DBClassrooms.IndexOf(this.DBClassrooms.Where<Classroom>((Func<Classroom, bool>)(x => x.RoomNumber == this.AddRoomName)).First<Classroom>());
                        this.UpdateRoomList(index);
                    }
                    this.ClearAddClassroomText();
                }
            }
            catch
            {
            }
        }

        public void RoomTypeChecked(string updatedType)
        {
            if (this.GridTypeDescs.Where<GridListItemViewModel<TypeDesc>>((Func<GridListItemViewModel<TypeDesc>, bool>)(x => x.Data.Description == updatedType)).First<GridListItemViewModel<TypeDesc>>().IsEnabled)
                this.RoomTypeRemoves.Remove(updatedType);
            else
                this.RoomTypeAdds.Add(updatedType);
        }

        public void RoomTypeUnchecked(string updatedType)
        {
            if (this.GridTypeDescs.Where<GridListItemViewModel<TypeDesc>>((Func<GridListItemViewModel<TypeDesc>, bool>)(x => x.Data.Description == updatedType)).First<GridListItemViewModel<TypeDesc>>().IsEnabled)
                this.RoomTypeRemoves.Add(updatedType);
            else
                this.RoomTypeAdds.Remove(updatedType);
        }

        public void ControlTypeChecked(string updatedType)
        {
            if (this.GridControlTypes.Where<GridListItemViewModel<ControlType>>((Func<GridListItemViewModel<ControlType>, bool>)(x => x.Data.Description == updatedType)).First<GridListItemViewModel<ControlType>>().IsEnabled)
                this.ControlTypeRemoves.Remove(updatedType);
            else
                this.ControlTypeAdds.Add(updatedType);
        }

        public void RoomDisabled(string RoomNumber)
        {
            if (this.DBClassrooms.Where<Classroom>((Func<Classroom, bool>)(x => x.RoomNumber == RoomNumber)).First<Classroom>().Enabled.Value)
                this.RoomDisables.Add(RoomNumber);
            else
                this.RoomEnables.Remove(RoomNumber);
        }

        public void RoomEnabled(string RoomNumber)
        {
            if (this.DBClassrooms.Where<Classroom>((Func<Classroom, bool>)(x => x.RoomNumber == RoomNumber)).First<Classroom>().Enabled.Value)
                this.RoomDisables.Remove(RoomNumber);
            else
                this.RoomEnables.Add(RoomNumber);
        }

        public void BuildingDisabled(string buildingName)
        {
            if (this.DBBuildings.Where<Building>((Func<Building, bool>)(x => x.BuildingName == buildingName)).First<Building>().Enabled.Value)
                this.BuildingDisables.Add(buildingName);
            else
                this.BuildingEnables.Remove(buildingName);
        }

        public void BuildingEnabled(string buildingName)
        {
            if (this.DBBuildings.Where<Building>((Func<Building, bool>)(x => x.BuildingName == buildingName)).First<Building>().Enabled.Value)
                this.BuildingDisables.Remove(buildingName);
            else
                this.BuildingEnables.Add(buildingName);
        }

        public void ControlTypeUnchecked(string updatedType)
        {
            if (this.GridControlTypes.Where<GridListItemViewModel<ControlType>>((Func<GridListItemViewModel<ControlType>, bool>)(x => x.Data.Description == updatedType)).First<GridListItemViewModel<ControlType>>().IsEnabled)
                this.ControlTypeRemoves.Add(updatedType);
            else
                this.ControlTypeAdds.Remove(updatedType);
        }

        public void FeatureUnchecked(string updatedFeature)
        {
            if (this._roomFeatures.Where<GridListItemViewModel<FeatureDesc>>((Func<GridListItemViewModel<FeatureDesc>, bool>)(x => x.Data.Description == updatedFeature)).First<GridListItemViewModel<FeatureDesc>>().IsEnabled)
                this.RoomDescriptionRemoves.Add(updatedFeature);
            else
                this.RoomDescriptionAdds.Remove(updatedFeature);
        }

        public void ClearFeatureChanges()
        {
            this.RoomDescriptionRemoves.Clear();
            this.RoomDescriptionAdds.Clear();
        }

        public void ClearRoomEnables()
        {
            this.RoomEnables.Clear();
            this.RoomDisables.Clear();
        }

        public void ClearBuildingEnables()
        {
            this.BuildingEnables.Clear();
            this.BuildingDisables.Clear();
        }

        public void ClearControlTypes()
        {
            this.ControlTypeAdds.Clear();
            this.ControlTypeRemoves.Clear();
        }

        public void ClearRoomTypes()
        {
            this.RoomTypeAdds.Clear();
            this.RoomTypeRemoves.Clear();
        }

        public async void CommitChanges()
        {
            int? controlTypeIdToUpdate = new int?();
            int? typeIdToUpdate = new int?();
            try
            {
                int? buildingId = this._model.GetBuildingCodeForBuildingName(this._selectedBuilding);
                List<int> featureIdsToAdd = this._model.GetFeatureDescs(this.RoomDescriptionAdds);
                List<int> featureIdsToRemove = this._model.GetFeatureDescs(this.RoomDescriptionRemoves);
                if (this.ControlTypeAdds.Count > 0)
                    controlTypeIdToUpdate = this._model.GetControlTypeToUpdate(this.GridControlTypes, this.ControlTypeAdds.First<string>());
                if (this.RoomTypeAdds.Count > 0)
                    typeIdToUpdate = this._model.GetTypeIdToUpdate(this.GridTypeDescs, this.RoomTypeAdds.First<string>());
                if (buildingId.HasValue)
                {
                    MessageDialogResult result = await this._dialogCoordinator.ShowMessageAsync((object)this, "Commit Changes to Database", "Are you sure? This action cannot be undone", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings()
                    {
                        AffirmativeButtonText = "Confirm",
                        NegativeButtonText = "Decline"
                    });
                    if (result == MessageDialogResult.Affirmative)
                    {
                        if ((uint)featureIdsToAdd.Count > 0U)
                            this._model.AddDescriptions(featureIdsToAdd, this._roomNumberSelected, buildingId.Value);
                        if ((uint)featureIdsToRemove.Count > 0U)
                            this._model.RemoveDescriptions(featureIdsToRemove, this._roomNumberSelected, buildingId.Value);
                        if (controlTypeIdToUpdate.HasValue)
                            this._model.UpdateControlType(controlTypeIdToUpdate.Value, this._selectedBuilding, this._roomNumberSelected);
                        if (typeIdToUpdate.HasValue)
                            this._model.UpdateType(typeIdToUpdate.Value, this._selectedBuilding, this._roomNumberSelected);
                        if (this.RoomEnables.Count > 0)
                            this._model.SetRoomEnables(this.RoomEnables, this._selectedBuilding);
                        if (this.RoomDisables.Count > 0)
                            this._model.SetRoomDisables(this.RoomDisables, this._selectedBuilding);
                        if (this.BuildingEnables.Count > 0)
                            this._model.SetBuildingEnables(this.BuildingEnables);
                        if (this.BuildingDisables.Count > 0)
                            this._model.SetBuildingDisables(this.BuildingDisables);
                        this.ClearRoomEnables();
                        this.ClearBuildingEnables();
                        this.ClearFeatureChanges();
                        this.ControlTypeAdds.Clear();
                        this.ControlTypeRemoves.Clear();
                        this.RoomTypeAdds.Clear();
                        this.RoomTypeRemoves.Clear();
                        this.UpdateClassroomDataGrid();
                        this.UpdateBuildingDataGrid();
                        this.UpdateDashboardLists();
                        if (this.DBClassrooms.Count > 0)
                        {
                            int index = this.DBClassrooms.IndexOf(this.DBClassrooms.Where<Classroom>((Func<Classroom, bool>)(x => x.RoomNumber == this._roomNumberSelected)).First<Classroom>());
                            this.UpdateRoomList(index);
                        }
                    }
                }
                buildingId = new int?();
                featureIdsToAdd = (List<int>)null;
                featureIdsToRemove = (List<int>)null;
            }
            catch
            {
            }
        }

        private void UpdateBuildingDataGrid()
        {
            try
            {
                this.DBBuildings.Clear();
                this.DBBuildings = this._model.GetBuildingNames();
            }
            catch
            {
            }
        }

        private void UpdateClassroomDataGrid()
        {
            try
            {
                this.DBClassrooms.Clear();
                this.DBClassrooms = this._model.GetClassroomNames(this._selectedBuilding).OrderBy<Classroom, string>((Func<Classroom, string>)(x => x.RoomNumber)).ToList<Classroom>();
            }
            catch
            {
            }
        }

        public void BuildingSelected(string item)
        {
            try
            {
                this._selectedBuilding = item;
                this.DBClassrooms = this._model.GetClassroomNames(item);
            }
            catch
            {
                return;
            }
            this.UpdateRoomList(0);
            this.ClearRoomEnables();
        }

        public void RoomSelected(string item)
        {
            this._roomNumberSelected = item;
            this.ClearFeatureChanges();
            this.ClearControlTypes();
            this.ClearRoomTypes();
            try
            {
                this.RoomFeatures = this._model.GetRoomFeatures(item);
                this.GridControlTypes = this._model.GetRoomControlTypes(this._roomNumberSelected, this._selectedBuilding);
                this.GridTypeDescs = this._model.GetRoomDescTypes(this._roomNumberSelected, this._selectedBuilding);
            }
            catch
            {
            }
        }

        public void ResetChanges()
        {
            this.ClearRoomEnables();
            this.ClearBuildingEnables();
            this.ClearFeatureChanges();
            this.ClearControlTypes();
            this.ClearRoomTypes();
            this.UpdateClassroomDataGrid();
            this.UpdateBuildingDataGrid();
            this.UpdateDashboardLists();
            try
            {
                this.RoomFeatures = this._model.GetRoomFeatures(this._roomNumberSelected);
                this.GridControlTypes = this._model.GetRoomControlTypes(this._roomNumberSelected, this._selectedBuilding);
                this.GridTypeDescs = this._model.GetRoomDescTypes(this._roomNumberSelected, this._selectedBuilding);
            }
            catch
            {
            }
        }

        public delegate void BuildingListUpdateDelegate(int itemIndex);

        public delegate void RoomListUpdateDelegate(int itemIndex);

        public delegate void UpdateDashboardStatus();
    }
}
