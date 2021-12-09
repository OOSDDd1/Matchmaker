using System;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MovieMatcher.Models.Api;
using MovieMatcher.Models.Api.Components;

namespace MovieMatcher.Views
{
    public partial class DetailView: UserControl
    {
        public DetailView()
        {
            InitializeComponent();

            Movie movie = Api.GetMovie(524434);
            BackDropImage.Source = new BitmapImage(new Uri($"https://image.tmdb.org/t/p/w1280/{movie?.backdrop_path}"));
            MovieReleaseDate releaseDate = movie.release_dates.results.First().release_dates.First();
            
            // Left
            Poster.Source = new BitmapImage(new Uri($"https://image.tmdb.org/t/p/w342/{movie?.poster_path}"));
            var videoKey = movie?.videos.results.First(res => res.official && res.site.Equals("YouTube") && res.type.Equals("Trailer")).key;
            Browser.Address = $"https://www.youtube.com/embed/{videoKey}";
            
            // Right
            Title.Content = movie.title;
            
            AgeRating.Content = releaseDate.certification;
            Year.Content = movie.release_date.Substring(0, 4);
            PlayTime.Content = CalculateRunTime(movie.runtime);
            Rating.Content = movie.vote_average + "/10";
            Rating.ToolTip = $"Rating from {movie.vote_count} votes";
            
            Genres.Content = string.Join(", ", movie.genres.Select(genre => genre.name));

            Description.Text = movie.overview;
        }

        private string CalculateRunTime(int length)
        {
            var hours = (length - length % 60) / 60;
            return $"{hours}h {(length - hours * 60)}m";
        }

    }
}