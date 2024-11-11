using MusicStore.BLL.Services;
using MusicStore.DAL.Models;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace MusicStore
{
    public partial class AdminManageUserWindow : Window
    {
        private UserService _userService = new();
        private RoleService _roleService = new();
        public User EditedUser { get; set; } = null;

        public AdminManageUserWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillRoleComboBox();
            FillElements(EditedUser);
            ManageWindowModeLabel.Content = EditedUser == null ? "Add New User" : "Update User";
        }

        private void FillElements(User user)
        {
            if (user == null) return;
            NameTextBox.Text = user.Name;
            UsernameTextBox.Text = user.Username;
            PasswordBox.Password = user.Password;
            EmailTextBox.Text = user.Email;
            PhoneNumberTextBox.Text = user.PhoneNumber;
            AddressTextBox.Text = user.Address;
            RoleComboBox.SelectedValue = user.RoleId;
            StatusComboBox.SelectedItem = user.Status == "Active" ? "Active" : "Inactive";
            TotalAmountTextBox.Text = user.TotalAmount.ToString("F2");
        }

        private void FillRoleComboBox()
        {
            RoleComboBox.ItemsSource = _roleService.GetAllRoles();
            RoleComboBox.DisplayMemberPath = "Name";
            RoleComboBox.SelectedValuePath = "RoleId";
        }

        private void SaveUserButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                // Validate the input fields
                if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(UsernameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(PasswordBox.Password) ||
                    string.IsNullOrWhiteSpace(EmailTextBox.Text) ||
                    RoleComboBox.SelectedValue == null ||
                    StatusComboBox.SelectedItem == null ||
                    !decimal.TryParse(TotalAmountTextBox.Text, out decimal totalAmount))
                {
                    MessageBox.Show("Please fill in all fields correctly.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (decimal.Parse(TotalAmountTextBox.Text) <0) {
                    MessageBox.Show("Total amount can't be negative", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!Regex.IsMatch(EmailTextBox.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                {
                    MessageBox.Show("Invalid email format.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!Regex.IsMatch(PhoneNumberTextBox.Text, @"^0\d{9}$"))
                {
                    MessageBox.Show("Phone number must start with '0' and contain exactly 10 digits.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                User user = new User
                {
                    Name = NameTextBox.Text,
                    Username = UsernameTextBox.Text,
                    Password = PasswordBox.Password,
                    Email = EmailTextBox.Text,
                    PhoneNumber = PhoneNumberTextBox.Text,
                    Address = AddressTextBox.Text,
                    CreatedAt = DateTime.Now,
                    RoleId = (int)RoleComboBox.SelectedValue,
                    Status = ((ComboBoxItem)StatusComboBox.SelectedItem).Content.ToString(),
                    TotalAmount = totalAmount
                };

                if (EditedUser == null)
                {
                    var usernameCheck = _userService.CheckUserame(UsernameTextBox.Text);
                    if(usernameCheck != null)
                    {
                        MessageBox.Show("Duplicated Username !", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    _userService.AddUser(user);
                    MessageBox.Show("User added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                }
                else
                {
                    user.Userid = EditedUser.Userid;
                    _userService.UpdateUser(user);
                    MessageBox.Show("User updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void BackToHomeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
