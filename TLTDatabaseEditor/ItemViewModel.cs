using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLTDatabaseEditor
{
    public class ItemViewModel : ViewModelBase
    {
        private string _name;

        public ItemViewModel(string tabName)
        {
            TabName = tabName;
        }

        public string TabName
        {
            get;
            private set;
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                SetProperty(ref _name, value);
            }
        }
    }
}
