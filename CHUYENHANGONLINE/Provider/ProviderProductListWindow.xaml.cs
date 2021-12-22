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
    /// Interaction logic for ProviderProductListWindow.xaml
    /// </summary>
    public partial class ProviderProductListWindow : Window
    {
        static Provider _provider;
        public BindingList<Product> _productList = new BindingList<Product>();
        public ProviderProductListWindow()
        {
            InitializeComponent();
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            int index = ProductListView.SelectedIndex;
            var editProductWindow = new EditProductWindow(_productList[index]);
            editProductWindow.Show();

            //GetProductList();
        }

        private void ProviderProductList_OnLoaded(object sender, RoutedEventArgs e)
        {
            _provider = MainWindow.User as Provider;
            GetProductList();
        }

        //Utility
        private void GetProductList() 
        {
            //Clear mảng trước
            _productList.Clear();

            //create query for stored procedure
            SqlCommand sqlCmd = new SqlCommand();
            string query = $"USP_XEM_SANPHAMDOITAC";
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
                Product product = new Product
                {
                    ProviderID = reader.GetInt32(0),
                    BranchID = reader.GetInt32(1),
                    ProID = reader.GetInt32(2),
                    ProName = reader.GetString(3),
                    ProInfo = reader.SafeGetString(4),
                    ProPrice = reader.GetFloat(5),
                    ProUnit = reader.GetString(6),
                    ProAmount = reader.GetInt32(7),
                };
                _productList.Add(product);
            }
            reader.Close();

            ProductListView.Items.Clear();
            ProductListView.ItemsSource = _productList;
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            Product _product = _productList[ProductListView.SelectedIndex];

            SqlCommand sqlCmd = new SqlCommand($"USP_CAU1_1b", MainWindow.sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            //create parameter 
            sqlCmd.Parameters.Add(new SqlParameter("@MADT", _product.ProviderID));
            sqlCmd.Parameters.Add(new SqlParameter("@MACN", _product.BranchID));
            sqlCmd.Parameters.Add(new SqlParameter("@MASP", _product.ProID));

            //execute query
            int ret = sqlCmd.ExecuteNonQuery();

            MessageBox.Show(ret != -1 ? $"Xóa thành công({ret})" : "Xóa thất bại");

            _productList.RemoveAt(ProductListView.SelectedIndex);
        }
    }
}
