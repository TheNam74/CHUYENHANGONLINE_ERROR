using System;
using System.Collections.Generic;
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
    /// Interaction logic for OrderDetailsWindow.xaml
    /// </summary>
    public partial class OrderDetailsWindow : Window
    {
        private OrderDetail orderDetail;
        public OrderDetailsWindow(OrderDetail inputorderDetail)
        {
            orderDetail = inputorderDetail;
            InitializeComponent();
        }

        private void OrderDetailsWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = orderDetail;
        }
    }
}
