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
    /// Interaction logic for CustomerListWindow.xaml
    /// </summary>
    public partial class CustomerListWindow : Window
    {
        private BindingList<Customer.Customer> _customerList;
        public CustomerListWindow(BindingList<Customer.Customer> customerList)
        {
            _customerList = customerList;
            InitializeComponent();
        }
        private void CustomerListWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            CustomerListView.Items.Clear();
            CustomerListView.ItemsSource = _customerList;
        }
        
        private void LockMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var customer = CustomerListView.SelectedItem as Customer.Customer;
            using (SqlCommand cmd = new SqlCommand("USP_KHOATAIKHOAN", MainWindow.sqlCon))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MATK", SqlDbType.VarChar).Value = customer.LoginId;
                cmd.ExecuteNonQuery();
            }

            customer.Status = false;
        }

        private void UnlockMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var customer = CustomerListView.SelectedItem as Customer.Customer;
            using (SqlCommand cmd = new SqlCommand("USP_MOKHOATAIKHOAN", MainWindow.sqlCon))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MATK", SqlDbType.VarChar).Value = customer.LoginId;
                cmd.ExecuteNonQuery();
            }
            customer.Status = true;
        }
    }
}
