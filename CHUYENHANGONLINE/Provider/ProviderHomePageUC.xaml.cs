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

namespace CHUYENHANGONLINE.Provider
{
    /// <summary>
    /// Interaction logic for ProviderHomePageUC.xaml
    /// </summary>
    public partial class ProviderHomePageUC : UserControl
    {
        private Provider _provider = (MainWindow.User as Provider);

        public ProviderHomePageUC()
        {
            InitializeComponent();
        }
        private void ProviderHomePageUC_OnLoaded(object sender, RoutedEventArgs e)
        {
            HelloLabel.Content = $"Xin chào {_provider.Name}";
        }

        private void BtnLogout_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BranchList_Click(object sender, RoutedEventArgs e)
        {
            var ProvicerBranchListWindow = new ProviderBranchList();
            ProvicerBranchListWindow.Show();
        }

        private void OrderList_Click(object sender, RoutedEventArgs e)
        {
            var ProviderOrderListWindow = new ProviderOrderList();
            ProviderOrderListWindow.Show();
        }

        private void ProductList_Click(object sender, RoutedEventArgs e)
        {
            var providerProductListWindow = new ProviderProductListWindow();
            providerProductListWindow.Show();
        }

        private void ProviderDetails_Click(object sender, RoutedEventArgs e)
        {
            var providerDetailsWindow = new ProviderDetailsWindow();
            providerDetailsWindow.Show();
        }

        private void Revenue_Click(object sender, RoutedEventArgs e)
        {
            var revenueWindow = new RevenueWindow();
            revenueWindow.Show();
        }
    }
}
