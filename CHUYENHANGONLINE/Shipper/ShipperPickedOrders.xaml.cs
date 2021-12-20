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
    /// Interaction logic for ShipperPickedOrders.xaml
    /// </summary>
    public partial class ShipperPickedOrders : Window
    {
        static Shipper _shipper;
        BindingList<Order> _pickedOrderList = new BindingList<Order>();
        public ShipperPickedOrders()
        {
            InitializeComponent();
        }


        public void ExecuteQuery(string query, string commandType, ref SqlDataReader reader, List<SqlParameter> parameters = null)
        {
            //create query 
            SqlCommand sqlCmd = new SqlCommand();
            if (commandType == "storedProc")
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
            }
            else if (commandType == "query")
            {
                sqlCmd.CommandType = CommandType.Text;
            }

            sqlCmd.CommandText = query;

            //create parameter 
            //add parameter to stored procedure
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    sqlCmd.Parameters.Add(param);
                }
            }

            //connection to database
            sqlCmd.Connection = MainWindow.sqlCon;

            //execute query
            reader = sqlCmd.ExecuteReader();
        }
        private void PickedOrderList_Loaded(object sender, RoutedEventArgs e)
        {
            _shipper = MainWindow.User as Shipper;
            SqlDataReader reader = null;

            string storedProc = $"usp_select_mataixedonhang";
            SqlParameter param = new SqlParameter("@matx", SqlDbType.Int);
            param.Value = _shipper.Id;

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(param);

            ExecuteQuery(storedProc, "storedProc", ref reader, parameters);
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
                _pickedOrderList.Add(order);
            }
            reader.Close();
            PickedOrderList.Items.Clear();
            PickedOrderList.ItemsSource = _pickedOrderList;

            parameters.Clear();

            string query = $"usp_select_phivanchuyendonhang";
            SqlParameter param2 = new SqlParameter("@matx", SqlDbType.Int);
            param2.Value = _shipper.Id;


            parameters.Add(param2);

            ExecuteQuery(query, "storedProc", ref reader, parameters);
            while (reader.Read())
            {
                NumOfDeliveredOrder.Text = reader.SafeGetInt(0).ToString();
                Revenue.Text = reader.SafeGetInt(1).ToString();

            }
            reader.Close();
        }

        private void UpdateShipSuccess_Click(object sender, RoutedEventArgs e)
        {
            var order = PickedOrderList.SelectedItem as Order;

            if(order.Status == "đã giao")
            {
                MessageBox.Show("Không thể cập nhật tình trạng đơn hàng đã giao");
            }
            else
            {
                string query = $"usp_update_tinhtrangdonhang";
                SqlParameter param = new SqlParameter("@madh", SqlDbType.Int);
                SqlParameter param2 = new SqlParameter("@tinhtrang", SqlDbType.NVarChar);
                SqlParameter param3 = new SqlParameter("@ngaygiao", SqlDbType.Date);
                param.Value = order.OrdID;
                param2.Value = "đã giao";
                param3.Value = DateTime.Today;

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(param);
                parameters.Add(param2);
                parameters.Add(param3);

                SqlDataReader reader = null;
                ExecuteQuery(query, "storedProc", ref reader, parameters);
                reader.Close();
                parameters.Clear();

                //exec SP query doanh thu cua tai xe
                query = $"usp_select_phivanchuyendonhang";
                SqlParameter param4 = new SqlParameter("@matx", SqlDbType.Int);
                param4.Value = _shipper.Id;

                parameters.Add(param4);
            
                ExecuteQuery(query, "storedProc", ref reader, parameters);
                while (reader.Read())
                {
                    NumOfDeliveredOrder.Text = reader.SafeGetInt(0).ToString();
                    Revenue.Text = reader.SafeGetInt(1).ToString();
                }
                reader.Close();

                //cap nhat lai bindlist sau khi update 
                foreach (var child in _pickedOrderList)
                {
                    if (child.OrdID == order.OrdID)
                    {
                        query = $"select * from donhang where madh = {order.OrdID}";
                        ExecuteQuery(query, "query", ref reader);
                        while (reader.Read())
                        {
                            child.Status = "đã giao";
                            child.ShipDate = reader.SafeGetDate(9);
                        }
                        reader.Close();
                    }
                }
            }
        }
    }
}