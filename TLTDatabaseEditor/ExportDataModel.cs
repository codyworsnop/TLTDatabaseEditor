using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace TLTDatabaseEditor
{
    public class ExportDataModel
    {

        public DatabaseConnectionManagerDataContext dc = new DatabaseConnectionManagerDataContext(Properties.Settings.Default.TLT_InventoryConnectionString);

        public ExportDataModel()
        {
        }

        public void ExportExcelData()
        {
            var excelApp = new Excel.Application();
            excelApp.Visible = true;
            excelApp.Workbooks.Add();

            var buildings = GetBuildings();
            
            foreach (var building in buildings)
            {
                Excel._Worksheet activeWorkSheet = (Excel.Worksheet)excelApp.ActiveSheet;
                activeWorkSheet.Name = building;

                //get view for building
                var view = GetViewDescriptionDataForBuilding(building);

                activeWorkSheet.Cells[1, "A"] = "Room Number";
                activeWorkSheet.Cells[1, "B"] = "Features";

                var rowIndex = 2;

                var currentRoomNumber = string.Empty;

                foreach (var room in view)
                {
                    if (room.RoomNumber != currentRoomNumber)
                    {
                        currentRoomNumber = room.RoomNumber;
                        activeWorkSheet.Cells[rowIndex, "A"] = room.RoomNumber;
                    }
                    
                    activeWorkSheet.Cells[rowIndex, "B"] = room.Description;

                    rowIndex++;
                }

                activeWorkSheet.Columns[1].AutoFit();
                activeWorkSheet.Columns[2].AutoFit();
                activeWorkSheet.Columns[2].AutoFit();
                activeWorkSheet.Columns[2].AutoFit();

                if (building != buildings[buildings.Count -1])
                {
                    excelApp.Worksheets.Add();
                }
            }
        }

        private List<string> GetBuildings()
        {
            if (dc.DatabaseExists())
            {
                return dc.Buildings.OrderByDescending(x => x.BuildingName).Select(x => x.BuildingCode).ToList();
            }

            return null;
        }

        private List<vBuildingRoomFeature> GetViewDescriptionDataForBuilding(string buildingCode)
        {
            if (dc.DatabaseExists())
            {
                return dc.vBuildingRoomFeatures.Where(x => x.BuildingCode == buildingCode).OrderBy(x => x.RoomNumber).ToList();
            }

            return null;
        }
    }
}
