using System.Windows;
using MovieMatcher.Views;
using Stores;

namespace MovieMatcher
{
    public partial class MainWindow : Window
    {
        private MatcherView _matcherView;
        private static DetailView? _detailView;
        public static MainWindow Main;
        
        public MainWindow()
        {
            InitializeComponent();
            UserName.Content = UserStore.username;
            DataContext = new HomeView();
            Main = this;
        }

        private void HomeView_Clicked(object sender, RoutedEventArgs e)
        {
            _detailView?.Browser.Dispose();
            _matcherView?.Browser.Dispose();
            DataContext = new HomeView();
        }

        private void SearchView_Clicked(object sender, RoutedEventArgs e)
        {
            _detailView?.Browser.Dispose();
            _matcherView?.Browser.Dispose();
            DataContext = new SearchView();
        }

        private void Matcher_Clicked(object sender, RoutedEventArgs e)
        {
            _detailView?.Browser.Dispose();
            _matcherView?.Browser.Dispose();
            _matcherView = new MatcherView();
            DataContext = _matcherView;
        }

        private void History_Clicked(object sender, RoutedEventArgs e)
        {
            _detailView?.Browser.Dispose();
            _matcherView?.Browser.Dispose();
            DataContext = new HistoryView();
        }
        
        public static DetailView DetailView_Clicked()
        {
            _detailView = new DetailView();
            _detailView.Initialize();
            
            return _detailView;
        }

        private void Statistics_Clicked(object sender, RoutedEventArgs e)
        {
            Collapsable.Visibility = Visibility.Collapsed;
            MenuButton.Content = "▼";
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
            _detailView?.Browser.Dispose();
            _matcherView?.Browser.Dispose();
            UserStore.Clear();
            DetailViewStore.Clear();
            
            var loginScreen = new Login();
            loginScreen.Show();
            
            Close();
        }

        private void Account_Clicked(object sender, RoutedEventArgs e)
        {
            UserName.Content = UserStore.username;
            DataContext = new AccountView();
        }
    }
}