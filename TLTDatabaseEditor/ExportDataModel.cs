using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public void ExportExcelData(
      List<GridListItemViewModel<Building>> dbBuildings,
      ref ProgressDialogController controller,
      ref CancellationToken ct)
        {
            double buildingIndex = 0;

            var excelApp = new Excel.Application();
            excelApp.Workbooks.Add();

            var buildings = GetBuildings();

            foreach (var listItemViewModel in dbBuildings.Where(x => x.IsEnabled))
            {
                if (!ct.IsCancellationRequested)
                {
                    double progress = buildingIndex / buildings.Count;
                    controller.SetProgress(progress);
                    buildingIndex++;

                    Excel._Worksheet activeWorkSheet = (Excel.Worksheet)excelApp.ActiveSheet;
                    activeWorkSheet.Name = listItemViewModel.Data.BuildingCode;


                    //get view for building
                    var view = GetViewDescriptionDataForBuilding(listItemViewModel.Data.BuildingCode);

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

                    if (listItemViewModel.Data.BuildingCode != buildings[buildings.Count - 1])
                    {
                        excelApp.Worksheets.Add();
                    }
                }
                else
                    break;
            }
            if (ct.IsCancellationRequested)
                return;

            excelApp.Visible = true;
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

        public List<GridListItemViewModel<Building>> GetBuildingNames()
        {
            if (!this.dc.DatabaseExists())
                return (List<GridListItemViewModel<Building>>)null;
            List<GridListItemViewModel<Building>> buildingNames = new List<GridListItemViewModel<Building>>();

            foreach (Building building in dc.Buildings.Where(x => x.Enabled == true).ToList())
                buildingNames.Add(new GridListItemViewModel<Building>()
                {
                    Data = building,
                    IsEnabled = true
                });
            return buildingNames;
        }
    }
}
