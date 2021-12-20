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

namespace CHUYENHANGONLINE.Admin
{
    /// <summary>
    /// Interaction logic for StaffListWindow.xaml
    /// </summary>
    public partial class StaffListWindow : Window
    {
        private BindingList<Staff.Staff> _staffList;
        public StaffListWindow(BindingList<Staff.Staff> staffListt)
        {
            _staffList = staffListt;
            InitializeComponent();
        }
        private void StaffListWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            StaffListView.Items.Clear();
            StaffListView.ItemsSource = _staffList;
        }

        private void LockMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var staff = StaffListView.SelectedItem as Staff.Staff;
            using (SqlCommand cmd = new SqlCommand("USP_KHOATAIKHOAN", MainWindow.sqlCon))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MATK", SqlDbType.VarChar).Value = staff.LoginId;
                cmd.ExecuteNonQuery();
            }

            staff.Status = false;
        }

        private void UnlockMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var staff = StaffListView.SelectedItem as Staff.Staff;
            using (SqlCommand cmd = new SqlCommand("USP_MOKHOATAIKHOAN", MainWindow.sqlCon))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MATK", SqlDbType.VarChar).Value = staff.LoginId;
                cmd.ExecuteNonQuery();
            }
            staff.Status = true;
        }
    }
}
