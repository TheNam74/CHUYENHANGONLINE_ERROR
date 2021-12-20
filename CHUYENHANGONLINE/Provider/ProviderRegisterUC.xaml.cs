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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CHUYENHANGONLINE.Provider
{
    /// <summary>
    /// Interaction logic for ProviderRegisterUC.xaml
    /// </summary>
    public partial class ProviderRegisterUC : UserControl
    {
        public ProviderRegisterUC()
        {
            InitializeComponent();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            //Check thông tin
            if (string.IsNullOrWhiteSpace(TaxCodeTextBox.Text) || string.IsNullOrWhiteSpace(AddressTextBox.Text)
                || string.IsNullOrWhiteSpace(NameTextBox.Text) || string.IsNullOrWhiteSpace(PhoneTextBox.Text)
                || string.IsNullOrWhiteSpace(RepresentTextBox.Text) || string.IsNullOrWhiteSpace(CityTextBox.Text)
                || string.IsNullOrWhiteSpace(DistrictTextBox.Text) || string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            //Lấy thông tin đăng kí từ màn hình xuống
            string MST = TaxCodeTextBox.Text;
            string DIACHI = AddressTextBox.Text;
            string TEN = NameTextBox.Text;
            string SDT = PhoneTextBox.Text;
            string NGUOIDAIDIEN = RepresentTextBox.Text;
            string THANHPHO = CityTextBox.Text;
            string QUAN = DistrictTextBox.Text;
            string EMAIL = EmailTextBox.Text;
            string LOAIHANG = ProductTypeTextBox.Text;
            string TENDANGNHAP = UserNameTextBox.Text;
            string MATKHAU = PasswordTextBox.Text;
            int SOCHINHANH;
            int SODONMOINGAY;

            if (!int.TryParse(DepartAmountTextBox.Text, out SOCHINHANH))
            {
                MessageBox.Show($"Số chi nhánh phải là số");
                return;
            }
            if (!int.TryParse(OrderCountTextBox.Text, out SODONMOINGAY))
            {
                MessageBox.Show($"Số đơn mỗi ngày phải là số");
                return;
            }

            //create query for stored procedure
            SqlCommand sqlCmd = new SqlCommand($"USP_DANGKITAIKHOAN_DOITAC", MainWindow.sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            //create parameter 
            sqlCmd.Parameters.Add(new SqlParameter("@MST", MST));
            sqlCmd.Parameters.Add(new SqlParameter("@DIACHI", DIACHI));
            sqlCmd.Parameters.Add(new SqlParameter("@TEN", TEN));
            sqlCmd.Parameters.Add(new SqlParameter("@SDT", SDT));
            sqlCmd.Parameters.Add(new SqlParameter("@NGUOIDAIDIEN", NGUOIDAIDIEN));
            sqlCmd.Parameters.Add(new SqlParameter("@THANHPHO", THANHPHO));
            sqlCmd.Parameters.Add(new SqlParameter("@QUAN", QUAN));
            sqlCmd.Parameters.Add(new SqlParameter("@EMAIL", EMAIL));
            sqlCmd.Parameters.Add(new SqlParameter("@SODONMOINGAY", SODONMOINGAY));
            sqlCmd.Parameters.Add(new SqlParameter("@LOAIHANG", LOAIHANG));
            sqlCmd.Parameters.Add(new SqlParameter("@SOCHINHANH", SOCHINHANH));
            sqlCmd.Parameters.Add(new SqlParameter("@TENDANGNHAP", TENDANGNHAP));
            sqlCmd.Parameters.Add(new SqlParameter("@MATKHAU", MATKHAU));

            //execute query
            int ret = sqlCmd.ExecuteNonQuery();

            //MessageBox.Show(ret.ToString());
            MessageBox.Show(ret != -1 ? "Đăng kí thành công" : "Trùng tài khoản");
        }
    }
}
