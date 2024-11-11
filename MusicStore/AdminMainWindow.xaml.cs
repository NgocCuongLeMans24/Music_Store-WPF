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

namespace MusicStore
{
    /// <summary>
    /// Interaction logic for AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        public AdminMainWindow()
        {
            InitializeComponent();
        }

        private void ManageAlbumButton_Click(object sender, RoutedEventArgs e)
        {
            AdminHomeWindow a = new();
            a.Show();
            this.Close();
        }

        private void ManageUserButton_Click(object sender, RoutedEventArgs e)
        {
            AdminUserWindow a = new();
            a.Show();
            this.Close();
        }

        private void RevenueReportButton_Click(object sender, RoutedEventArgs e)
        {
            RevenueReportWindow r = new();
            r.Show();
            this.Close();
        }
    }
}
