using MusicStore.BLL.Services;
using MusicStore.DAL.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicStore
{
    public partial class MainWindow : Window
    {
        private AlbumService _albumService = new();
        private GenreService _genreService = new();
        private ArtistService _artistService = new();
        private OrderService _orderService = new();
        private OrderDetailService _orderDetailService = new();
        public User currentAccount { get; set; }
    

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
            FillComboBox();
        }

        private void FillDataGrid()
        {
            AlbumDataGrid.ItemsSource = null;
            AlbumDataGrid.ItemsSource = _albumService.GetAllAlbums();
        }

       
        //Page redirect
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new();
            main.Show();
            this.Close();
        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            CartWindow cart = new();
            cart.Show();
            this.Close();
        }

        //Page redirect


        private void SearchTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePlaceholderVisibility();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePlaceholderVisibility();
        }

        private void UpdatePlaceholderVisibility()
        {
            if (string.IsNullOrEmpty(SearchTextBox.Text))
            {
                PlaceholderText.Visibility = Visibility.Visible;
            }
            else
            {
                PlaceholderText.Visibility = Visibility.Collapsed;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchTextBox.Text)) FillDataGrid();
            AlbumDataGrid.ItemsSource = _albumService.SeacrhAlbumByTitle(SearchTextBox.Text);
        }

       private void FillComboBox()
        {
            GenreComboBox.ItemsSource = _genreService.GetAllGenres();
            GenreComboBox.DisplayMemberPath = "Name";
            GenreComboBox.SelectedValuePath = "GenreId";
            ArtistComboBox.ItemsSource = _artistService.GetAllArtists();
            ArtistComboBox.DisplayMemberPath = "Name";
            ArtistComboBox.SelectedValuePath = "ArtistId";
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = string.Empty;
            FillDataGrid();
        }

        private void GenreComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GenreComboBox.SelectedItem is Genre selectedGenre) 
            {
                ArtistComboBox.SelectedIndex = -1;

                AlbumDataGrid.ItemsSource = _albumService.GetAlbumsByGenre(selectedGenre.GenreId);
            }
            else
            {
                FillDataGrid();
            }
        }

        private void ArtistComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ArtistComboBox.SelectedItem is Artist selectedArtist)
            {
                GenreComboBox.SelectedIndex = -1;

                AlbumDataGrid.ItemsSource = _albumService.GetAlbumsByArtist(selectedArtist.ArtistId);
            }
            else
            {
                FillDataGrid();
            }
        }
        //Add to cart
        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            var loggedInUser = Application.Current.Properties["LoggedInUser"] as User;
            if (loggedInUser == null)
            {
                MessageBox.Show("No user is currently logged in.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Album selectedAlbum = (Album)((Button)sender).DataContext;
            var userId = loggedInUser.Userid;

            var existingOrder = _orderService.FindPendingOrder(userId) ?? new Order
            {
                Userid = userId,
                Status = "pending",
                Total = 0,
                Address = loggedInUser.Address
            };

            if (existingOrder.OrderId == 0)
            {
                _orderService.AddNewOrder(existingOrder);
            }

            var existingOrderDetail = _orderDetailService.GetOrderDetailByAlbum(selectedAlbum,existingOrder);
            if (existingOrderDetail != null)
            {
                existingOrderDetail.Quantity += 1;
                _orderDetailService.UpdateOrderDetail(existingOrderDetail);
            }
            else
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = existingOrder.OrderId,
                    AlbumId = selectedAlbum.AlbumId,
                    Quantity = 1,
                    Price = selectedAlbum.Price
                };
                _orderDetailService.AddOrderDetail(orderDetail);
                existingOrder.OrderDetails.Add(orderDetail);
            }

            existingOrder.Total += selectedAlbum.Price;
            _orderService.UpdateOrder(existingOrder);
            CartWindow c = new();
            c.Show();
            this.Close();
        }

        private void ViewOrderButton_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow o = new();
            o.Show();
            this.Close();
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            AccountWindow a = new();
            a.Show();
            this.Close();
        }
    }
}
