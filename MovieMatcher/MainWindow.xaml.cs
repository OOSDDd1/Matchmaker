using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using MovieMatcher.ViewModels;
using System;
using Newtonsoft.Json;
using MovieMatcher.Models.Database;

namespace MovieMatcher
{
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
            UserName.Content = UserInfo.Username;
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

        private void Statics_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new StaticsViewModel();
        }
    }
}