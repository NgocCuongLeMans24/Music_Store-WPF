using System.Windows;
using MusicStore.DAL.Models;

namespace MusicStore
{
    public partial class AccountWindow : Window
    {
        public User currentUser { get; set; }

        public AccountWindow()
        {
            InitializeComponent();
            LoadUserData();
            FillElements(); // Populate the textboxes with currentUser data
        }

        private void LoadUserData()
        {
            // Assuming the logged-in user is stored in Application properties
            var loggedInUser = Application.Current.Properties["LoggedInUser"] as User;

            if (loggedInUser != null)
            {
                currentUser = loggedInUser;
            }
            else
            {
                MessageBox.Show("No user is currently logged in.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FillElements()
        {
            if (currentUser != null)
            {
                NameTextBox.Text = currentUser.Name;
                UsernameTextBox.Text = currentUser.Username;
                EmailTextBox.Text = currentUser.Email;
                PhoneNumberTextBox.Text = currentUser.PhoneNumber;
                AddressTextBox.Text = currentUser.Address;
                TotalAmountTextBox.Text = currentUser.TotalAmount.ToString("C") + "$"; // Assuming TotalAmount is a decimal
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Log out the user (this will vary depending on your logout implementation)
            Application.Current.Properties["LoggedInUser"] = null;

            // Optionally redirect to login screen or close the window
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void BackToHomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new();
            m.Show();
            this.Close();
        }
    }
}
