using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHUYENHANGONLINE
{
    public class Order :INotifyPropertyChanged
    {
        public int OrdID { get; set; }
        public string Payments { get; set; }
        public int ShipCost { get; set; }
        public int TotalBill { get; set; }
        public string ShipAddress { get; set; }
        public string Status { get; set; }
        public int? ShipID { get; set; }
        public int CusID { get; set; }
        public DateTime? CreDate { get; set; }
        public DateTime? ShipDate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
