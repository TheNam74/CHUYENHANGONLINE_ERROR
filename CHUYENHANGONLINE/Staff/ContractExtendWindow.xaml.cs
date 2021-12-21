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

namespace CHUYENHANGONLINE.Staff
{
    /// <summary>
    /// Interaction logic for ContractExtendWindow.xaml
    /// </summary>
    public partial class ContractExtendWindow : Window
    {
        private Provider.Provider _provider;
        public ContractExtendWindow(Provider.Provider provider)
        {
            this._provider = provider;
            InitializeComponent();
        }
        private void ContractExtendWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            ProviderNameLabel.Content = _provider.Name;
            DatePicker.SelectedDate = _provider.ContractDate;
            TaxCodeTextBlock.Text = _provider.TaxCode;
            ProductTypeTextBlock.Text = _provider.ProductType;
            EmailTextBlock.Text = _provider.Email;
            TelTextBlock.Text = _provider.Tel;
            CommissionTextBlock.Text = _provider.Commission.ToString();
            BranchAmountTextBlock.Text = _provider.BranchAmount.ToString();
            OrderAmountTextBlock.Text = _provider.OrderAmount.ToString();


        }
        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_provider.ContractDate != DatePicker.SelectedDate)
            {
                _provider.ContractDate = DatePicker.SelectedDate;

                using (SqlCommand cmd = new SqlCommand("USP_CAU4_4a", MainWindow.sqlCon))//gây ra error04 dirty read
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@maDoiTac", SqlDbType.VarChar).Value = _provider.Id;
                    cmd.Parameters.Add("@ngayGiaHan", SqlDbType.VarChar).Value = _provider.ContractDate;
                    cmd.ExecuteNonQuery();
                }
            }
            Close();
        }
    }
}
