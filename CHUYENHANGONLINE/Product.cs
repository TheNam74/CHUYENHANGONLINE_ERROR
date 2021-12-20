using System.ComponentModel;

namespace CHUYENHANGONLINE
{
    public class Product :  INotifyPropertyChanged
    {
        public int ProviderID { get; set; }
        public int BranchID { get; set; }
        public int ProID { get; set; }
        public string ProName { get; set; }
        public string ProInfo { get; set; }
        public float ProPrice { get; set; }
        public string ProUnit { get; set; }
        public int ProAmount { get; set; }

        public int? BuyingAmount { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
