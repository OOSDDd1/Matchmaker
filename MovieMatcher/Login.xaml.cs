using System;
using System.Windows;

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

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            var result = Database.CheckPassword(txtUsername.Text.ToString(), txtPassword.Password.ToString());
            Label_Result.Content = result;
            if (String.IsNullOrEmpty(result))
            {
                MessageBox.Show($"FOUT1 {result}");
            }
            
            
        }

        private void Button_Register_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
