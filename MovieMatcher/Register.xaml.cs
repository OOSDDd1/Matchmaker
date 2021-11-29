using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            }
        }
        
        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
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
