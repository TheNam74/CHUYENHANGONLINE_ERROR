using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHUYENHANGONLINE
{
    public class Branch : INotifyPropertyChanged
    {
        public int BranchID { get; set; }
        public int ProviderID { get; set; }
        public string Address { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
