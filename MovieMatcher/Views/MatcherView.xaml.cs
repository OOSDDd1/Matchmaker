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
        private Queue<Movie> _discoveredMovies = new Queue<Movie>();
        
        
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
                _currentContent = _discoveredMovies.Dequeue();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("https://image.tmdb.org/t/p/w500/" + _currentContent.poster_path, UriKind.Absolute);
                bitmap.EndInit();
                Image image = new Image();
                image.Source = bitmap;
                image.Width = Width;
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
            var movies = Api.GetDiscoveredMovies();

            foreach (var movie in movies.results)
            {
                _discoveredMovies.Enqueue(movie);
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
            //todo Checken of hij al in de database zit, en zo ja updaten inplaats van inserten.[extra DB methode]
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