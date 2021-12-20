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
    /// Interaction logic for ProviderBranchList.xaml
    /// </summary>
    public partial class ProviderBranchList : Window
    {
        static Provider _provider;
        BindingList<Branch> _branchList = new BindingList<Branch>();
        public ProviderBranchList()
        {
            InitializeComponent();
        }

        private void ProviderBranchList_OnLoaded(object sender, RoutedEventArgs e)
        {
            _provider = MainWindow.User as Provider;
            //create query for stored procedure
            SqlCommand sqlCmd = new SqlCommand();
            string query = $"USP_XEM_DANHSACHCHINHANH";
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
                Branch branch = new Branch
                {
                    BranchID = reader.GetInt32(0),
                    ProviderID = reader.GetInt32(1),
                    Address = reader.GetString(2),
                };
                _branchList.Add(branch);
            }
            reader.Close();
            BranchListView.Items.Clear();
            BranchListView.ItemsSource = _branchList;
        }

        private void OrderDetail_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditBranch_Click(object sender, RoutedEventArgs e)
        {
            int index = BranchListView.SelectedIndex;
            var editBranchWindow = new EditBranchWindow(_branchList[index]);
            editBranchWindow.Show();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            int index = BranchListView.SelectedIndex;
            var addProductWindow = new AddProductWindow(_branchList[index]);
            addProductWindow.Show();
        }
    }
}
