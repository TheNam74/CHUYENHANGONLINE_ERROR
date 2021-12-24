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

        DataSet GetDataSetFromProc (string query, List<SqlParameter> parameters)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = query;

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    sqlCmd.Parameters.Add(param);
                }
            }

            sqlCmd.Connection = MainWindow.sqlCon;

            SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;
        }
        private void PickedOrderList_Loaded(object sender, RoutedEventArgs e)
        {
            PickedOrderList.Items.Clear();
            PickedOrderList.ItemsSource = _pickedOrderList;

            _shipper = MainWindow.User as Shipper;

            //string query = $"usp_select_phivanchuyendonhang";
            string query = $"usp_cau5_3a";

            SqlParameter param2 = new SqlParameter("@matx", SqlDbType.Int);
            param2.Value = _shipper.Id;

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(param2);

            DataSet result = GetDataSetFromProc(query, parameters);

            NumOfDeliveredOrder.Text = result.Tables[0].Rows[0][0].ToString();
            Revenue.Text =  result.Tables[0].Rows[0][1].ToString();

            for(var i = 0; i < result.Tables[1].Rows.Count; i++)
            {
                Order order = new Order
                {
                    OrdID =       (int)(result.Tables[1].Rows[i][0]),
                    Payments =    result.Tables[1].Rows[i][1].ToString(),
                    ShipCost =    (int)result.Tables[1].Rows[i][2],
                    TotalBill =   (int)result.Tables[1].Rows[i][3],
                    ShipAddress = result.Tables[1].Rows[i][4].ToString(),
                    Status =      result.Tables[1].Rows[i][5].ToString(),
                    ShipID =      (int)result.Tables[1].Rows[i][6],
                    CusID =       (int)result.Tables[1].Rows[i][7],
                    CreDate =     (DateTime)result.Tables[1].Rows[i][8],
                    ShipDate = result.Tables[1].Rows[i][9] == DBNull.Value?null: (DateTime?)result.Tables[1].Rows[i][9],
                };
                _pickedOrderList.Add(order);

            }
            //MainWindow.sqlCon.Close();
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
                string storedProc = $"USP_CAU5_3b";

                SqlParameter param = new SqlParameter("@madh", SqlDbType.Int);
                SqlParameter param2 = new SqlParameter("@tinhtrang", SqlDbType.NVarChar);
                SqlParameter param3 = new SqlParameter("@ngaygiao", SqlDbType.Date);
                SqlParameter param11 = new SqlParameter("@returnvalue", SqlDbType.Int);

                param.Value = order.OrdID;
                param2.Value = "đã giao";//-----------------------------------------
                param3.Value = DateTime.Today;

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = MainWindow.sqlCon;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = storedProc;

                sqlCmd.Parameters.Add(param);
                sqlCmd.Parameters.Add(param2);
                sqlCmd.Parameters.Add(param3);
                sqlCmd.Parameters.Add(param11);

                sqlCmd.Parameters["@returnvalue"].Direction = ParameterDirection.ReturnValue;

                SqlDataReader reader = sqlCmd.ExecuteReader();
                reader.Close();

                int checkError = (int)sqlCmd.Parameters["@returnvalue"].Value;

                if (checkError == -1)
                {
                    MessageBox.Show("Xảy ra lỗi khi xác nhận giao hàng");
                    return;
                }

                //exec SP query doanh thu cua tai xe
                string query = $"usp_select_phivanchuyendonhang";
                SqlParameter param4 = new SqlParameter("@matx", SqlDbType.Int);
                param4.Value = _shipper.Id;

                List<SqlParameter> parameters = new List<SqlParameter>();
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