using System.Windows;
using MovieMatcher.Views;
using Stores;

namespace MovieMatcher
{
    public partial class MainWindow : Window
    {
        private MatcherView _matcherView;
        private static DetailView? _detailView;
        
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
            _matcherView ??= new MatcherView();
            DataContext = _matcherView;
        }

        private void History_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new HistoryView();
        }
        
        public static DetailView DetailView_Clicked()
        {
            _detailView ??= new DetailView();
            _detailView.Initialize();
            
            return _detailView;
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
            UserStore.Clear();
            DetailViewStore.Clear();
            
            var loginScreen = new Login();
            loginScreen.Show();
            
            Close();
        }
    }
}