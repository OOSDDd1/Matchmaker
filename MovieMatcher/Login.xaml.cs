using System;
using System.Collections.Generic;
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
                
            } else
            {
                Database D = new Database();
                
                if (D.CheckPassword(txtUsername.Text.ToString(), txtPassword.Password.ToString()))
                {
                    //naar home scherm
                    MessageBox.Show($"Inloggegevens juist ;)");
                }
                else
                    MessageBox.Show($"Inloggegevens onjuist");
            }

        }

        private void Button_Register_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
