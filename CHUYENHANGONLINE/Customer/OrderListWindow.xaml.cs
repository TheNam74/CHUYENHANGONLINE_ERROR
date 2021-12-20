using System.ComponentModel;
using System.Windows;

namespace CHUYENHANGONLINE.Customer
{
    /// <summary>
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        private BindingList<Order> _orderList;
        public OrderListWindow(BindingList<Order> orderList)
        {
            _orderList = orderList;
            InitializeComponent();
        }

        private void OrderListWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            OrderListView.Items.Clear();
            OrderListView.ItemsSource = _orderList;
        }

        private void BtnOrderDetailOnClick(object sender, RoutedEventArgs e)
        {
            int index = OrderListView.SelectedIndex;
            var orderDetails = new OrderDetailsWindow(_orderList[index]);
            orderDetails.Show();
        }
    }
}
