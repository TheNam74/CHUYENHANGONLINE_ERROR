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

namespace CHUYENHANGONLINE.Staff
{
    /// <summary>
    /// Interaction logic for StaffDetails.xaml
    /// </summary>
    public partial class StaffDetails : Window
    {
        public StaffDetails()
        {
            InitializeComponent();
        }

        private void StaffDetails_OnLoaded(object sender, RoutedEventArgs e)
        {
            Staff staff = MainWindow.User as Staff;
            NameTextBlock.Text = staff.Name;
            EmailTextBlock.Text = staff.Email;
            TelTextBlock.Text = staff.Tel;

        }
    }
}
