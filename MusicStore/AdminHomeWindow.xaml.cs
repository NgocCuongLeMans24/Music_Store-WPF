using MusicStore.BLL.Services;
using MusicStore.DAL.Models;
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
    /// Interaction logic for AdminHomeWindow.xaml
    /// </summary>
    public partial class AdminHomeWindow : Window
    {
        private AlbumService _albumService = new();
        public User currentAccount { get; set; }
        public AdminHomeWindow()
        {
            InitializeComponent();
        }

        
        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            AdminMainWindow a = new();
            a.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        public void FillDataGrid()
        {
            AlbumDataGrid.ItemsSource = null;
            AlbumDataGrid.ItemsSource = _albumService.GetAllAlbums();
        }

        private void CreateAlbumButton_Click(object sender, RoutedEventArgs e)
        {
            AdminManageAlbumWindow add = new();
            add.ShowDialog();
            FillDataGrid();
        }

        private void UpdateAlbumButton_Click(object sender, RoutedEventArgs e)
        {

            Album? selected = AlbumDataGrid.SelectedItem as Album;
            if (selected == null)
            {
                MessageBox.Show("Please select a row/ an album before editing", "Select a row", 
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            AdminManageAlbumWindow update = new();
            update.EditedAlbum = selected;
            update.ShowDialog();
            FillDataGrid();
        }

        private void DeleteAlbumButton_Click(object sender, RoutedEventArgs e)
        {
            Album? selected = AlbumDataGrid.SelectedItem as Album;
            if (selected == null)
            {
                MessageBox.Show("Please select a row/ an album before deleting", "Select a row",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            MessageBoxResult answer = MessageBox.Show("Do you really want to delete", "Confirm?",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.No) return;
            _albumService.DeleteAlbum(selected);
            FillDataGrid();
        }
    }
}
