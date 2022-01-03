using System.Windows;
using MovieMatcher.Views;
using Stores;

namespace MovieMatcher
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UserName.Content = UserStore.username;
            DataContext = new HomeView();
        }

        private void HomeView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new HomeView();
        }

        private void SearchView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new SearchView();
        }

        private void Matcher_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new MatcherView();
        }

        private void History_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new HistoryView();
        }

        private void Statistics_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new StatisticsView();
        }

        private void AccountArrow_Clicked(object sender, RoutedEventArgs e)
        {
            if(Collapsable.Visibility == Visibility.Visible)
            {
                Collapsable.Visibility = Visibility.Collapsed;
                MenuButton.Content = "▼";
            }
            else
            {
                Collapsable.Visibility = Visibility.Visible;
                MenuButton.Content = "▲";
            }
        }

        private void LogOut_Clicked(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}