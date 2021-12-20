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
using Microsoft.Win32;

namespace CHUYENHANGONLINE
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var userName = UsernameTextBox.Text;
            var pass = PasswordBox.Password; 
            var query = $"select * from taikhoan t where t.TENDANGNHAP='{userName}' and t.MATKHAU = '{pass}' and tinhtrang = 'true'";
            int loginId =-1;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.CommandText = query;
            sqlCmd.Connection = MainWindow.sqlCon;
            SqlDataReader reader = sqlCmd.ExecuteReader();
            if (reader.Read())
            {
                MainWindow.Actor = reader.GetString(4);
                loginId = reader.GetInt32(0);
                
            }
            reader.Close();

            if (loginId != -1) //đăng nhập thành công
            {

                //string userQuery;
                //SqlCommand userSqlCmd = new SqlCommand();
                //userSqlCmd.CommandType = CommandType.Text;
                switch (MainWindow.Actor)
                {
                    case "staff":
                        query = $"select * from nhanvien n where n.mataikhoan={loginId}";
                        sqlCmd.CommandText = query;
                        sqlCmd.Connection = MainWindow.sqlCon;
                        reader = sqlCmd.ExecuteReader();
                        if (reader.Read())
                        {
                            MainWindow.User = new Staff.Staff
                            {

                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Email = reader.GetString(2),
                                Tel = reader.GetString(3),
                                LoginId = reader.GetInt32(4),
                            };

                        }
                        reader.Close();
                        break;
                        case "admin":
                        query = $"select * from nhanvien n where n.mataikhoan={loginId}";//admin la nhan vien
                        sqlCmd.CommandText = query;
                        sqlCmd.Connection = MainWindow.sqlCon;
                        reader = sqlCmd.ExecuteReader();
                        if (reader.Read())
                        {
                            MainWindow.User = new Staff.Staff
                            {

                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Email = reader.GetString(2),
                                Tel = reader.GetString(3),
                                LoginId = reader.GetInt32(4),
                            };

                        }
                        reader.Close();
                        break;
                        case "customer":
                        query = $"select * from khachhang n where n.mataikhoan={loginId}";
                        sqlCmd.CommandText = query;
                        sqlCmd.Connection = MainWindow.sqlCon;
                        reader = sqlCmd.ExecuteReader();
                        if (reader.Read())
                        {
                            MainWindow.User = new Customer.Customer()
                            {

                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Email = reader.GetString(2),
                                Tel = reader.GetString(3),
                                Address = reader.GetString(4),
                                LoginId = reader.GetInt32(5),
                            };

                        }
                        reader.Close();
                        break;
                        case "provider":
                        query = $"select * from doitac n where n.mataikhoan={loginId}";
                        sqlCmd.CommandText = query;
                        sqlCmd.Connection = MainWindow.sqlCon;
                        reader = sqlCmd.ExecuteReader();
                        if (reader.Read())
                        {
                            MainWindow.User = new Provider.Provider()
                            {
                                Id = reader.GetInt32(0),
                                TaxCode = reader.GetString(1),
                                Address = reader.GetString(2),
                                Name = reader.GetString(3),
                                Tel = reader.GetString(4),
                                Represent = reader.GetString(5),
                                City = reader.GetString(6),
                                District = reader.GetString(7),
                                Email = reader.GetString(8),
                                ContractDate = reader.SafeGetDate(9),
                                OrderAmount = reader.GetInt32(10),
                                ProductType = reader.GetString(11),
                                BranchAmount = reader.GetInt32(12),
                                LoginId = reader.GetInt32(13),
                                Commission =   reader.GetFloat(14),
                            };

                        }
                        reader.Close();
                        break;
                        case "deliver":
                        query = $"select * from taixe n where n.mataikhoan={loginId}";
                        sqlCmd.CommandText = query;
                        sqlCmd.Connection = MainWindow.sqlCon;
                        reader = sqlCmd.ExecuteReader();
                        if (reader.Read())
                        {
                            MainWindow.User = new Shipper.Shipper()
                            {

                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Email = reader.GetString(2),
                                Tel = reader.GetString(3),
                                Address = reader.GetString(4),
                                Plate = reader.GetString(5),
                                BankAccount = reader.GetString(6),
                                Area = reader.GetString(7),
                                CitizenId = reader.GetString(8),
                                LoginId = reader.GetInt32(9),
                            };

                        }
                        reader.Close();
                        break;
                    

                }


                this.DialogResult = true;
                var test = MainWindow.User;
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu");
                
            }
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string actor = (string) (ActorComboBox.SelectedItem as ComboBoxItem)?.Content;
            UserControl RegisterUC;
            switch (actor)
            {
                case "Khách hàng":
                    RegisterUC = new Customer.CustomerRegisterUC();
                    RegisterContentControl.Content= RegisterUC;
                    this.Height = 300 + RegisterUC.Height;
                    break;
                case "Tài xế":
                    RegisterUC = new Shipper.ShipperRegisterUC();
                    RegisterContentControl.Content = RegisterUC;
                    this.Height = 300 + RegisterUC.Height;
                    break;
                case "Đối tác":
                    RegisterUC = new Provider.ProviderRegisterUC();
                    RegisterContentControl.Content = RegisterUC;
                    this.Height = 300 + RegisterUC.Height;
                    break;

            }


        }
    }
}
