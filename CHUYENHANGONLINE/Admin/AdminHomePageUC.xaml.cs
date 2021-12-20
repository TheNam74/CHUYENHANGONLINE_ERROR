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
using CHUYENHANGONLINE.Staff;

namespace CHUYENHANGONLINE.Admin
{
    /// <summary>
    /// Interaction logic for AdminHomePageUC.xaml
    /// </summary>
    public partial class AdminHomePageUC : UserControl
    {
        public string temp;
        private Staff.Staff _admin = (MainWindow.User as Staff.Staff);
        public AdminHomePageUC()
        {
            InitializeComponent();
        }
        private void AdminHomePageUC_OnLoaded(object sender, RoutedEventArgs e)
        {
            HelloLabel.Content = $"Xin chào {_admin.Name}";
        }

        private void SignOutButton_OnClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void ProfileButton_OnClick(object sender, RoutedEventArgs e)
        {
            var adminDetailsWindow = new AdminDetailsWindow();
            adminDetailsWindow.ShowDialog();
        }

        private void AdminListButton_OnClick(object sender, RoutedEventArgs e)
        {
            BindingList<Staff.Staff> adminList = new();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.CommandText = "USP_DANHSACH_ADMIN";
            sqlCmd.Connection = MainWindow.sqlCon;
            SqlDataReader reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                var temp = new Staff.Staff()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Tel = reader.GetString(3),
                    LoginId = reader.GetInt32(4),
                    Status = reader.GetBoolean(5),
                };
                adminList.Add(temp);
            }

            reader.Close();
            var staffListWindow = new AdminListWindow(adminList);
            staffListWindow.ShowDialog();
        }

        private void StaffListButton_OnClick(object sender, RoutedEventArgs e)
        {
            BindingList<Staff.Staff> staffList = new();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.CommandText = "USP_DANHSACH_STAFF";
            sqlCmd.Connection = MainWindow.sqlCon;
            SqlDataReader reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                var temp = new Staff.Staff()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Tel = reader.GetString(3),
                    LoginId = reader.GetInt32(4),
                    Status = reader.GetBoolean(5),
                };
                staffList.Add(temp);
            }

            reader.Close();
            var staffListWindow = new StaffListWindow(staffList);
            staffListWindow.ShowDialog();
        }

        private void ProviderListButton_OnClick(object sender, RoutedEventArgs e)
        {
            BindingList<Provider.Provider> providerList = new();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.CommandText = "USP_DANHSACH_PROVIDER";
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
                    Status = reader.GetBoolean(15),
                };
                providerList.Add(temp);
            }

            reader.Close();
            var providerListWindow = new ProviderListWindow(providerList);
            providerListWindow.ShowDialog();
        }

        private void CustomerListButton_OnClick(object sender, RoutedEventArgs e)
        {
            BindingList<Customer.Customer> customerList = new();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.CommandText = "USP_DANHSACH_CUSTOMER";
            sqlCmd.Connection = MainWindow.sqlCon;
            SqlDataReader reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                var temp = new Customer.Customer()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Tel = reader.GetString(3),
                    Address = reader.GetString(4),
                    LoginId = reader.GetInt32(5),
                    Status = reader.GetBoolean(6),
                };
                customerList.Add(temp);
            }

            reader.Close();
            var customerListWindow = new CustomerListWindow(customerList);
            customerListWindow.ShowDialog();
        }

        private void DeliverListButton_OnClick(object sender, RoutedEventArgs e)
        {
            BindingList<Shipper.Shipper> deliverList = new();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.CommandText = "USP_DANHSACH_DELIVER";
            sqlCmd.Connection = MainWindow.sqlCon;
            SqlDataReader reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                var temp = new Shipper.Shipper()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Tel = reader.GetString(3),
                    Address = reader.GetString(4),
                    Plate = reader.GetString(5),
                    BankAccount = reader.GetString(6),
                    Area = reader.GetString(7),
                    CitizenId = reader.GetString(8),
                    LoginId = reader.GetInt32(9),
                    Status = reader.GetBoolean(10),
                };
                deliverList.Add(temp);
            }

            reader.Close();
            var deliverListWindow = new DeliverListWindow(deliverList);
            deliverListWindow.ShowDialog();
        }

        private void AddStaffButton_OnClick(object sender, RoutedEventArgs e)
        {
            var addStaffWindow = new AddStaffWindow();
            addStaffWindow.ShowDialog();
        }

        private void AddAdminButton_OnClick(object sender, RoutedEventArgs e)
        {
            var addAdminWindow = new AddAdminWindow();
            addAdminWindow.ShowDialog();
        }
    }
}
