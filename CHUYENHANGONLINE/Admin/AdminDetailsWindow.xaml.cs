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

namespace CHUYENHANGONLINE.Admin
{
    /// <summary>
    /// Interaction logic for AdminDetailsWindow.xaml
    /// </summary>
    public partial class AdminDetailsWindow : Window
    {
        public AdminDetailsWindow()
        {
            InitializeComponent();
        }

        private void AdminDetailsWindow_OnLoadedDetails_OnLoaded(object sender, RoutedEventArgs e)
        {
            Staff.Staff admin = MainWindow.User as Staff.Staff;//admin cũng là staff
            NameTextBlock.Text = admin.Name;
            EmailTextBlock.Text = admin.Email;
            TelTextBlock.Text = admin.Tel;
        }
    }
}
