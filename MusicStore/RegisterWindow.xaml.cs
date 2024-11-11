using System;
using System.Text.RegularExpressions;
using System.Windows;
using MusicStore.BLL.Services;
using MusicStore.DAL.Models;

namespace MusicStore
{
    public partial class RegisterWindow : Window
    {
        private UserService _userService = new UserService();

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve values from text boxes
            string fullName = textBoxFullName.Text;
            string username = textBoxUsername.Text;
            string password = passwordBoxPassword.Password;
            string confirmPassword = passwordBoxConfirmPassword.Password;
            string email = textBoxEmail.Text;
            string phoneNumber = textBoxPhoneNumber.Text;
            string address = textBoxAddress.Text;

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phoneNumber))
            {
                errormessage.Text = "Please fill in all fields.";
                return;
            }

            // Password confirmation
            if (password != confirmPassword)
            {
                errormessage.Text = "Passwords do not match.";
                return;
            }
            var usernameCheck = _userService.CheckUserame(username);
            if (usernameCheck != null)
            {
                errormessage.Text = "Duplicate account !";
                return;
            }

                // Email validation
                if (!Regex.IsMatch(email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Invalid email format.";
                return;
            }

            // Phone number validation: starts with '0' and has exactly 10 digits
            if (!Regex.IsMatch(phoneNumber, @"^0\d{9}$"))
            {
                errormessage.Text = "Phone number must start with '0' and contain exactly 10 digits.";
                return;
            }

            User newUser = new User
            {
                Name = fullName,
                Username = username,
                Password = password,
                Email = email,
                PhoneNumber = phoneNumber,
                Address = address,
                RoleId = 2, 
                CreatedAt = DateTime.Now,
                Status = "active",
                TotalAmount = 0.00m 
            };

            try
            {
                _userService.AddUser(newUser);

                MessageBox.Show("Registration successful!");
                LoginWindow l = new();
                l.Show();
                Close();
            }
            catch (Exception ex)
            {
                errormessage.Text = $"Error: {ex.Message}";
            }
        }


        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            // Clear all input fields
            textBoxFullName.Text = "";
            textBoxUsername.Text = "";
            passwordBoxPassword.Password = "";
            passwordBoxConfirmPassword.Password = "";
            textBoxEmail.Text = "";
            textBoxPhoneNumber.Text = "";
            textBoxAddress.Text = "";
            errormessage.Text = "";
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }
    }
}
