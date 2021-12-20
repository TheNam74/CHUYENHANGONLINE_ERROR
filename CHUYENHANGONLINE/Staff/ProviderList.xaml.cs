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

namespace CHUYENHANGONLINE.Staff
{
    /// <summary>
    /// Interaction logic for ProviderList.xaml
    /// </summary>
    public partial class ProviderList : Window
    {
        private BindingList<Provider.Provider> _providers;

        public ProviderList(BindingList<Provider.Provider> productList)
        {
            _providers = productList;
            InitializeComponent();
            
        }

        private void ProviderList_OnLoaded(object sender, RoutedEventArgs e)
        {
            ProviderListView.Items.Clear();
            ProviderListView.ItemsSource = _providers;
        }

        private void ContractExtendMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var provider = ProviderListView.SelectedItem as Provider.Provider;
            if (provider != null)
            {
                var contractExtendWindow = new ContractExtendWindow(provider);
                contractExtendWindow.Show();
            }
        }

    }
}
