using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHUYENHANGONLINE.Shipper
{
    public class Shipper: IUser, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int LoginId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public string Plate { get; set; }
        public string BankAccount { get; set; }
        public string Area { get; set; }
        public string CitizenId { get; set; }
        public bool Status { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
