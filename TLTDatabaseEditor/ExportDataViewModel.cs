using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace TLTDatabaseEditor
{
    public class ExportDataViewModel : ItemViewModel
    {
        ExportDataModel _model = new ExportDataModel();
        private IDialogCoordinator _dialogCoordinator;
        private List<Building> _dbBuildings;

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
        public ExportDataViewModel(string tabName) : base(tabName)
        {
        }

        public ExportDataViewModel(IDialogCoordinator instance) : base("Empty")
        {
            _dialogCoordinator = instance;
        }


        public async void ExportToExcel()
        {
            var controller = await _dialogCoordinator.ShowProgressAsync(this, "Generating report", null);

            await Task.Run(() =>
            {
                _model.ExportExcelData(ref controller);
            });

            await controller.CloseAsync();
        }

    }
}
