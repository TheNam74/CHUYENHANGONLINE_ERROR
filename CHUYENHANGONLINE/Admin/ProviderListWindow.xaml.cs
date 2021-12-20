using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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

namespace CHUYENHANGONLINE.Admin
{
    /// <summary>
    /// Interaction logic for ProviderListWindow.xaml
    /// </summary>
    public partial class ProviderListWindow : Window
    {
        private BindingList<Provider.Provider> _providerList;

        public ProviderListWindow(BindingList<Provider.Provider> productList)
        {
            _providerList = productList;
            InitializeComponent();

        }
        private void ProviderListWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            ProviderListView.Items.Clear();
            ProviderListView.ItemsSource = _providerList;
        }

        private void LockMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var provider = ProviderListView.SelectedItem as Provider.Provider;
            using (SqlCommand cmd = new SqlCommand("USP_KHOATAIKHOAN", MainWindow.sqlCon))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MATK", SqlDbType.VarChar).Value = provider.LoginId;
                cmd.ExecuteNonQuery();
            }

            provider.Status = false;
        }

        private void UnlockMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var provider = ProviderListView.SelectedItem as Provider.Provider;
            using (SqlCommand cmd = new SqlCommand("USP_MOKHOATAIKHOAN", MainWindow.sqlCon))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MATK", SqlDbType.VarChar).Value = provider.LoginId;
                cmd.ExecuteNonQuery();
            }
            provider.Status = true;
        }
    }
}
