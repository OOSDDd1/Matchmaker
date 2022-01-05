using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Models.Database;
using Services;
using Stores;

namespace MovieMatcher
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void UsernameChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            if (borderUsername.BorderBrush == Brushes.Red)
            {
                borderUsername.BorderBrush = Brushes.Black;
            }
        }

        private void PasswordChangedEventHandler(object sender, RoutedEventArgs e)
        {
            if (borderPassword.BorderBrush == Brushes.Red)
            {
                borderPassword.BorderBrush = Brushes.Black;
            }
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtUsername.Text))
            {
                borderUsername.BorderBrush = Brushes.Red;
            }

            if (String.IsNullOrEmpty(txtPassword.Password))
            {
                borderPassword.BorderBrush = Brushes.Red;
            }
            else
            {
                if (DatabaseService.GetUserInfo(txtUsername.Text.ToString()))
                {
                    if (UserStore.password != null && PasswordService.Verify(txtPassword.Password.ToString(), UserStore.password))
                    {
                        // Redirect to HomeScreen
                        var mainWindow = new MainWindow();
                        mainWindow.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show($"Wachtwoord onjuist");
                    }
                }
                else
                {
                    MessageBox.Show($"Inloggegevens onjuist");
                }
            }
        }

        /**
         * Redirect to register screen
         */
        private void Button_Register_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new Register();
            registerWindow.Show();
            Close();
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}