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

namespace CHUYENHANGONLINE.Admin
{
    /// <summary>
    /// Interaction logic for AddAdminWindow.xaml
    /// </summary>
    public partial class AddAdminWindow : Window
    {
        public AddAdminWindow()
        {
            InitializeComponent();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text) || string.IsNullOrWhiteSpace(PasswordTextBox.Password)
                                                                || string.IsNullOrWhiteSpace(NameTextBox.Text) || string.IsNullOrWhiteSpace(EmailTextBox.Text) || string.IsNullOrWhiteSpace(TelTextBox.Text))
            {
                MessageBox.Show("Kiểm tra lại dữ liệu nhập");
                return;
            }
            using var cmd = new SqlCommand("USP_THEMTAIKHOAN_NVQT", MainWindow.sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TENDANGNHAP", SqlDbType.NVarChar).Value = UsernameTextBox.Text;
            cmd.Parameters.Add("@MATKHAU", SqlDbType.NVarChar).Value = PasswordTextBox.Password;
            cmd.Parameters.Add("@VAITRO", SqlDbType.NVarChar).Value = "admin";
            cmd.Parameters.Add("@HOTEN", SqlDbType.NVarChar).Value = NameTextBox.Text;
            cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = EmailTextBox.Text;
            cmd.Parameters.Add("@SDT", SqlDbType.VarChar).Value = TelTextBox.Text;
            SqlParameter returnValue = new SqlParameter();
            returnValue.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(returnValue);

            cmd.ExecuteNonQuery();
            int res = (int)returnValue.Value;
            MessageBox.Show(res == 0 ? "Tên đăng nhập đã tồn tại" : "Thêm thành công");
        }
    }
}
