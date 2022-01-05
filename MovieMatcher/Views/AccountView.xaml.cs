using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Services;
using Stores;
using MovieMatcher;

namespace MovieMatcher.Views
{
    /// <summary>
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountView : UserControl
    {
        public AccountView()
        {
            InitializeComponent();
            Username.Text = UserStore.username;
            Email.Text = UserStore.email;
            DateOfBirth.Text = Convert.ToString(UserStore.birthYear);
            CheckBoxAdult.IsChecked = UserStore.adult;
            CheckBoxProviders.IsChecked = UserStore.provider;
            if (UserStore.birthYear.Value.AddYears(18) > DateTime.Now)
            {
                CheckBoxAdult.IsEnabled = false;
            }
            else
            {
                CheckBoxAdult.IsEnabled = true;
            }
        }

        //grid 0 - Update General
        private void OnUpdateClick(object sender, RoutedEventArgs e)
        {
            var registrationIsValid = true;
            if (string.IsNullOrEmpty(Username.Text))
            {
                BorderUsername.BorderBrush = Brushes.Red;
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
                // Convert date of birth to database format
                DateTime convertedDate = Convert.ToDateTime(DateOfBirth.Text);
                var dateOfBirth = convertedDate.ToString("MM-dd-yyyy");

                var responseMessage = DatabaseService.UpdateUserGeneral(
                    Username.Text,
                    Email.Text,
                    dateOfBirth,
                    (int)UserStore.id
                );
                MessageBox.Show(responseMessage);

                if (responseMessage.Equals("Your account has been updated!"))
                {
                    DatabaseService.GetUserInfo(Username.Text.ToString());
                    MainWindow.Main.UserName.Content = UserStore.username;
                }
            }
        }

        private void UsernameChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            if (BorderUsername.BorderBrush == Brushes.Red) BorderUsername.BorderBrush = Brushes.Black;
        }

        private void EmailChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            if (Email.Text.IsValidEmailAddress()) BorderEmail.BorderBrush = Brushes.Black;
        }

        private void DateOfBirthChangedEventHandler(object sender, RoutedEventArgs args)
        {
            if (DateOfBirth.Text.IsValidDate()) BorderDateOfBirth.BorderBrush = Brushes.Black;
        }


        //grid 1 - Update Password
        private void OnUpdate1Click(object sender, RoutedEventArgs e)
        {
            var registrationIsValid = true;
            if (string.IsNullOrEmpty(OldPassword.Password) || !PasswordService.Verify(OldPassword.Password, UserStore.password))
            {
                BorderOldPassword.BorderBrush = Brushes.Red;
                registrationIsValid = false;
            }

            if (string.IsNullOrEmpty(NewPassword.Password) || !NewPassword.Password.IsValidPassword())
            {
                BorderNewPassword.BorderBrush = Brushes.Red;
                registrationIsValid = false;
            }

            if (string.IsNullOrEmpty(NewPassword1.Password) || NewPassword.Password != NewPassword1.Password)
            {
                BorderNewPassword.BorderBrush = Brushes.Pink;
                BorderNewPassword1.BorderBrush = Brushes.Pink;
                registrationIsValid = false;
            }

            if (registrationIsValid)
            {
                var responseMessage = DatabaseService.UpdateUserPassword(
                    PasswordService.Hash(NewPassword.Password),
                    (int)UserStore.id
                );
                MessageBox.Show(responseMessage);

                if (responseMessage.Equals("Your password has been updated!"))
                {
                    DatabaseService.GetUserInfo(UserStore.username);
                }
            }
        }

        private void OldPasswordChangedEventHandler(object sender, RoutedEventArgs args)
        {
            if (OldPassword.Password.IsValidPassword()) BorderOldPassword.BorderBrush = Brushes.Black;
        }

        private void NewPasswordChangedEventHandler(object sender, RoutedEventArgs args)
        {
            if (NewPassword.Password.IsValidPassword()) BorderNewPassword.BorderBrush = Brushes.Black;
        }

        private void NewPassword1ChangedEventHandler(object sender, RoutedEventArgs args)
        {
            if (NewPassword1.Password.IsValidPassword()) BorderNewPassword1.BorderBrush = Brushes.Black;
        }

        //grid 2 Update Filters
        private void OnUpdate2Click(object sender, RoutedEventArgs e)
        {
            var responseMessage = DatabaseService.UpdateUserFilters(
                (bool)CheckBoxAdult.IsChecked,
                (bool)CheckBoxProviders.IsChecked,
                (int)UserStore.id
            );
            MessageBox.Show(responseMessage);
            DatabaseService.GetUserInfo(UserStore.username);
        }

    }
}
    

