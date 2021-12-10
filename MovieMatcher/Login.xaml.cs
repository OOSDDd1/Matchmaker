using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using MovieMatcher.Models.Database;

namespace MovieMatcher
{
    public class LoginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
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
                if (Database.GetUserInfo(txtUsername.Text.ToString()))
                {
                    if (UserInfo.Password != null && PasswordHasher.Verify(txtPassword.Password.ToString(), UserInfo.Password))
                    {
                        //doorverwijzig naar het homescherm
                        var appMainWindow = new MainWindow();
                        appMainWindow.Show();
                        //MessageBox.Show($"Inloggegevens juist ;)");
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

        private void Button_Register_Click(object sender, RoutedEventArgs e)
        {
            //doorverwijzig naar het registeer scherm
            var appreg = new Register();
            appreg.Show();
            Close();
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
