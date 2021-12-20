using System.Windows;

namespace CHUYENHANGONLINE.Customer
{
    /// <summary>
    /// Interaction logic for OrderDetailsWindow.xaml
    /// </summary>
    public partial class OrderDetailsWindow : Window
    {
        private Order order;
        public OrderDetailsWindow(Order inputOrder)
        {
            order = inputOrder;
            InitializeComponent();
        }

        private void OrderDetailsWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = order;
        }
    }
}
