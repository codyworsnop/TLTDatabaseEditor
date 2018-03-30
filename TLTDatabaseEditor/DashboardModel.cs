using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLTDatabaseEditor
{   
    public class DashboardModel
    {
        public DatabaseConnectionManagerDataContext dc = new DatabaseConnectionManagerDataContext(Properties.Settings.Default.TLT_InventoryConnectionString);

        public DashboardModel()
        {
        }
    }
}
