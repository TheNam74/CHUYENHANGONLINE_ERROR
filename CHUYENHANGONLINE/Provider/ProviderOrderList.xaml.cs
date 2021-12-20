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

namespace CHUYENHANGONLINE.Provider
{
    /// <summary>
    /// Interaction logic for ProviderOrderList.xaml
    /// </summary>
    public partial class ProviderOrderList : Window
    {
        static Provider _provider;
        BindingList<Order> _orderList = new BindingList<Order>();
        List<OrderDetail> _orderDetailList = new List<OrderDetail>();
        public ProviderOrderList()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void ProviderOrderList_OnLoaded(object sender, RoutedEventArgs e)
        {
            _provider = MainWindow.User as Provider;
            //create query for stored procedure
            SqlCommand sqlCmd = new SqlCommand();
            string query = $"USP_XEM_DOITACDONHANG";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = query;

            //create parameter 
            SqlParameter param = new SqlParameter("@MADT", SqlDbType.Int);
            param.Value = _provider.Id;

            //add parameter to stored procedure
            sqlCmd.Parameters.Add(param);

            //connection to database
            sqlCmd.Connection = MainWindow.sqlCon;

            //execute query
            SqlDataReader reader = sqlCmd.ExecuteReader();

            while (reader.Read())
            {
                Order order = new Order
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
                //Đọc luôn cả order detailt lưu vào list để tí đỡ phải đọc
                OrderDetail orderDetail = new OrderDetail
                {
                    OrdID = reader.GetInt32(10),
                    BranchID = reader.GetInt32(11),
                    ProviderID = reader.GetInt32(12),
                    ProID = reader.GetInt32(13),
                    Amount = reader.GetInt32(14),
                    OrderDetailBill = reader.GetFloat(15),
                };
                _orderDetailList.Add(orderDetail);
                _orderList.Add(order);
            }
            reader.Close();

            OrderList.Items.Clear();
            OrderList.ItemsSource = _orderList;
        }

        private void ContractExtendMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReadyToShip_Click(object sender, RoutedEventArgs e)
        {
            int index = OrderList.SelectedIndex;
            //create query for stored procedure
            SqlCommand sqlCmd = new SqlCommand($"USP_CAPNHATTINHTRANGDONHANG_DOITACDONHANG", MainWindow.sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            //create parameter 
            sqlCmd.Parameters.Add(new SqlParameter("@MADH", _orderList[index].OrdID));

            //execute query
            int ret = sqlCmd.ExecuteNonQuery();

            //Cập nhật lại trạng thái mới trên listview
            SqlCommand sqlCmd2 = new SqlCommand($"USP_LAYTRANGTHAI_MOTDONHANG", MainWindow.sqlCon);
            sqlCmd2.CommandType = CommandType.StoredProcedure;

            sqlCmd2.Parameters.Add(new SqlParameter("@MADH", _orderList[index].OrdID));
            SqlDataReader reader = sqlCmd2.ExecuteReader();
            if (reader.Read())
            {
                _orderList[index].Status = reader.GetString(0);
                MessageBox.Show(reader.GetString(0));
            }
            reader.Close();
        }

        private void OrderDetail_Click(object sender, RoutedEventArgs e)
        {
            int index = OrderList.SelectedIndex;
            var orderDetailsWindow = new OrderDetailsWindow(_orderDetailList[index]);
            orderDetailsWindow.Show();
        }
    }
}
