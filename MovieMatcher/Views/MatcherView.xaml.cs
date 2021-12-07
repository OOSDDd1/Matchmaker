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
        private Movie _currentContent;
        private Queue<Movie> _discoveredMovies = new();
        private HashSet<int> _reviewedMovies;
        private int _pageCount = 1;

        public MatcherView()
        {
 
            InitializeComponent();
            _reviewedMovies = Database.GetReviewedMovies(UserInfo.Id);
            SetNewContent();
        }

        private void SetNewContent()
        {
            
            // Get new list of content
            if (_discoveredMovies.Any())
            {
                _currentContent = _discoveredMovies.Dequeue();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("https://image.tmdb.org/t/p/w500/" + _currentContent.poster_path, UriKind.Absolute);
                bitmap.EndInit();
                Image image = new()
                {
                    Source = bitmap,
                    Width = Width
                };
                ContentImage.Source = bitmap;
            }
            // Update information
            else
            {
                SetNewListOfMovies();

            }
            
        }

        private void SetNewListOfMovies()
        {
            var movies = Api.GetDiscoveredMovies(_pageCount);
            
            foreach (var movie in movies.results)
            {
                if (!_reviewedMovies.Contains(movie.id))
                {
                    _discoveredMovies.Enqueue(movie);
                }

            }
            _pageCount++;
            if (_pageCount > 5)
            {
                MessageBox.Show("Whoops");
            }
            SetNewContent();
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
                _currentContent.id,
                UserInfo.Id,
                isLike,
                (bool)SeenCheckBox.IsChecked,
                false
            );

            SeenCheckBox.IsChecked = false;
        }
    }
}