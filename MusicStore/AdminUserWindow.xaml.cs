using MusicStore.BLL.Services;
using MusicStore.DAL.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MusicStore
{
    public partial class AdminUserWindow : Window
    {
        private UserService _userService = new();

        public AdminUserWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }

        public void FillDataGrid()
        {
            UserDataGrid.ItemsSource = null;
            UserDataGrid.ItemsSource = _userService.GetAllUsers();
        }

        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            AdminManageUserWindow addUserWindow = new();
            addUserWindow.ShowDialog();
            FillDataGrid();
        }

        private void UpdateUserButton_Click(object sender, RoutedEventArgs e)
        {
            User? selectedUser = UserDataGrid.SelectedItem as User;
            if (selectedUser == null)
            {
                MessageBox.Show("Please select a user before editing", "Select a user",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            AdminManageUserWindow updateUserWindow = new();
            updateUserWindow.EditedUser = selectedUser;
            updateUserWindow.ShowDialog();
            FillDataGrid();
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            User? selectedUser = UserDataGrid.SelectedItem as User;
            if (selectedUser == null)
            {
                MessageBox.Show("Please select a user before deleting", "Select a user",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            MessageBoxResult answer = MessageBox.Show("Do you really want to delete this user?", "Confirm?",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.No) return;

            _userService.DeleteUser(selectedUser);
            FillDataGrid();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            AdminMainWindow a = new();
            a.Show();
            this.Close();
        }
    }
}
