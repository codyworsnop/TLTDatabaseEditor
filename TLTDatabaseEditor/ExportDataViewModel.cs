// Decompiled with JetBrains decompiler
// Type: TLTDatabaseEditor.ExportDataViewModel
// Assembly: TLTDatabaseEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D3AA8152-A60D-4A42-87BE-242C5FFCE9A0
// Assembly location: C:\Program Files (x86)\GizmoTron v1.5\TLTDatabaseEditor.exe

using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TLTDatabaseEditor
{
    public class ExportDataViewModel : ItemViewModel
    {
        private ExportDataModel _model = new ExportDataModel();
        private IDialogCoordinator _dialogCoordinator;
        private List<GridListItemViewModel<Building>> _dbBuildings;

        public List<GridListItemViewModel<Building>> DBBuildings
        {
            get => this._dbBuildings;
            set => this.SetProperty<List<GridListItemViewModel<Building>>>(ref this._dbBuildings, value, nameof(DBBuildings));
        }

        public ExportDataViewModel(string tabName)
          : base(tabName)
        {
        }

        public ExportDataViewModel(IDialogCoordinator instance)
          : base("Empty")
        {
            this._dialogCoordinator = instance;
            this.DBBuildings = this._model.GetBuildingNames();
        }

        public async void ExportToExcel()
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = tokenSource.Token;
            ProgressDialogController controller = await this._dialogCoordinator.ShowProgressAsync((object)this, "Generating report", "Please wait . . . ", true);
            controller.Canceled += (EventHandler)(async (source, e) =>
            {
                tokenSource.Cancel();
                await controller.CloseAsync();
            });
            await Task.Factory.StartNew((Action)(() => this._model.ExportExcelData(this.DBBuildings.OrderByDescending<GridListItemViewModel<Building>, string>((Func<GridListItemViewModel<Building>, string>)(x => x.Data.BuildingCode)).ToList<GridListItemViewModel<Building>>(), ref controller, ref cancellationToken)), cancellationToken);
            if (controller.IsCanceled)
                return;
            await controller.CloseAsync();
        }
    }
}
