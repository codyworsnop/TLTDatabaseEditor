using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLTDatabaseEditor
{
    public class DashboardViewModel : ItemViewModel
    {
        DashboardModel _model = new DashboardModel();

        private List<string> _dbBuildings;
        private List<string> _dbClassrooms;
        private List<string> _roomFeatures;

        public DashboardViewModel(string tabName) : base(tabName)
        {
        }

        public DashboardViewModel() : base("Empty")
        {
            DBBuildings = _model.GetBuildingNames();
        }

        public List<string> RoomFeatures
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

        public List<string> DBClassrooms
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


        public List<string> DBBuildings
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
            DBClassrooms = _model.GetClassroomNames(item);
        }

        public void RoomSelected(string item)
        {
            RoomFeatures = _model.GetRoomFeatures(item);
        }
    }
}
