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

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            Database D = new Database();
            var result = D.CheckPassword(txtUsername.Text.ToString(), txtPassword.Password.ToString());
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
