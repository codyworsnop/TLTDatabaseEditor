
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TLTDatabaseEditor
{
    public class DashboardViewModel : ItemViewModel
    {
        DashboardModel _model = new DashboardModel();

        private List<Building> _dbBuildings;
        private List<Classroom> _dbClassrooms;
        private List<RoomFeatureDataItemViewModel> _roomFeatures;
        private List<string> _roomDescriptionAdds;
        private List<string> _roomDescriptionRemoves;

        private List<string> _controlTypeAdds;
        private List<string> _controlTypeRemoves;
        private List<string> _roomTypeAdds;
        private List<string> _roomTypeRemoves;

        private IDialogCoordinator _dialogCoordinator;

        private string _selectedBuilding;
        private string _roomNumberSelected;

        private string _addBuildingName;
        private string _addBuildingCode;
        private string _addRoomName;
        private List<string> _roomTypes;
        private List<string> _roomControlTypes;
        private List<ControlTypeViewModel> _gridControlTypes;
        private List<RoomTypeViewModel> _gridTypeDescs;
        private string _selectedAddRoomType;
        private string _selectedAddControlType;

        private bool _addBuildingIsEnabled;
        private bool _addRoomIsEnabled;

        public DashboardViewModel(string tabName) : base(tabName)
        {
        }

        public DashboardViewModel(IDialogCoordinator instance) : base("Empty")
        {
            _dialogCoordinator = instance;

            DBBuildings = _model.GetBuildingNames();

            RoomDescriptionAdds = new List<string>();
            RoomDescriptionRemoves = new List<string>();
            ControlTypeAdds = new List<string>();
            ControlTypeRemoves = new List<string>();
            RoomTypeAdds = new List<string>();
            RoomTypeRemoves = new List<string>();

            RoomControlTypes = _model.GetRoomControlTypes();
            RoomTypes = _model.GetRoomTypes();
        }

        public void SetControlType(string item)
        {
            _selectedAddControlType = item;
        }

        public void SetRoomType(string item)
        {
            _selectedAddRoomType = item;
        }

        //I'd really like to find a better way of doing this checked unchecked thing it erks me
        public void FeatureChecked(string updatedFeature)
        {
            var feature = _roomFeatures.Where(x => x.Feature.Description == updatedFeature);
            if (feature.First().FeatureIsEnabled)
            {
                RoomDescriptionRemoves.Remove(updatedFeature);
            }
            else
            {
                RoomDescriptionAdds.Add(updatedFeature);
            }
        }

        public async void AddBuilding()
        {
            var result = await _dialogCoordinator.ShowMessageAsync(this, "Commit Changes to Database", "Are you sure? This action cannot be undone", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings() { AffirmativeButtonText = "Confirm", NegativeButtonText = "Decline" });

            if (result == MessageDialogResult.Affirmative)
            {
                _model.AddBuilding(AddBuildingName, AddBuildingCode, AddBuildingIsEnabled);
                ClearAddBuildingText();
            }
        }

        public void ClearAddBuildingText()
        {
            AddBuildingName = string.Empty;
            AddBuildingCode = string.Empty;
            AddBuildingIsEnabled = false;
        }

        public void ClearAddClassroomText()
        {
            AddRoomName = string.Empty;
            AddRoomIsEnabled = false;
        }

        public async void AddClassroom()
        {

            var buildingId = _model.GetBuildingCodeForBuildingName(_selectedBuilding);

            if (buildingId != null)
            {
                var result = await _dialogCoordinator.ShowMessageAsync(this, "Commit Changes to Database", "Are you sure? This action cannot be undone", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings() { AffirmativeButtonText = "Confirm", NegativeButtonText = "Decline" });

                if (result == MessageDialogResult.Affirmative)
                {
                    _model.AddClassroom(buildingId.Value, AddRoomName, _selectedAddRoomType, _selectedAddControlType, 50, AddRoomIsEnabled);
                    ClearAddBuildingText();
                }
            }
        }

        public void RoomTypeChecked(string updatedType)
        {
            var type = GridTypeDescs.Where(x => x.Type.Description == updatedType);
            if (type.First().RoomHasType)
            {
                RoomTypeRemoves.Remove(updatedType);
            }
            else
            {
                RoomTypeAdds.Add(updatedType);
            }
        }

        public void RoomTypeUnchecked(string updatedType)
        {
            var type = GridTypeDescs.Where(x => x.Type.Description == updatedType);
            if (type.First().RoomHasType)
            {
                RoomTypeRemoves.Add(updatedType);
            }
            else
            {
                RoomTypeAdds.Remove(updatedType);
            }
        }

        public void ControlTypeChecked(string updatedType)
        {
            var type = GridControlTypes.Where(x => x.Type.Description == updatedType);
            if (type.First().RoomHasType)
            {
                ControlTypeRemoves.Remove(updatedType);
            }
            else
            {
                ControlTypeAdds.Add(updatedType);
            }
        }

        public void ControlTypeUnchecked(string updatedType)
        {
            var type = GridControlTypes.Where(x => x.Type.Description == updatedType);
            if (type.First().RoomHasType)
            {
                ControlTypeRemoves.Add(updatedType);
            }
            else
            {
                ControlTypeAdds.Remove(updatedType);
            }
        }

        public void FeatureUnchecked(string updatedFeature)
        {
            var feature = _roomFeatures.Where(x => x.Feature.Description == updatedFeature);
            if (feature.First().FeatureIsEnabled)
            {
                RoomDescriptionRemoves.Add(updatedFeature);
            }
            else
            {
                RoomDescriptionAdds.Remove(updatedFeature);
            }
        }

        public void ClearFeatureChanges()
        {
            RoomDescriptionRemoves.Clear();
            RoomDescriptionAdds.Clear();
        }

        public async void CommitChanges()
        {
            var buildingId = _model.GetBuildingCodeForBuildingName(_selectedBuilding);
            var featureIdsToAdd = _model.GetFeatureDescs(RoomDescriptionAdds);
            var featureIdsToRemove = _model.GetFeatureDescs(RoomDescriptionRemoves);

            if (buildingId != null)
            {
                var result = await _dialogCoordinator.ShowMessageAsync(this, "Commit Changes to Database", "Are you sure? This action cannot be undone", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings() { AffirmativeButtonText = "Confirm", NegativeButtonText = "Decline" });

                if (result == MessageDialogResult.Affirmative)
                {
                    _model.AddDescriptions(featureIdsToAdd, _roomNumberSelected, buildingId.Value);
                    _model.RemoveDescriptions(featureIdsToRemove, _roomNumberSelected, buildingId.Value);

                    RoomDescriptionAdds.Clear();
                    RoomDescriptionRemoves.Clear();
                }
            }
        }

        public List<ControlTypeViewModel> GridControlTypes
        {
            get
            {
                return _gridControlTypes;
            }

            set
            {
                SetProperty(ref _gridControlTypes, value);
            }
        }

        public List<RoomTypeViewModel> GridTypeDescs
        {
            get
            {
                return _gridTypeDescs;
            }

            set
            {
                SetProperty(ref _gridTypeDescs, value);
            }
        }

        public List<string> RoomControlTypes
        {
            get
            {
                return _roomControlTypes;
            }

            set
            {
                SetProperty(ref _roomControlTypes, value);
            }
        }

        public List<string> RoomTypes
        {
            get
            {
                return _roomTypes;
            }

            set
            {
                SetProperty(ref _roomTypes, value);
            }
        }

        public bool AddBuildingIsEnabled
        {
            get
            {
                return _addBuildingIsEnabled;
            }

            set
            {
                SetProperty(ref _addBuildingIsEnabled, value);
            }
        }

        public bool AddRoomIsEnabled
        {
            get
            {
                return _addRoomIsEnabled;
            }

            set
            {
                SetProperty(ref _addRoomIsEnabled, value);
            }
        }

        public string AddRoomName
        {
            get
            {
                return _addRoomName;
            }

            set
            {
                SetProperty(ref _addRoomName, value);
            }
        }

        public string AddBuildingName
        {
            get
            {
                return _addBuildingName;
            }

            set
            {
                SetProperty(ref _addBuildingName, value);
            }
        }

        public string AddBuildingCode
        {
            get
            {
                return _addBuildingCode;
            }

            set
            {
                SetProperty(ref _addBuildingCode, value);
            }
        }

        public List<string> RoomTypeAdds
        {
            get
            {
                return _roomTypeAdds;
            }
            
            set
            {
                SetProperty(ref _roomTypeAdds, value);
            }
        }

        public List<string> RoomTypeRemoves
        {
            get
            {
                return _roomTypeRemoves;
            }

            set
            {
                SetProperty(ref _roomTypeRemoves, value);
            }
        }

        public List<string> ControlTypeAdds
        {
            get
            {
                return _controlTypeAdds;
            }

            set
            {
                SetProperty(ref _controlTypeAdds, value);
            }
        }

        public List<string> ControlTypeRemoves
        {
            get
            {
                return _controlTypeRemoves;
            }

            set
            {
                SetProperty(ref _controlTypeRemoves, value);
            }
        }

        public List<string> RoomDescriptionAdds
        {
            get
            {
                return _roomDescriptionAdds;
            }

            set
            {
                SetProperty(ref _roomDescriptionAdds, value);
            }
        }

        public List<string> RoomDescriptionRemoves
        {
            get
            {
                return _roomDescriptionRemoves;
            }

            set
            {
                SetProperty(ref _roomDescriptionRemoves, value);
            }
        }

        public List<RoomFeatureDataItemViewModel> RoomFeatures
        {
            get
            {
                return _roomFeatures;
            }

            set
            {
                SetProperty(ref _roomFeatures, value);
            }
        }

        public List<Classroom> DBClassrooms
        {
            get
            {
                return _dbClassrooms;
            }

            set
            {
                SetProperty(ref _dbClassrooms, value);
            }
        }


        public List<Building> DBBuildings
        {
            get
            {
                return _dbBuildings;
            }

            set
            {
                SetProperty(ref _dbBuildings, value);
            }
        }

        public void BuildingSelected(string item)
        {
            _selectedBuilding = item;

            DBClassrooms = _model.GetClassroomNames(item);
        }

        public void RoomSelected(string item)
        {
            _roomNumberSelected = item;
            RoomFeatures = _model.GetRoomFeatures(item);
            GridControlTypes = _model.GetRoomControlTypes(_roomNumberSelected, _selectedBuilding);
            GridTypeDescs = _model.GetRoomDescTypes(_roomNumberSelected, _selectedBuilding);
        }
    }
}
