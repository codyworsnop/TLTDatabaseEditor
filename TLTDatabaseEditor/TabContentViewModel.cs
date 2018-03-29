using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLTDatabaseEditor
{
    public class TabContentViewModel : ViewModelBase
    {
        private ObservableCollection<ItemViewModel> _items = new ObservableCollection<ItemViewModel>();

        public ObservableCollection<ItemViewModel> Items { get => _items; set => _items = value; }

        public TabContentViewModel()
        {
            this.Items.Add(new DashboardViewModel("DASHBOARD"));
            this.Items.Add(new AddRoomViewModel("ADD ROOM"));
            this.Items.Add(new AddBuildingViewModel("ADD BUILDING"));
        }
    }
}
