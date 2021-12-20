using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace CHUYENHANGONLINE.Shipper
{
    /// <summary>
    /// Interaction logic for ShipperProfile.xaml
    /// </summary>
    public partial class ShipperProfile : Window
    {
        public ShipperProfile()
        {
            InitializeComponent();
            var shipper = MainWindow.User as Shipper;
            Name.Text = shipper.Name;
            Email.Text = shipper.Email;
            Phone.Text = shipper.Tel;
            Address.Text = shipper.Address;
            VRP.Text = shipper.Plate;
            BankAccount.Text = shipper.BankAccount;
            Area.Text = shipper.Area;
            CitizenId.Text = shipper.CitizenId;
        }
    }
}
