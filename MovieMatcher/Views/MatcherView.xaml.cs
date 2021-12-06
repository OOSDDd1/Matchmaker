using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using MovieMatcher.Models.Api;
using MovieMatcher.Models.Database;

namespace MovieMatcher.Views
{
    public partial class MatcherView : UserControl
    {
        private int _currentContentId;
        private Queue<Movie> _discoveredMovies;
        
        
        public MatcherView()
        {
            InitializeComponent();
            SetNewContent();
        }

        private void SetNewContent()
        {
            
            // Get new list of content
            if (_discoveredMovies.Any())
            {
                GetNewListOfMovies();
            }
            // Update information
            else
            {
                var movie = _discoveredMovies.Dequeue();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("https://image.tmdb.org/t/p/w500/" + movie.poster_path, UriKind.Absolute);
                bitmap.EndInit();
                Image image = new Image();
                image.Source = bitmap;
                image.Width = Width;
                ContentImage.Source = bitmap;
            }
            
        }

        private List<Movie> GetNewListOfMovies()
        {
            var movie = Api.GetMovie();
        }

        private void OnLikeClick(object sender, RoutedEventArgs e)
        {
            SubmitContentReview(true);
            SetNewContent();
        }

        private void OnDislikeClick(object sender, RoutedEventArgs e)
        {
            SubmitContentReview(false);
            SetNewContent();
        }

        private void SubmitContentReview(bool isLike)
        {
            Database.InsertItem(
                _currentContentId,
                UserInfo.Id,
                isLike,
                SeenCheckBox.IsChecked.GetValueOrDefault(),
                false
            );

            SeenCheckBox.IsChecked = false;
        }
    }
}