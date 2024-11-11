using MusicStore.BLL.Services;
using MusicStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for AdminAddAlbumWindow.xaml
    /// </summary>
    public partial class AdminManageAlbumWindow : Window
    {
        private AlbumService _albumService = new();
        private ArtistService _artistService = new();
        private GenreService _genreService = new();
        public Album EditedAlbum { get; set; } = null; 

        public AdminManageAlbumWindow()
        {
            InitializeComponent();
        }


        private void BackToHomeButton_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private void SaveAlbumButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TitleTextBox.Text) ||
                    string.IsNullOrWhiteSpace(PriceTextBox.Text) ||
                    string.IsNullOrWhiteSpace(StockTextBox.Text) ||
                    GenreComboBox.SelectedValue == null ||
                    ArtistComboBox.SelectedValue == null ||
                    string.IsNullOrWhiteSpace(AlbumUrlTextBox.Text))
                {
                    MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (int.Parse(StockTextBox.Text) < 0 || decimal.Parse(PriceTextBox.Text) <0) {
                    MessageBox.Show("Stock or price can't be negative", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Album album = new()
                {
                    Title = TitleTextBox.Text,
                    Price = decimal.Parse(PriceTextBox.Text),
                    GenreId = (int)GenreComboBox.SelectedValue,
                    ArtistId = (int)ArtistComboBox.SelectedValue,
                    Stock =  int.Parse(StockTextBox.Text),
                    AlbumUrl = AlbumUrlTextBox.Text,
                    IsTop10BestSeller = false
                };
                if (EditedAlbum == null)
                {
                    var titleCheck = _albumService.CheckTitle(TitleTextBox.Text);
                    if (titleCheck != null)
                    {
                        MessageBox.Show("Duplicated Title !", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    _albumService.AddNewAlbum(album);
                }
                else
                    album.AlbumId = EditedAlbum.AlbumId;
                    _albumService.UpdateAlbum(album);
                MessageBox.Show("Album saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid number for Price.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillComboBox();
            FillElements(EditedAlbum);
            if (EditedAlbum != null)
            {
                ManageWindowModeLabel.Content = "Update Album";
            }
            else
            {
                ManageWindowModeLabel.Content = "Add New Album";
            }
        }
        private void FillElements(Album a)
        {
            if (a == null) return;
            TitleTextBox.Text = a.Title;
            PriceTextBox.Text = a.Price.ToString();
            GenreComboBox.SelectedValue = a.GenreId;
            ArtistComboBox.SelectedValue = a.ArtistId;
            StockTextBox.Text = a.Stock.ToString();
            AlbumUrlTextBox.Text = a.AlbumUrl;

        }
        private void FillComboBox()
        {
            ArtistComboBox.ItemsSource = _artistService.GetAllArtists();
            ArtistComboBox.DisplayMemberPath = "Name";
            ArtistComboBox.SelectedValuePath = "ArtistId";
            GenreComboBox.ItemsSource = _genreService.GetAllGenres();
            GenreComboBox.DisplayMemberPath = "Name";
            GenreComboBox.SelectedValuePath = "GenreId";
        }
    }
}
