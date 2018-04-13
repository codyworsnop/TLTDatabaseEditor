using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLTDatabaseEditor
{
    public class DashboardModel
    {
        public DatabaseConnectionManagerDataContext dc = new DatabaseConnectionManagerDataContext(Properties.Settings.Default.TLT_InventoryConnectionString);

        private IQueryable<int> _buildingId;

        public DashboardModel()
        {

        }

        public void AddBuilding(string BuildingName, string BuildingCode, bool IsEnabled)
        {
            if (dc.DatabaseExists())
            {
                var result = dc.ExecuteQuery<BuildingQuery>(@"INSERT INTO Building VALUES ({0}, {1}, {2})", BuildingName, BuildingCode, IsEnabled);
            }
        }

        public void AddClassroom(int BuildingID, string RoomNumber, bool IsEnabled)
        {
            if (dc.DatabaseExists())
            {

                var result = dc.ExecuteQuery<ClassroomQuery>(@"INSERT INTO Classroom VALUES ({0}, {1}, {2})", RoomNumber, BuildingID, IsEnabled);
            }
        }

        public void RemoveDescriptions(List<int> FeatureDescIDs, string RoomNumber, int BuildingID)
        {
            if (dc.DatabaseExists())
            {
                foreach (var id in FeatureDescIDs)
                {
                    var result = dc.ExecuteQuery<ClassroomFeatureDescQuery>(@"DELETE FROM ClassroomFeatureDesc WHERE FeatureDescID = {0} AND RoomNumber = {1} AND BuildingID = {2}", id, RoomNumber, BuildingID);
                }
            }


        }

        public void AddDescriptions(List<int> FeatureDescIDs, string RoomNumber, int BuildingID)
        {
            if (dc.DatabaseExists())
            {
                foreach (var id in FeatureDescIDs)
                {
                    var result = dc.ExecuteQuery<ClassroomFeatureDescQuery>(@"INSERT INTO ClassroomFeatureDesc VALUES ({0}, {1}, {2})", id, RoomNumber, BuildingID);
                }
            }


        }

        public List<int> GetFeatureDescs(List<string> features)
        {
            if (dc.DatabaseExists())
            {
                var descriptions = new List<int>();

                foreach (var feature in features)
                {
                    descriptions.Add(dc.FeatureDescs.Where(x => x.Description == feature).Select(x => x.FeatureDescID).First());
                }

                return descriptions;
            }

            return null;
        }

        public List<Building> GetBuildingNames()
        {
            if (dc.DatabaseExists())
            {
                return dc.Buildings.Where(x => x.Enabled == true).ToList();
            }

            return null;
        }

        public List<Classroom> GetClassroomNames(string buildingName)
        {
            if (dc.DatabaseExists())
            {
                _buildingId = dc.Buildings.Where(x => x.BuildingName == buildingName && x.Enabled == true).Select(x => x.BuildingID);
                return dc.Classrooms.Where(x => x.BuildingID == _buildingId.First() && x.Enabled == true).ToList();
            }

            return null;
        }

        public int? GetBuildingCodeForBuildingName(string buildingName)
        {
            if (dc.DatabaseExists())
            {
                var buildingList = _buildingId = dc.Buildings.Where(x => x.BuildingName == buildingName && x.Enabled == true).Select(x => x.BuildingID);
                return buildingList.First();
            }

            return null;
        }

        private List<FeatureDesc> GetAllRoomFeatures()
        {
            if (dc.DatabaseExists())
            {
                return dc.FeatureDescs.ToList();
            }

            return null;
        }

        public List<RoomFeatureIDataItemViewModel> GetRoomFeatures(string classroomNumber)
        {
            if (dc.DatabaseExists())
            {
                var featureList = new List<RoomFeatureIDataItemViewModel>();

                var featureId = dc.ClassroomFeatureDescs.Where(x => x.RoomNumber == classroomNumber && x.BuildingID == _buildingId.First()).Select(x => x.FeatureDescID);

                foreach (var id in featureId)
                {
                    var classroomFeature = dc.FeatureDescs.Where(x => x.FeatureDescID == id);
                    if (classroomFeature.Count() > 0)
                    {
                        featureList.Add(new RoomFeatureIDataItemViewModel() { Feature = classroomFeature.First(), FeatureIsEnabled = true });
                    }
                }

                var allRoomFeatures = GetAllRoomFeatures();

                foreach (var feature in allRoomFeatures)
                {
                    if (!featureList.Select(x => x.Feature.Description).Contains(feature.Description))
                    {
                        featureList.Add(new RoomFeatureIDataItemViewModel() { Feature = feature, FeatureIsEnabled = false });
                    }
                }

                return featureList;
            }

            return null;
        }
    }
}



