using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace CHUYENHANGONLINE.Customer
{
    /// <summary>
    /// Interaction logic for ProviderList.xaml
    /// </summary>
    public partial class ProviderListWindow : Window
    {
        private BindingList<Provider.Provider> _providerList;
        public ProviderListWindow(BindingList<Provider.Provider> providerList)
        {
            _providerList = providerList;
            InitializeComponent();
        }

        private void ProviderList_OnLoaded(object sender, RoutedEventArgs e)
        {
            ProviderListView.Items.Clear();
            ProviderListView.ItemsSource = _providerList;
        }
        private void BtnShopProductList_Onclick(object sender, RoutedEventArgs e)
        {
            int index = ProviderListView.SelectedIndex;
            int providerID = _providerList[index].Id;
            BindingList<Product> productList = new();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.CommandText = "USP_XEMDANHSACHSANPHAM_KHACHHANG";
            sqlCmd.Connection = MainWindow.sqlCon;
            SqlParameter maDtParameter = new SqlParameter("@MADT", SqlDbType.Int);
            maDtParameter.Value = providerID;
            sqlCmd.Parameters.Add(maDtParameter);
            SqlDataReader reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
                var temp = new Product()
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
                productList.Add(temp);
            }

            reader.Close();
            var productListView = new ProductListWindow(productList);
            productListView.Show();
        }
    }
}
