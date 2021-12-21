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

namespace CHUYENHANGONLINE.Shipper
{
    /// <summary>
    /// Interaction logic for ShipperOrderList.xaml
    /// </summary>
    public partial class ShipperOrderList : Window
    {
        static Shipper _shipper;
        BindingList<Order> _orderList = new BindingList<Order>();

        public ShipperOrderList()
        {
            InitializeComponent();
        }

        private void ShipperOrderList_OnLoaded(object sender, RoutedEventArgs e)
        {
            _shipper = MainWindow.User as Shipper;

            //create query fro stored procedure
            SqlCommand sqlCmd = new SqlCommand();
            string query = $"usp_select_khuvucdonhang";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = query;

            //create parameter 
            SqlParameter param = new SqlParameter("@MAKV", SqlDbType.NVarChar);
            param.Value = _shipper.Area;

            //add parameter to stored procedure
            sqlCmd.Parameters.Add(param);

            //connection to database
            sqlCmd.Connection = MainWindow.sqlCon;

            //execute query
            SqlDataReader reader = sqlCmd.ExecuteReader();
            
            while (reader.Read())
            {
                if (reader.GetString(5) == "chưa duyệt")
                {
                    continue;
                }
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
                _orderList.Add(order);
            }
            OrderList.Items.Clear();
            OrderList.ItemsSource = _orderList;
        }

        public void ExecuteQuery(string query, string commandType, ref SqlDataReader reader, List<SqlParameter> parameters = null )
        {
            //create query 
            SqlCommand sqlCmd = new SqlCommand();
            if (commandType == "storedProc")
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
            }
            else if(commandType == "query")
            {
                sqlCmd.CommandType = CommandType.Text;
            }

            sqlCmd.CommandText = query;

            //create parameter 
            //add parameter to stored procedure
            if (parameters != null)
            {
                foreach(var param in parameters)
                {
                    sqlCmd.Parameters.Add(param);
                }
            }

            //connection to database
            sqlCmd.Connection = MainWindow.sqlCon;

            //execute query
            reader = sqlCmd.ExecuteReader();
        }


        private void PickOrder_Click(object sender, RoutedEventArgs e)
        {
            var order = OrderList.SelectedItem as Order;
            if(order.Status!="đang chờ"&&order.Status!="chờ duyệt")
            {
                MessageBox.Show("Không thể nhận đơn hàng");
            }
            else
            {
                SqlDataReader reader = null;
                //Cập nhật mã tài xế vào ddonw hàng
                string storedProc = $"usp_update_mataixedonhang";
                SqlParameter param = new SqlParameter("@matx", SqlDbType.Int);
                SqlParameter param2 = new SqlParameter("@madh", SqlDbType.Int);
                param.Value = _shipper.Id;
                param2.Value = order.OrdID;

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(param);
                parameters.Add(param2);

                ExecuteQuery(storedProc, "storedProc", ref reader, parameters);
                reader.Close();

                //Cập nhật tình trạng đơn hàng thành "đang giao"
                storedProc = $"usp_update_tinhtrangdonhang";
                SqlParameter param3 = new SqlParameter("@madh", SqlDbType.Int);
                SqlParameter param4 = new SqlParameter("@tinhtrang", SqlDbType.NVarChar);
                param3.Value = order.OrdID;
                param4.Value = "đang giao";

                parameters.Clear();
                parameters.Add(param3);
                parameters.Add(param4);

                ExecuteQuery(storedProc, "storedProc", ref reader, parameters);
                reader.Close();
                foreach (var child in _orderList)
                {
                    if (child.OrdID == order.OrdID)
                    {
                        string query = $"select * from donhang where madh = {order.OrdID}";
                        ExecuteQuery(query, "query", ref reader);
                        while (reader.Read())
                        {
                            child.Status = reader.GetString(5);
                            child.ShipID = reader.SafeGetInt(6);
                        }
                        reader.Close();
                    }
                }
            }
        }
    }
}
