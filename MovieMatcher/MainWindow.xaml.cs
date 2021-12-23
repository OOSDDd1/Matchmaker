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
            DataContext = new ResultView();
        }

        private void ResultView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new ResultView();
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