using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLTDatabaseEditor
{
    public class ListViewItemViewModel : ViewModelBase
    {
        private string _title;

        public ListViewItemViewModel(string title)
        {
            _title = title;
        }

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                SetProperty(ref _title, value);
            }
        }
    }
}
