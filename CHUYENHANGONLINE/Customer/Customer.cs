using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHUYENHANGONLINE.Customer
{
    public class Customer:IUser,INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int LoginId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
