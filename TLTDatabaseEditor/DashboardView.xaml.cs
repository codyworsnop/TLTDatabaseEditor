using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TLTDatabaseEditor
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        DashboardModel _model = new DashboardModel();
        public ObservableCollection<object> DBRawData = new ObservableCollection<object>();

        public DashboardView()
        {
            InitializeComponent();

            if (_model.dc.DatabaseExists())
            {
                var buildingNames = _model.dc.Buildings.Select(x => x.BuildingName);

                foreach (var building in buildingNames)
                {
                    DBRawData.Add(new ListViewItemViewModel(building));
                }

                thisgridsux.ItemsSource = DBRawData;
            }
        }
    }
}
