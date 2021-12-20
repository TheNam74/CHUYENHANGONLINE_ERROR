using System.Windows;

namespace CHUYENHANGONLINE.Customer
{
    /// <summary>
    /// Interaction logic for CustomerDetail.xaml
    /// </summary>
    public partial class CustomerDetailsWindow : Window
    {
        public CustomerDetailsWindow()
        {
            InitializeComponent();
        }

        private void CustomerDetails_OnLoadedDetails_OnLoaded(object sender, RoutedEventArgs e)
        {
            Customer customer = MainWindow.User as Customer;
            NameTextBlock.Text = customer.Name;
            EmailTextBlock.Text = customer.Email;
            TelTextBlock.Text = customer.Tel;
            AddressTextBlock.Text = customer.Address;
        }
    }
}
