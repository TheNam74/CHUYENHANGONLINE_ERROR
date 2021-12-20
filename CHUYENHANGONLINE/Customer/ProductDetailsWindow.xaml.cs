using System.Windows;

namespace CHUYENHANGONLINE.Customer
{
    /// <summary>
    /// Interaction logic for ProductDetailsWindow.xaml
    /// </summary>
    public partial class ProductDetailsWindow : Window
    {
        private Product _productDetail;
        public ProductDetailsWindow(Product selectedProduct)
        {
            _productDetail = selectedProduct;
            InitializeComponent();
        }

        private void ProductDetailsWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _productDetail;
        }
    }
}
