using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace CHUYENHANGONLINE.Customer
{
    /// <summary>
    /// Interaction logic for CustomerHomePageUC.xaml
    /// </summary>
    public partial class CustomerHomePageUC : UserControl
    {

        public static BindingList<OrderDetail> CartItem = new();

        private Customer _customer = (MainWindow.User as Customer);
        public CustomerHomePageUC()
        {
            InitializeComponent();
        }

        private void CustomerHomePageUC_OnLoaded(object sender, RoutedEventArgs e)
        {
            HelloLabel.Content = $"Xin chào {_customer.Name}";
        }

        private void Profile_OnClick(object sender, RoutedEventArgs e)
        {
            var customerDetailsWindow = new CustomerDetailsWindow();
            customerDetailsWindow.Show();
        }


        private void ProviderList_OnClick(object sender, RoutedEventArgs e)
        {

            BindingList<Provider.Provider> providerList = new();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.CommandText = "USP_XEMDANHSACHDOITAC_KHACHHANG";
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
                    Email = reader.GetString(6),
                    ContractDate = reader.GetDateTime(9),
                    OrderAmount = reader.GetInt32(10),
                    ProductType = reader.GetString(11),
                    BranchAmount = reader.GetInt32(12),
                    LoginId = reader.GetInt32(13),
                    Commission = reader.GetFloat(14),
                };
                providerList.Add(temp);
            }

            reader.Close();
            var providerListView = new ProviderListWindow(providerList);
            providerListView.Show();
        }

        private void OrderList_OnClick(object sender, RoutedEventArgs e)
        {
            BindingList<Order> orderList = new();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "USP_CAU5_2b";
            sqlCmd.Connection = MainWindow.sqlCon;
            SqlParameter maKHParameter = new SqlParameter("@MAKH", SqlDbType.Int);
            maKHParameter.Value = MainWindow.User.Id;
            sqlCmd.Parameters.Add(maKHParameter);
            SqlDataReader reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                var temp = new Order()
                {
                    OrdID = reader.GetInt32(0),
                    Payments = reader.GetString(1),
                    ShipCost = reader.GetInt32(2),
                    TotalBill = reader.GetInt32(3),
                    ShipAddress = reader.GetString(4),
                    Status = reader.GetString(5),
                    ShipID = reader.SafeGetInt(6),
                    CusID = reader.GetInt32(7),
                    CreDate = reader.SafeGetDate(8),
                    ShipDate = reader.SafeGetDate(9)
                };
                orderList.Add(temp);
            }

            reader.Close();
            var providerListView = new OrderListWindow(orderList);
            providerListView.Show();
        }

        private void Cart_OnClick_OnClick(object sender, RoutedEventArgs e)
        {
            var cartView = new CartWindow();
            cartView.Show();
        }

        private void BtnLogout_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
