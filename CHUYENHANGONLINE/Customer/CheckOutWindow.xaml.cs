using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace CHUYENHANGONLINE.Customer
{
    /// <summary>
    /// Interaction logic for CheckOutWindow.xaml
    /// </summary>
    public partial class CheckOutWindow : Window
    {
        private Order order = new();
        private string[] LoadComboBoxData()
        {
            string[] strArray = {
                "Tân Phú",
                "Thủ Đức",
                "Tân Bình",
                "Bình Tân",
                "Gò Vấp",
                "Bình Thạnh",
                "Phú Nhuận"
            };
            return strArray;
        }
        public CheckOutWindow()
        {
            InitializeComponent();
        }

        private void Onl_OnChecked(object sender, RoutedEventArgs e)
        {
            order.Payments = "ONL";
        }

        private void Cod_OnChecked(object sender, RoutedEventArgs e)
        {
            order.Payments = "COD";
        }

        private void CheckOutWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            ComboBoxZone.ItemsSource = LoadComboBoxData();
        }
        private void Confirm_OnClicks(object sender, RoutedEventArgs e)
        {
            order.ShipAddress = AddressNumber.Text + ", " + ComboBoxZone.Text;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "USP_CAU2_1a";
            sqlCmd.Connection = MainWindow.sqlCon;
            SqlParameter paymentsParameter = new SqlParameter("@HINHTHUCTHANHTOAN", SqlDbType.VarChar);
            paymentsParameter.Value = order.Payments;
            SqlParameter shipAddressParameter = new SqlParameter("@DIACHIGIAO", SqlDbType.NVarChar);
            shipAddressParameter.Value = order.ShipAddress;
            SqlParameter LoginIdParameter = new SqlParameter("@MATK", SqlDbType.Int);
            LoginIdParameter.Value = MainWindow.User.LoginId;
            SqlParameter returnValueParameter = sqlCmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            returnValueParameter.Direction = ParameterDirection.ReturnValue;
            sqlCmd.Parameters.Add(paymentsParameter);
            sqlCmd.Parameters.Add(shipAddressParameter);
            sqlCmd.Parameters.Add(LoginIdParameter);
            sqlCmd.ExecuteNonQuery();
            order.OrdID = (int)returnValueParameter.Value;
            if (order.OrdID == -1)
            {
                MessageBox.Show("Them don hang that bai");
                return;
            }
            foreach (var orderDetail in CustomerHomePageUC.CartItem)
            {
                SqlCommand sqlOrderDetail = new SqlCommand();
                sqlOrderDetail.CommandType = CommandType.StoredProcedure;
                sqlOrderDetail.CommandText = "USP_THEMCHITIETDH_KHACHHANG";
                sqlOrderDetail.Connection = MainWindow.sqlCon;
                SqlParameter ordIdParameter = new SqlParameter("@MADH", SqlDbType.Int);
                SqlParameter branchParameter = new SqlParameter("@MACN", SqlDbType.Int);
                SqlParameter providerParameter = new SqlParameter("@MADT", SqlDbType.Int);
                SqlParameter productParameter = new SqlParameter("@MASP", SqlDbType.Int);
                SqlParameter buyingAmounParameter = new SqlParameter("@INPUTSL", SqlDbType.Int);

                ordIdParameter.Value = order.OrdID;
                branchParameter.Value = orderDetail.BranchID;
                productParameter.Value = orderDetail.ProID;
                providerParameter.Value = orderDetail.ProviderID;
                buyingAmounParameter.Value = orderDetail.Amount;

                sqlOrderDetail.Parameters.Add(ordIdParameter);
                sqlOrderDetail.Parameters.Add(branchParameter);
                sqlOrderDetail.Parameters.Add(productParameter);
                sqlOrderDetail.Parameters.Add(providerParameter);
                sqlOrderDetail.Parameters.Add(buyingAmounParameter);

                sqlOrderDetail.ExecuteNonQuery();
            }

            //xoá bindinglist
            CustomerHomePageUC.CartItem.Clear();
        }
    }
}
