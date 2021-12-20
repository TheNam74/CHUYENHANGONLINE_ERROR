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
    /// Interaction logic for ProviderDetailsWindow.xaml
    /// </summary>
    public partial class ProviderDetailsWindow : Window
    {
        private Provider _provider;
        public ProviderDetailsWindow()
        {
            InitializeComponent();
        }

        private void ProviderDetails_Loaded(object sender, RoutedEventArgs e)
        {
            _provider = MainWindow.User as Provider;
            this.DataContext = _provider;
        }
    }
}
