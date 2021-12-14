using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MovieMatcher
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void OnRegisterClick(object sender, RoutedEventArgs e)
        {
            var registrationIsValid = true;
            if (string.IsNullOrEmpty(Username.Text))
            {
                BorderUsername.BorderBrush = Brushes.Red;
                registrationIsValid = false;
            }

            if (string.IsNullOrEmpty(Password.Password) || !Password.Password.IsValidPassword())
            {
                BorderPassword.BorderBrush = Brushes.Red;
                registrationIsValid = false;
            }

            if (string.IsNullOrEmpty(Email.Text) || !Email.Text.IsValidEmailAddress())
            {
                BorderEmail.BorderBrush = Brushes.Red;
                registrationIsValid = false;
            }

            if (string.IsNullOrEmpty(DateOfBirth.Text) || !DateOfBirth.Text.IsValidDate())
            {
                BorderDateOfBirth.BorderBrush = Brushes.Red;
                registrationIsValid = false;
            }

            if (registrationIsValid)
            {
                var responseMessage = Database.CreateUser(
                    Username.Text,
                    PasswordHasher.Hash(Password.Password),
                    Email.Text,
                    DateOfBirth.Text
                );
                MessageBox.Show(responseMessage);

                if (responseMessage.Equals("Your account has been registered!"))
                {
                    var appLogin = new Login();
                    appLogin.Show();
                    Close();
                }
            }
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            var appLogin = new Login();
            appLogin.Show();
            Close();
        }

        private void UsernameChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            if (BorderUsername.BorderBrush == Brushes.Red) BorderUsername.BorderBrush = Brushes.Black;
        }

        private void PasswordChangedEventHandler(object sender, RoutedEventArgs args)
        {
            if (Password.Password.IsValidPassword()) BorderPassword.BorderBrush = Brushes.Black;
        }

        private void EmailChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            if (Email.Text.IsValidEmailAddress()) BorderEmail.BorderBrush = Brushes.Black;
        }

        private void DateOfBirthChangedEventHandler(object sender, RoutedEventArgs args)
        {
            if (DateOfBirth.Text.IsValidDate()) BorderDateOfBirth.BorderBrush = Brushes.Black;
        }
    }
}