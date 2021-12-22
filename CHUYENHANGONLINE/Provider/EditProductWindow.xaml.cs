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
    /// Interaction logic for EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {
        BindingList<Product> _productList = new BindingList<Product>();
        private Product _product;
        int SelectedIndex;
        public EditProductWindow(Product inputProduct)
        {
            _product = inputProduct;

            InitializeComponent();
        }
        private void EditProductWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            NewProAmount.Text = _product.ProAmount.ToString();
            NewProName.Text = _product.ProName;
            NewProInfo.Text = _product.ProInfo;
            NewProPrice.Text = _product.ProPrice.ToString();
            NewProUnit.Text = _product.ProUnit;
            BranchId.Text =_product.BranchID.ToString();
            ProId.Text =_product.ProID.ToString();
            ProviderId.Text =_product.ProviderID.ToString();
            this.DataContext = _product;
        }
        private void ApplyChange_Click(object sender, RoutedEventArgs e)
        {
            //Check thông tin
            if (string.IsNullOrWhiteSpace(NewProName.Text)
                || string.IsNullOrWhiteSpace(NewProInfo.Text)
                || string.IsNullOrWhiteSpace(NewProUnit.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            string TENSANPHAM = NewProName.Text;
            string THONGTIN = NewProInfo.Text;
            string DONVITINH = NewProUnit.Text;
            float DONGIA;
            int SOLUONG;

            if (!float.TryParse(NewProPrice.Text, out DONGIA))
            {
                MessageBox.Show($"Đơn giá phải là số");
                return;
            }
            if (!int.TryParse(NewProAmount.Text, out SOLUONG))
            {
                MessageBox.Show($"Số lượng phải là số");
                return;
            }
            //create query for stored procedure
            SqlCommand sqlCmd = new SqlCommand($"USP_CAU1_4a", MainWindow.sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            //create parameter 
            sqlCmd.Parameters.Add(new SqlParameter("@MADT", _product.ProviderID));
            sqlCmd.Parameters.Add(new SqlParameter("@MACN", _product.BranchID));
            sqlCmd.Parameters.Add(new SqlParameter("@MASP", _product.ProID));
            sqlCmd.Parameters.Add(new SqlParameter("@TENSANPHAM", TENSANPHAM));
            sqlCmd.Parameters.Add(new SqlParameter("@THONGTIN", THONGTIN));
            sqlCmd.Parameters.Add(new SqlParameter("@DONGIA", DONGIA));
            sqlCmd.Parameters.Add(new SqlParameter("@DONVITINH", DONVITINH));
            sqlCmd.Parameters.Add(new SqlParameter("SOLUONG", SOLUONG));

            //execute query
            int ret = sqlCmd.ExecuteNonQuery();

            MessageBox.Show(ret != -1 ? $"Cập nhật thành công({ret})" : "Cập nhật thất bại");
            //Cập nhật lại địa chỉ mới trên listview
            _product.ProName = NewProName.Text;
            _product.ProInfo = NewProInfo.Text;
            _product.ProPrice = (float)Convert.ToDouble(NewProPrice.Text);
            _product.ProUnit = NewProUnit.Text;
            _product.ProAmount = Convert.ToInt32(NewProAmount.Text);

        }

    }
}
