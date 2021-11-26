using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using MovieMatcher.ViewModels;
using System;
using Newtonsoft.Json;

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
            Database db = new Database();
        }

        private void ResultView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new ResultViewModel();
        }

        private void SearchView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new SearchViewModel();
        }
    }
}