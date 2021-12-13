using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using MovieMatcher.Models.Api;
using MovieMatcher.Models.Api.Components;
using MovieMatcher.Stores;
using Newtonsoft.Json;

namespace MovieMatcher.Views
{
    public partial class DetailView : UserControl
    {
        public DetailView()
        {
            InitializeComponent();
            Console.WriteLine(JsonConvert.SerializeObject(DetailViewStore.MediaType));

            switch (DetailViewStore.MediaType)
            {
                case "movie":
                    MovieDetail(DetailViewStore.Id);
                    break;
                case "tv":
                    ShowDetail(DetailViewStore.Id);
                    break;
                default:
                    return;
            }
        }

        private void MovieDetail(int id)
        {
            Movie? movie = Api.GetMovie(id);

            if (movie == null)
            {
                return;
            }

            BackDropImage.Source = new BitmapImage(new Uri($"https://image.tmdb.org/t/p/w1280/{movie.backdrop_path}"));
            MovieReleaseDate releaseDate = movie.release_dates.results.First(result => result.iso_3166_1.Equals("NL"))
                .release_dates.First();

            // Left
            Poster.Source = new BitmapImage(new Uri($"https://image.tmdb.org/t/p/w342/{movie.poster_path}"));
            var videoKey = movie.videos.results
                .First(res => res.official && res.site.Equals("YouTube") && res.type.Equals("Trailer")).key;
            Browser.Address = $"https://www.youtube.com/embed/{videoKey}";

            // Right
            Title.Content = movie.title ?? "";

            AgeRating.Content = releaseDate.certification;
            Year.Content = movie.release_date.Substring(0, 4) ?? "";
            PlayTime.Content = CalculateRunTime(movie.runtime) ?? "";
            Rating.Content = movie.vote_average + "/10";
            Rating.ToolTip = $"Rating from {movie.vote_count} votes" ?? "";

            Genres.Content = GenresToString(movie.genres) ?? "";

            Description.Text = movie.overview ?? "";
        }

        private void ShowDetail(int id)
        {
            Show? show = Api.GetShow(id);

            if (show == null)
            {
                return;
            }

            BackDropImage.Source = new BitmapImage(new Uri($"https://image.tmdb.org/t/p/w1280/{show.backdrop_path}"));

            // Left
            Poster.Source = new BitmapImage(new Uri($"https://image.tmdb.org/t/p/w342/{show.poster_path}"));
            var videoKey = show.videos.results
                .First(res => res.official && res.site.Equals("YouTube") && res.type.Equals("Trailer")).key;
            Browser.Address = $"https://www.youtube.com/embed/{videoKey}";

            // Right
            Title.Content = show.name;
            TageLine.Content = show.tagline ?? "";
            ShowStats.Content = $"{show.number_of_seasons}S {show.number_of_episodes}E" ?? "";

            AgeRating.Content = show.content_ratings.results.First(rating => rating.iso_3166_1.Equals("NL")).rating;
            Year.Content = show.first_air_date.Substring(0, 4) ?? "";
            PlayTime.Content = CalculateRunTime(show.number_of_episodes * show.episode_run_time.First()) ?? "";
            Rating.Content = show.vote_average + "/10" ?? "";
            Rating.ToolTip = $"Rating from {show.vote_count} votes" ?? "";

            Genres.Content = GenresToString(show.genres) ?? "";

            Description.Text = show.overview ?? "";
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
    }
}