using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for EditBranchWindow.xaml
    /// </summary>
    public partial class EditBranchWindow : Window
    {
        private Branch _branch;
        public EditBranchWindow(Branch inputBranch)
        {
            _branch = inputBranch;
            InitializeComponent();
        }

        private void EditBranchWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _branch;
        }

        private void ApplyChange_Click(object sender, RoutedEventArgs e)
        {
            //create query for stored procedure
            SqlCommand sqlCmd = new SqlCommand($"USP_CHINHSUA_CHINHANHDOITAC", MainWindow.sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            //create parameter 
            sqlCmd.Parameters.Add(new SqlParameter("@MADT", _branch.ProviderID));
            sqlCmd.Parameters.Add(new SqlParameter("@MACN", _branch.BranchID));
            sqlCmd.Parameters.Add(new SqlParameter("@DIACHI", NewAddressTextBox.Text));

            //execute query
            int ret = sqlCmd.ExecuteNonQuery();

            MessageBox.Show(ret != -1 ? $"Cập nhật thành công({ret})" : "Cập nhật thất bại");
            //Cập nhật lại địa chỉ mới trên listview
            _branch.Address = NewAddressTextBox.Text;
        }
    }
}
