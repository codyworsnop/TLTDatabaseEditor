using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLTDatabaseEditor
{
    public class ExportDataViewModel : ItemViewModel
    {
        ExportDataModel _model = new ExportDataModel();

        public ExportDataViewModel(string tabName) : base(tabName)
        {
        }

        public ExportDataViewModel() : base("Empty")
        {
        }

        public void ExportToExcel()
        {
            _model.ExportExcelData();
        }

    }
}
