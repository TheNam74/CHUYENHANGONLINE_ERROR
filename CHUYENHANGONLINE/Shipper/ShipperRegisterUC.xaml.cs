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

namespace CHUYENHANGONLINE.Shipper
{
    /// <summary>
    /// Interaction logic for ShipperRegisterUC.xaml
    /// </summary>
    public partial class ShipperRegisterUC : UserControl
    {
        public ShipperRegisterUC()
        {
            InitializeComponent();
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (UserName.Text == "" || Password.Text =="" || Name.Text =="" || Email.Text==""||
                Phone.Text == "" || Address.Text == "" || VRP.Text == "" || BankAccount.Text == "" || CitizenId.Text == "" || Area.Text == "")
            {
                MessageBox.Show("Các thông tin cần được nhập đầy đủ để thực hiện đăng kí");
            }
            else
            {
                string storedProc = $"usp_insert_taikhoantaixe";

                SqlParameter param = new SqlParameter("@tendangnhap", SqlDbType.NVarChar);
                SqlParameter param2 = new SqlParameter("@matkhau", SqlDbType.NVarChar);
                SqlParameter param3 = new SqlParameter("@hoten", SqlDbType.NVarChar);
                SqlParameter param4 = new SqlParameter("@email", SqlDbType.VarChar);
                SqlParameter param5 = new SqlParameter("@sdt", SqlDbType.VarChar);
                SqlParameter param6 = new SqlParameter("@diachi", SqlDbType.NVarChar);
                SqlParameter param7 = new SqlParameter("@bienso", SqlDbType.VarChar);
                SqlParameter param8 = new SqlParameter("@tknh", SqlDbType.VarChar);
                SqlParameter param9 = new SqlParameter("@cmnd", SqlDbType.VarChar);
                SqlParameter param10 = new SqlParameter("@khuvuc", SqlDbType.NVarChar);
                SqlParameter param11 = new SqlParameter("@returnvalue", SqlDbType.Int);

                param.Value =  UserName.Text;
                param2.Value = Password.Text;
                param3.Value = Name.Text;
                param4.Value = Email.Text;
                param5.Value = Phone.Text;
                param6.Value = Address.Text;
                param7.Value = VRP.Text;
                param8.Value = BankAccount.Text;
                param9.Value = CitizenId.Text;
                param10.Value = Area.Text;

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = MainWindow.sqlCon;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = storedProc;

                sqlCmd.Parameters.Add(param);
                sqlCmd.Parameters.Add(param2);
                sqlCmd.Parameters.Add(param3);
                sqlCmd.Parameters.Add(param4);
                sqlCmd.Parameters.Add(param5);
                sqlCmd.Parameters.Add(param6);
                sqlCmd.Parameters.Add(param7);
                sqlCmd.Parameters.Add(param8);
                sqlCmd.Parameters.Add(param9);
                sqlCmd.Parameters.Add(param10);
                sqlCmd.Parameters.Add(param11);

                sqlCmd.Parameters["@returnvalue"].Direction = ParameterDirection.ReturnValue;

                //connection to database
                SqlDataReader reader = null;
                //execute query
                reader = sqlCmd.ExecuteReader();
                int checkError = (int)sqlCmd.Parameters["@returnvalue"].Value;
                if (checkError == -1)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại");
                }
                else
                {
                    MessageBox.Show("Đăng kí thành công");
                    UserName.Text = "";
                    Password.Text = "";
                    Name.Text = "";
                    Email.Text = "";
                    Phone.Text = "";
                    Address.Text = "";
                    VRP.Text = "";
                    BankAccount.Text = "";
                    CitizenId.Text = "";
                    Area.Text = "";
                }
                reader.Close();
            }
        }
    }
}
