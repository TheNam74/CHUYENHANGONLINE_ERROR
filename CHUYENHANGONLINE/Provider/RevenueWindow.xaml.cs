using System;
using System.Collections.Generic;
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
    /// Interaction logic for RevenueWindow.xaml
    /// </summary>
    public partial class RevenueWindow : Window
    {
        private Provider _provider;
        public RevenueWindow()
        {
            InitializeComponent();
        }

        private void RevenueWindow_OnLoad(object sender, RoutedEventArgs e)
        {
            Revenue.Text = "0";

            _provider = MainWindow.User as Provider;
            //create query for stored procedure
            SqlCommand sqlCmd = new SqlCommand();
            string query = $"USP_TINHDOANHTHU_DOITAC";
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

            if (!reader.Read())
            {
                MessageBox.Show($"{_provider.Id}");
            }

            Revenue.Text = reader.SafeGetInt(0).ToString();

            reader.Close();
        }
    }
}
