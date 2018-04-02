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

        public List<string> GetBuildingNames()
        {
            if (dc.DatabaseExists())
            {
                return dc.Buildings.Where(x => x.Enabled == true).Select(x => x.BuildingName).ToList();
            }

            return null;
        }

        public List<string> GetClassroomNames(string buildingName)
        {
            if (dc.DatabaseExists())
            {
                _buildingId = dc.Buildings.Where(x => x.BuildingName == buildingName && x.Enabled == true).Select(x => x.BuildingID);
                return dc.Classrooms.Where(x => x.BuildingID == _buildingId.First() && x.Enabled == true).Select(x => x.RoomNumber).ToList();
            }

            return null;
        }

        public List<string> GetRoomFeatures(string classroomNumber)
        {
            if (dc.DatabaseExists())
            {
                var featureList = new List<string>();

                var featureId = dc.ClassroomFeatureDescs.Where(x => x.RoomNumber == classroomNumber && x.BuildingID == _buildingId.First()).Select(x => x.FeatureDescID);
           
                foreach (var id in featureId)
                {
                    var classroomFeature = dc.FeatureDescs.Where(x => x.FeatureDescID == id).Select(x => x.Description).ToList();
                    if (classroomFeature.Count > 0)
                    {
                        featureList.Add(classroomFeature.First());
                    }
                }

                return featureList;
            }

            return null;
        }
    }
}



