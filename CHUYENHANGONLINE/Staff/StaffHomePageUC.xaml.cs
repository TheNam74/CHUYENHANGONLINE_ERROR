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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CHUYENHANGONLINE.Staff
{
    /// <summary>
    /// Interaction logic for StaffHomePageUC.xaml
    /// </summary>
    public partial class StaffHomePageUC : UserControl
    {
        private Staff _staff = (MainWindow.User as Staff);
        public StaffHomePageUC()
        {
            InitializeComponent();
        }

        private void StaffHomePageUC_OnLoaded(object sender, RoutedEventArgs e)
        {
            HelloLabel.Content = $"Xin chào {_staff.Name}";
        }
        

        private void ProviderListButton_OnClick(object sender, RoutedEventArgs e)
        {
            BindingList<Provider.Provider> providerList = new();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.CommandText = "USP_CAU4_4b";// gây ra lỗi error04 dirty read
            sqlCmd.Connection = MainWindow.sqlCon;
            SqlDataReader reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                var temp = new Provider.Provider()
                {
                    Id = reader.GetInt32(0),
                    TaxCode = reader.GetString(1),
                    Address = reader.GetString(2),
                    Name = reader.GetString(3),
                    Tel = reader.GetString(4),
                    Represent = reader.GetString(5),
                    City = reader.GetString(6),
                    District = reader.GetString(7),
                    Email = reader.GetString(8),
                    ContractDate = reader.SafeGetDate(9),
                    OrderAmount = reader.GetInt32(10),
                    ProductType = reader.GetString(11),
                    BranchAmount = reader.GetInt32(12),
                    LoginId = reader.GetInt32(13),
                    Commission = reader.GetFloat(14),
                };
                providerList.Add(temp);
            }

            reader.Close();
            var providerListWindow = new ProviderList(providerList);
            providerListWindow.ShowDialog();
        }

        private void ProfileButton_OnClick(object sender, RoutedEventArgs e)
        {

            var staffDetailsWindow = new StaffDetails();
            staffDetailsWindow.ShowDialog();
        }

        private void NewProviderListButton_OnClick(object sender, RoutedEventArgs e)
        {
            BindingList<Provider.Provider> providerList = new();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "USP_XEMDANHSACHCHUADUYET_HOPDONGDOITAC";
            sqlCmd.Connection = MainWindow.sqlCon;
            SqlDataReader reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                var temp = new Provider.Provider()
                {
                    Id = reader.GetInt32(0),
                    TaxCode = reader.GetString(1),
                    Address = reader.GetString(2),
                    Name = reader.GetString(3),
                    Tel = reader.GetString(4),
                    Represent = reader.GetString(5),
                    City = reader.GetString(6),
                    District = reader.GetString(7),
                    Email = reader.GetString(8),
                    ContractDate = reader.SafeGetDate(9),
                    OrderAmount = reader.GetInt32(10),
                    ProductType = reader.GetString(11),
                    BranchAmount = reader.GetInt32(12),
                    LoginId = reader.GetInt32(13),
                    Commission = reader.GetFloat(14),
                };
                providerList.Add(temp);
            }

            reader.Close();
            var providerListWindow = new ProviderList(providerList);
            providerListWindow.ShowDialog();
        }

        private void SignOutButton_OnClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
            // System.Windows.Forms.Application.Restart();
        }
    }
}
