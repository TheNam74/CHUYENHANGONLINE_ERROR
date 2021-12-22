using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace CHUYENHANGONLINE.Customer
{
    /// <summary>
    /// Interaction logic for CustomerRegisterUC.xaml
    /// </summary>
    public partial class CustomerRegisterUC : UserControl
    {
        public CustomerRegisterUC()
        {
            InitializeComponent();
        }

        private void BtnRegister_OnClick(object sender, RoutedEventArgs e)
        {

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "USP_CAU2_2a";
            sqlCmd.Connection = MainWindow.sqlCon;
            SqlParameter userNameParameter = new SqlParameter("@TENDANGNHAP", SqlDbType.NVarChar);
            SqlParameter passwordParameter = new SqlParameter("@MATKHAU", SqlDbType.NVarChar);
            SqlParameter nameParameter = new SqlParameter("@HOTEN", SqlDbType.NVarChar);
            SqlParameter emailParameter = new SqlParameter("@EMAIL", SqlDbType.VarChar);
            SqlParameter phoneNumberParameter = new SqlParameter("@SDT", SqlDbType.VarChar);
            SqlParameter addressParameter = new SqlParameter("@DIACHI", SqlDbType.NVarChar);

            userNameParameter.Value = UserNameTextBox.Text;
            passwordParameter.Value = PasswordTextBox.Text;
            nameParameter.Value = NameTextBox.Text;
            emailParameter.Value = EmailTextBox.Text;
            phoneNumberParameter.Value = PhoneTextBox.Text;
            addressParameter.Value = AddressTextBox.Text;

            sqlCmd.Parameters.Add(userNameParameter);
            sqlCmd.Parameters.Add(passwordParameter);
            sqlCmd.Parameters.Add(nameParameter);
            sqlCmd.Parameters.Add(emailParameter);
            sqlCmd.Parameters.Add(phoneNumberParameter);
            sqlCmd.Parameters.Add(addressParameter);

            sqlCmd.ExecuteNonQuery();
            UserNameTextBox.Text = "";
            PasswordTextBox.Text = "";
            NameTextBox.Text = "";
            EmailTextBox.Text = "";
            PhoneTextBox.Text = "";
            AddressTextBox.Text = "";
        }
    }
}
