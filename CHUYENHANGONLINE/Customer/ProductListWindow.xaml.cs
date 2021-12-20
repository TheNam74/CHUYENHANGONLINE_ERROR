using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CHUYENHANGONLINE.Customer
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private BindingList<Product> productList;
        public ProductListWindow(BindingList<Product> productList)
        {
            this.productList = productList;
            InitializeComponent();
        }

        private void ProductListWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            ProductList.ItemsSource = productList;
        }

        private void BtnProductDetailOnClick(object sender, RoutedEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            Product selectedProduct = item.DataContext as Product;
            var productDetailsWindow = new ProductDetailsWindow(selectedProduct);
            productDetailsWindow.ShowDialog();
        }

        private void AddToCart_OnClick(object sender, RoutedEventArgs e)
        {
            var rowItem = (sender as Button).DataContext as Product;
            var temp = new OrderDetail()
            {
                Amount = rowItem.BuyingAmount ?? default(int),
                BranchID = rowItem.BranchID,
                OrdID = -1,
                ProviderID = rowItem.ProviderID,
                ProID = rowItem.ProID,
                OrderDetailBill = rowItem.ProPrice * (rowItem.BuyingAmount ?? default(int))
            };
            CustomerHomePageUC.CartItem.Add(temp);
        }

        private void AmountOfOrder_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var rowItem = (sender as TextBox).DataContext as Product;
            rowItem.BuyingAmount = int.Parse((sender as TextBox).Text);
        }
    }
}