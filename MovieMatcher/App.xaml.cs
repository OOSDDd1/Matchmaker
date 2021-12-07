using System.Windows;
using MovieMatcher.Stores;
using MovieMatcher.ViewModels;

namespace MovieMatcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationStore navigationStore = new NavigationStore();

            _navigationStore.CurrentViewModel = new ResultViewModel(navigationStore);
            
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };
            MainWindow.Show();
            
            base.OnStartup(e);
        }
    }
}