using System.Windows;
using Models.Database;
using MovieMatcher.ViewModels;
using Stores;

namespace MovieMatcher
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UserName.Content = UserStore.username;
            DataContext = new ResultViewModel();
        }

        private void ResultView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new ResultViewModel();
        }

        private void SearchView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new SearchViewModel();
        }

        private void Matcher_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new MatcherViewModel();
        }

        private void ButtonView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new ButtonViewModel();
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

        private void History_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new HistoryViewModel();
        }
    }
}