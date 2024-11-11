using System.Collections.ObjectModel;
using System.Windows;
using MusicStore.BLL.Services;
using MusicStore.DAL.Models;

namespace MusicStore
{
    public partial class OrderWindow : Window
    {
        private readonly OrderService _orderService = new();
        public ObservableCollection<Order> UserOrders { get; set; } = new();

        public OrderWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUserOrders();
            OrderDataGrid.ItemsSource = UserOrders;
        }

        private void LoadUserOrders()
        {
            var loggedInUser = Application.Current.Properties["LoggedInUser"] as User;

            if (loggedInUser != null)
            {
                UserOrders.Clear();
                var orders = _orderService.GetAllOrdersByUserId(loggedInUser.Userid);

                foreach (var order in orders)
                {
                    UserOrders.Add(order);
                }
            }
            else
            {
                MessageBox.Show("No user is currently logged in.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackToHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            this.Close();
        }
    }
}
