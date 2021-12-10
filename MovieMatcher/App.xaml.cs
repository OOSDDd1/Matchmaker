using System.Windows;
using MovieMatcher.ViewModels;

namespace MovieMatcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var appWindow = new Login();
            appWindow.DataContext = new LoginViewModel();
            appWindow.Show();
        }
    }
}