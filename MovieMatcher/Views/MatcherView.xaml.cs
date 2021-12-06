using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MovieMatcher.Views
{
    public partial class MatcherView : UserControl
    {
        public MatcherView()
        {
            InitializeComponent();
        }

        private void GetNewContent()
        {
            var movie = Api.GetMovie();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("https://image.tmdb.org/t/p/w500/" + movie.poster_path, UriKind.Absolute);
            bitmap.EndInit();
            Image image = new Image();
            image.Source = bitmap;
            image.Width = Width;
            ContentImage.Source = bitmap;
        }

        private void OnLikeClick(object sender, RoutedEventArgs e)
        {
            GetNewContent();
        }
    }
}