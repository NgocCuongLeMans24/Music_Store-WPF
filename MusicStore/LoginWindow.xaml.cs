using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.ApplicationServices;
using MusicStore.BLL.Services;
using MusicStore.DAL;
using MusicStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private UserService _service = new();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text.IsNullOrEmpty() || PasswordBox.Password.IsNullOrEmpty())
            {
                MessageBox.Show("Both username and password are required!", "Access denied", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // login authenticate success -> call Main
            DAL.Models.User? user = _service.Authenticate(UsernameTextBox.Text, PasswordBox.Password);
            // account co the null hoac ko null(thanh cong)
            if (user == null)
            {
                MessageBox.Show("Invalid username or password.", "Wrong Credentials", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } // TODO: phai thong bao sai cai gi: sai username hoac sai pass
            // account luc nay la 1 record nao do thuoc role 1,2,3 => check phan quyen
            Application.Current.Properties["LoggedInUser"] = user;
            if (user.RoleId == 1 )
            {
                AdminHomeWindow ah = new();
                ah.currentAccount = user;
                AdminMainWindow am = new();
                am.Show();
            }
            else if (user.RoleId == 2)
            {
                MainWindow m = new();
                CartWindow c = new();
                m.currentAccount = user; // day account login thanh cong sang ben main
                c.currentAccount = user;                         // 2 tro vao 1 
                m.Show();
            }

            // role con lai di sang main

            this.Hide(); // an man hinh login di


        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UsernameTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateUsernamePlaceholderVisibility();
        }

        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateUsernamePlaceholderVisibility();
        }

        private void PasswordBox_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePasswordPlaceholderVisibility();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            UpdatePasswordPlaceholderVisibility();
        }

        private void UpdateUsernamePlaceholderVisibility()
        {
            if (string.IsNullOrEmpty(UsernameTextBox.Text))
            {
                UsernamePlaceholder.Visibility = Visibility.Visible;
            }
            else
            {
                UsernamePlaceholder.Visibility = Visibility.Collapsed;
            }
        }

        private void UpdatePasswordPlaceholderVisibility()
        {
           
            if (string.IsNullOrEmpty(PasswordBox.Password))
            {
                PasswordPlaceholder.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordPlaceholder.Visibility = Visibility.Collapsed;
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow r = new();
            r.Show();
            this.Close();
        }
    }
}