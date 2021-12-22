using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using MovieMatcher.Models.Api;
using MovieMatcher.Models.Api.Components;
using MovieMatcher.Models.Database;
using MovieMatcher.Stores;

namespace MovieMatcher.Views
{
    public partial class MatcherView : UserControl
    {
        private Movie _currentRecommendation;
        private Queue<KeyValuePair<Movie, int>> _moviesToRecommend = new();
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
                var recommendation = _moviesToRecommend.Dequeue();
                _currentRecommendation = recommendation.Key;
                var currentRecommendationSource = recommendation.Value;

                Movie? movie = ApiService.GetMovie(_currentRecommendation.id);
                Movie? movieSource;
                if (currentRecommendationSource != -1) movieSource = ApiService.GetMovie(currentRecommendationSource);
                else movieSource = null;

                if (movie == null)
                {
                    return;
                }

                Title.Text = movie.title;
                Tagline.Text = movie.tagline;
                Genres.Text = GenresToString(movie.genres) ?? "";
                Description.Text = movie.overview;

                if (movieSource != null)
                {
                    RecommendationSource.Text = movieSource.title;
                    if (RecommendationSourceLabel.Visibility == Visibility.Hidden) RecommendationSourceLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    if (RecommendationSourceLabel.Visibility == Visibility.Visible) RecommendationSourceLabel.Visibility = Visibility.Hidden;
                }

                // Set trailer or poster
                string videoKey = "";
                try
                {
                    videoKey = movie.videos.results
                        .First(res => res.official && res.site.Equals("YouTube") && res.type.Equals("Trailer")).key;
                }
                catch
                {
                    if (movie.videos.results.Count != 0)
                        videoKey = movie.videos.results.First().key;
                }

                if (videoKey.IsValidYoutubeVideoId())
                {
                    Browser.Address = $"https://www.youtube.com/embed/{videoKey}?autoplay=1&loop=1&modestbranding=1&rel=0&playlist={videoKey}";
                    if (ContentImage.Visibility == Visibility.Visible) ContentImage.Visibility = Visibility.Hidden;
                    if (Browser.Visibility == Visibility.Hidden) Browser.Visibility = Visibility.Visible;
                }
                else
                {
                    BitmapImage bitmap = new();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri("https://image.tmdb.org/t/p/w500/" + _currentRecommendation.poster_path,
                        UriKind.Absolute);
                    bitmap.EndInit();

                    ContentImage.Source = bitmap;
                    if (ContentImage.Visibility == Visibility.Hidden) ContentImage.Visibility = Visibility.Visible;
                    if (Browser.Visibility == Visibility.Visible) Browser.Visibility = Visibility.Hidden;
                    // Navigate to black screen video with no sound so:
                    // - The previous player stops playing
                    // - The transition to the next trailer is comparable to a transition between trailers
                    Browser.Address = "https://www.youtube.com/embed/AjWfY7SnMBI?autoplay=1&loop=1&modestbranding=1&rel=0&playlist=AjWfY7SnMBI";
                }
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
                var movies = ApiService.GetDiscoveredMovies(_pageCount);

                foreach (var movie in movies.results)
                {
                    if (!_reviewedMovies.Contains(movie.id))
                    {
                        _moviesToRecommend.Enqueue(new KeyValuePair<Movie, int>(movie, -1));
                    }
                }

                _pageCount++;
            }
            else
            {
                var id =
                    _likedAndInterestingMovies.ElementAt(_random.Next(_likedAndInterestingMovies.Count));
                var page = GetPageForLikedOrInterestingMovie(id);
                var movies = ApiService.GetRecommendedMovies(id, page);

                foreach (var movie in movies.results)
                {
                    if (!_reviewedMovies.Contains(movie.id))
                    {
                        _moviesToRecommend.Enqueue(new KeyValuePair<Movie, int>(movie, id));
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

        private void OnMoreInfoClick(object sender, RoutedEventArgs e)
        {
            DetailViewStore.Id = _currentRecommendation.id;
            DetailViewStore.MediaType = _currentRecommendation.media_type;

            Window window = new()
            {
                Title = _currentRecommendation.title,
                Content = new DetailView()
            };

            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Width = SystemParameters.PrimaryScreenWidth;
            window.ShowDialog();
        }

        private void SubmitContentReview(bool isLike)
        {
            Database.InsertItem(
                _currentRecommendation.id,
                UserInfo.Id,
                isLike,
                (bool)SeenCheckBox.IsChecked,
                false
            );

            if (isLike) _likedAndInterestingMovies.Add(_currentRecommendation.id);
            _reviewedMovies.Add(_currentRecommendation.id);

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

        private string GenresToString(List<Genre> genres)
        {
            return string.Join(", ", genres.Select(genre => genre.name));
        }
    }
}