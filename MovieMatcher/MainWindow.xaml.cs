using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;

namespace MovieMatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static IConfigurationRoot Config =
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .AddEnvironmentVariables()
                .Build();
        public MainWindow()
        {
            InitializeComponent();
            Database db = new Database();
            Label1.Content = db.GetName();

            Application.Current.MainWindow = new Login();
            Application.Current.MainWindow.Show();
            //var appWindow = new Login();
            //appWindow.Show();

        }
    }
}
