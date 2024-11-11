using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MusicStore.BLL.Services;
using MusicStore.DAL.Models;

namespace MusicStore
{
    public partial class CartWindow : Window
    {
        private readonly OrderDetailService _orderDetailService = new();
        private OrderService _orderService = new();
        private AlbumService _albumService = new();
        private UserService _userService = new();
        public User currentAccount { get; set; }
        public ObservableCollection<OrderDetail> CartItems { get; set; } = new();

        public CartWindow()
        {
            InitializeComponent();
           
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCartItems();
            CartDataGrid.ItemsSource = CartItems;
        }

        private void LoadCartItems()
        {
            var loggedInUser = Application.Current.Properties["LoggedInUser"] as User;

            if (loggedInUser != null)
            {
                CartItems.Clear();
                var pendingOrder = _orderService.FindPendingOrder(loggedInUser.Userid);

                if (pendingOrder != null)
                {
                    var orderDetails = _orderDetailService.GetAllDetailByPendingOrder(pendingOrder);
                    foreach (var item in orderDetails)
                    {
                        CartItems.Add(item);
                    }
                    CartDataGrid.ItemsSource = CartItems;
                    UpdateTotalAmount();
                }
                else
                {
                    CartDataGrid.ItemsSource = null;
                    TotalAmountTextBlock.Text = "$0.00";
                }
            }
            else
            {
                MessageBox.Show("No user is currently logged in.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void UpdateTotalAmount()
        {
            var total = CartItems.Sum(item => item.Quantity * item.Price);
            TotalAmountTextBlock.Text = total.ToString("C");
            var loggedInUser = Application.Current.Properties["LoggedInUser"] as User;
            var pendingOrder = _orderService.FindPendingOrder(loggedInUser.Userid);
            pendingOrder.Total = decimal.Parse(total.ToString());
            _orderService.UpdateOrder(pendingOrder);
        }

        private void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is OrderDetail orderDetail)
            {
                orderDetail.Quantity += 1;
                _orderDetailService.UpdateOrderDetail(orderDetail);
                UpdateTotalAmount();
                CartDataGrid.Items.Refresh();
            }
        }

        private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is OrderDetail orderDetail)
            {
                if (orderDetail.Quantity > 1)
                {
                    orderDetail.Quantity -= 1;
                    _orderDetailService.UpdateOrderDetail(orderDetail);
                }
                else
                {
                    // If quantity reaches 0, remove the item from the cart
                    CartItems.Remove(orderDetail);
                    _orderDetailService.DeleteOrderDetail(orderDetail);
                }

                UpdateTotalAmount();
                CartDataGrid.Items.Refresh();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new();
            m.Show();
            this.Close();
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            var loggedInUser = Application.Current.Properties["LoggedInUser"] as User;
            var pendingOrder = _orderService.FindPendingOrder(loggedInUser.Userid);

            if (loggedInUser == null || pendingOrder == null)
            {
                MessageBox.Show("No user is logged in or there is no pending order.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var orderTotalAmount = CartItems.Sum(item => item.Quantity * item.Price);

            if (loggedInUser.TotalAmount >= orderTotalAmount)
            {
                loggedInUser.TotalAmount -= orderTotalAmount;
                _userService.UpdateUser(loggedInUser);

                // Update each album's quantity and order detail status
                foreach (var orderDetail in CartItems)
                {
                    
                    var album = _albumService.GetAlbumById(orderDetail.AlbumId); 
                    if(album.Stock < orderDetail.Quantity)
                    {
                        MessageBox.Show("Not enough album in stock", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    album.Stock -= orderDetail.Quantity;  
                    _albumService.UpdateAlbum(album);  
                }
               
                pendingOrder.Status = "delivering";
                _orderService.UpdateOrder(pendingOrder);

                MessageBox.Show("Purchase successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                LoadCartItems();
            }
            else
            {
                MessageBox.Show("Insufficient balance to complete the purchase.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
