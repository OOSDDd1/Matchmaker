using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using MovieMatcher.Models.Api.Components;
using MovieMatcher.Models.Database;
using MovieMatcher.Stores;

namespace MovieMatcher.Views
{
    public partial class DetailView : UserControl
    {
        public DetailView()
        {
            InitializeComponent();

            bool success;
            switch (DetailViewStore.MediaType)
            {
                case "movie":
                    success = MovieDetail(DetailViewStore.Id);
                    break;
                case "tv":
                    success = ShowDetail(DetailViewStore.Id);
                    break;
                default:
                    success = false;
                    return;
            }

            if (!success)
            {
                // TODO: show error message
            }
            else if (Database.CheckForWatched(DetailViewStore.Id, UserInfo.Id, DetailViewStore.MediaType.Equals("tv")) == true)
            {
                SeenCheckBox.IsChecked = true;
            }
        }
        
        private bool MovieDetail(int id)
        {
            if(!ApiService.GetMovie(id, out var movie))
                return false;
            if(movie == null)
                return false;

            BackDropImage.Source = new BitmapImage(new Uri($"https://image.tmdb.org/t/p/w1280/{movie.backdrop_path}"));
            string age;
            try
            {
                age = movie.release_dates.results.First(result => result.iso_3166_1.Equals("NL"))
                    .release_dates.First().certification;
            }
            catch
            {
                age = "0";
            }


            // Left
            // Poster.Source = new BitmapImage(new Uri($"https://image.tmdb.org/t/p/w342/{movie.poster_path}"));

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

            Browser.Address = $"https://www.youtube.com/embed/{videoKey}";

            // Right
            Title.Content = movie.title ?? "";
            TageLine.Content = movie.tagline ?? "";

            AgeRating.Content = age;
            Year.Content = movie.release_date.Substring(0, 4) ?? "";
            PlayTime.Content = CalculateRunTime(movie.runtime) ?? "";
            Rating.Content = movie.vote_average + "/10";
            Rating.ToolTip = $"Rating from {movie.vote_count} votes" ?? "";

            Genres.Content = GenresToString(movie.genres) ?? "";

            Description.Text = movie.overview ?? "";

            BudgetAmount.Content = movie.budget;
            ProductionCompanies.Content = string.Join(", ", movie.production_companies.Select(comp => comp.name));
            Actors.Text = string.Join("\n", 
                movie.credits.cast.OrderBy(person => person.order)
                    .Where(person => person.known_for_department.Equals("Acting"))
                    .Select(person => $"{person.name} ({person.character})")
            );
            return true;
        }

        private bool ShowDetail(int id)
        {
            if(!ApiService.GetShow(id, out var show))
                return false;
            if (show == null)
                return false;

            BackDropImage.Source = new BitmapImage(new Uri($"https://image.tmdb.org/t/p/w1280/{show.backdrop_path}"));

            // Left
            // Poster.Source = new BitmapImage(new Uri($"https://image.tmdb.org/t/p/w342/{show.poster_path}"));
            string videoKey;
            try
            {
                videoKey = show.videos.results
                    .First(res => res.official && res.site.Equals("YouTube") && res.type.Equals("Trailer")).key;
            }
            catch
            {
                videoKey = show.videos.results.First().key;
            }

            Browser.Address = $"https://www.youtube.com/embed/{videoKey}";

            // Right
            Title.Content = show.name;
            TageLine.Content = show.tagline ?? "";
            ShowStats.Content = $"{show.number_of_seasons}S {show.number_of_episodes}E" ?? "";

            string age;
            try
            {
                age = show.content_ratings.results.First(rating => rating.iso_3166_1.Equals("NL")).rating;
            }
            catch
            {
                age = "0";
            }

            AgeRating.Content = age;
            Year.Content = show.first_air_date.Substring(0, 4) ?? "";
            PlayTime.Content = CalculateRunTime(show.number_of_episodes * show.episode_run_time.Count > 0 ? show.episode_run_time.First() : 0 ) ?? "";
            Rating.Content = show.vote_average + "/10" ?? "";
            Rating.ToolTip = $"Rating from {show.vote_count} votes" ?? "";

            Genres.Content = GenresToString(show.genres) ?? "";

            Description.Text = show.overview ?? "";
            
            ProductionCompanies.Content = string.Join(", ", show.production_companies.Select(comp => comp.name));
            Actors.Text = string.Join("\n", 
                show.credits.cast.OrderBy(person => person.order)
                    .Where(person => person.known_for_department.Equals("Acting"))
                    .Select(person => $"{person.name} ({person.character})")
                );
            
            return true;
        }

        private string CalculateRunTime(int length)
        {
            var hours = (length - length % 60) / 60;
            return $"{hours}h {(length - hours * 60)}m";
        }

        private string GenresToString(List<Genre> genres)
        {
            return string.Join(", ", genres.Select(genre => genre.name));
        }
        
        private void OnLikeClick(object sender, RoutedEventArgs e)
        {
            SubmitContentReview(true);
        }

        private void OnDislikeClick(object sender, RoutedEventArgs e)
        {
            SubmitContentReview(false);
        }

        private void SubmitContentReview(bool isLike)
        {
            if (Database.CheckIfReviewed(DetailViewStore.Id, UserInfo.Id, DetailViewStore.MediaType.Equals("tv")) )
            {
                Database.ChangeItem(
                    DetailViewStore.Id,
                    UserInfo.Id,
                    isLike,
                    (bool) SeenCheckBox.IsChecked
                );
            }
            else
            {
                Database.InsertItem(
                    DetailViewStore.Id,
                    UserInfo.Id,
                    isLike,
                    (bool) SeenCheckBox.IsChecked,
                    DetailViewStore.MediaType.Equals("tv")
                );
            }
        }
    }
}