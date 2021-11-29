using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
                //Database D = new Database();
                
                if (Database.CheckPassword(txtUsername.Text.ToString(), txtPassword.Password.ToString()))
                {
                    //doorverwijzig naar het homescherm
                    var appMainWindow = new MainWindow();
                    appMainWindow.Show();
                    //MessageBox.Show($"Inloggegevens juist ;)");
                    Close();

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
    }
}
