using System.Windows;
namespace CHUYENHANGONLINE.Customer
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        public CartWindow()
        {
            InitializeComponent();
        }

        private void CartWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            CartView.Items.Clear();
            CartView.ItemsSource = CustomerHomePageUC.CartItem;
        }
        private void BtnDeleteOrderDetailOnClick(object sender, RoutedEventArgs e)
        {
            int index = CartView.SelectedIndex;
            CustomerHomePageUC.CartItem.RemoveAt(index);
        }

        private void CheckOutView_OnClick(object sender, RoutedEventArgs e)
        {
            var checkOutView = new CheckOutWindow();
            checkOutView.ShowDialog();
        }
    }
}
