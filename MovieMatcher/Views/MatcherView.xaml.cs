using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Queue<Movie> _moviesToRecommend = new();
        private HashSet<int> _reviewedMovies;
        private HashSet<int> _likedAndInterestingMovies;
        private Dictionary<int, int> _pagePerLikedOrInterestingMovie = new();
        private int _pageCount = 1;
        private static readonly Random _random = new();

        public MatcherView()
        {
            InitializeComponent();
            _reviewedMovies = Database.GetReviewedMovies(UserInfo.Id);
            _likedAndInterestingMovies = Database.GetInterestingAndLikedMovies();
            SetNewContent();
        }

        private void SetNewContent()
        {
            
            // Get new list of content
            if (_moviesToRecommend.Any())
            {
                _currentContent = _moviesToRecommend.Dequeue();
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
            // Recommend movies from discovery endpoint if user has not liked any movies
            if (_likedAndInterestingMovies.Count == 0)
            {
                var movies = Api.GetDiscoveredMovies(_pageCount);
            
                foreach (var movie in movies.results)
                {
                    if (!_reviewedMovies.Contains(movie.id))
                    {
                        _moviesToRecommend.Enqueue(movie);
                    }

                }
                _pageCount++;
            
            }
            else
            {
                var id =
                    _likedAndInterestingMovies.ElementAt(_random.Next(_likedAndInterestingMovies.Count));
                var page = GetPageForLikedOrInterestingMovie(id);
                Trace.WriteLine(id);
                Trace.WriteLine(page);

                var movies = Api.GetRecommendedMovies(id, page);
                
                foreach (var movie in movies.results)
                {
                    if (!_reviewedMovies.Contains(movie.id))
                    {
                        _moviesToRecommend.Enqueue(movie);
                    }
                }
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

            if(isLike) _likedAndInterestingMovies.Add(_currentContent.id);
            _reviewedMovies.Add(_currentContent.id);

            SeenCheckBox.IsChecked = false;
        }

        private int GetPageForLikedOrInterestingMovie(int id)
        {
            if (_pagePerLikedOrInterestingMovie.ContainsKey(id))
            {
                return ++_pagePerLikedOrInterestingMovie[id];
            }
            else
            {
                _pagePerLikedOrInterestingMovie.Add(id, 1);
                return 1;
            }
        }
    }
}