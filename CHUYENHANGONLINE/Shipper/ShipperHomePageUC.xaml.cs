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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CHUYENHANGONLINE.Shipper
{
    /// <summary>
    /// Interaction logic for ShipperHomePageUC.xaml
    /// </summary>
    public partial class ShipperHomePageUC : UserControl
    {
        private Shipper _shipper = MainWindow.User as Shipper;
        public ShipperHomePageUC()
        {
            InitializeComponent();
        }

        private void ShipperHomePageUC_OnLoaded(object sender, RoutedEventArgs e)
        {
            HelloLabel.Content = $"Xin chào {_shipper.Name}";
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var shipperProfile = new ShipperProfile();
            shipperProfile.Show();
        }

        private void ShowOrderListButton_Click(object sender, RoutedEventArgs e)
        {
            var shipperOrderListWindow = new ShipperOrderList();
            shipperOrderListWindow.Show();
        }
        private void ShowPickedOrderListButton_CLick(object sender, RoutedEventArgs e)
        {
            var shippePickedOrderListWindow = new ShipperPickedOrders();
            shippePickedOrderListWindow.Show();
        }

    }
}
