using System;
using System.Collections.Generic;
using System.Linq;
using MovieMatcher.Models.Api;
using MovieMatcher.Models.Api.Components;
using MovieMatcher.Models.Database;
using Newtonsoft.Json;
using RestSharp;
using Season = MovieMatcher.Models.Api.Season;

namespace MovieMatcher
{
    /**
     * Get providers from movie Eternals (524434).
     *      Api.Get<Movie>(Api.MovieBase, Api.GetWatchProviders, 524434);
     *
     * Get show Arcane (Id 94605).
     *      Api.GetShow(94605);
     *
     * Every method returns Api.Message on error.
     */
    public static class Api
    {
        #region Api strings
        private static readonly string ApiKey = MainWindow.Config["api-key"];
        private const string ApiBase = "https://api.themoviedb.org/3/";
        public const string ImageBase = "https://image.tmdb.org/t/p/";

        public const string MovieBase = "movie/";
        public const string ShowBase = "tv/";
        public const string SearchBase = "search/";
        public const string TrendingBase = "trending/";


        // For both movies and shows
        public const string GetDetails = "{id}";
        public const string GetImages = "{id}/images";
        public const string GetVideos = "{id}/videos";
        public const string GetWatchProviders = "{id}/watch/providers";
        public const string GetSimilar = "{id}/similar";
        public const string GetRecommendations = "{id}/recommendations";

        public const string GetTrendingList = "{media_type}/{time_window}";

        // Just for Movies
        public const string GetUpcoming = "upcoming";
        public const string GetNowPlaying = "now_playing";
        public const string ReleaseDates = "{id}/release_dates";

        // Just for Series
        public const string GetShowSeason = "{id}/season/{season}";
        public const string GetShowEpsiode = "{id}/season/{season}/episode/{episode}";
        public const string ContentRatings = "{id}/content_ratings";

        // Discover/Random
        public const string GetRandomMovie = "discover/movie";
        // public const string GetRandomShow = "discover/tv";

        // Search
        public const string SearchMulti = "multi";

        // SearchSizes
        public const string W185 = "w185";

        public const string YtTrailerUrl = "https://www.youtube.com/embed/";
        
        #endregion

        private static Dictionary<string, dynamic> _cache = new();

        public static dynamic? GetMovie(int id)
        {
            var urlSegments = new Dictionary<string, string>
                {{"id", id.ToString()}};
            var urlParameters = new Dictionary<string, string>
                {{"append_to_response", "videos,images,release_dates,credits"}};

            var cacheKey = GenerateCacheKey(MovieBase, GetDetails, urlSegments, urlParameters);

            if (CacheGetFromMemory(cacheKey, out var movie)) 
                return movie;
            
            if (CacheGetFromDatabase(cacheKey, out movie))
            {
                CacheAddToMemory(cacheKey, movie);
                return movie;
            }
            
            movie = Get<Movie>(MovieBase, GetDetails, urlSegments, urlParameters);
            
            if (movie is Movie)
                CacheAddMovieToDatabase(cacheKey, movie);

            return movie;
        }

        public static dynamic? GetTrending(string time)
        {
            var urlSegments = new Dictionary<string, string>
                {{"media_type", "all"}, {"time_window", time}};

            return Get<MultiSearch>(TrendingBase, GetTrendingList, urlSegments);
        }

        public static dynamic? GetDiscoveredMovies(int page)
        {
            var urlSegments = new Dictionary<string, string> {};
            var urlParameters = new Dictionary<string, string>
            {
                {"append_to_response", "videos,images,release_dates,credits"},
                {"page", page.ToString()}
            };
            
            return Get<DiscoveredMovie>(string.Empty, GetRandomMovie, urlSegments, urlParameters);
        }

        // Gets recommended movies based on a movie's id
        public static dynamic? GetRecommendedMovies(int id, int page)
        {
            var urlSegments = new Dictionary<string, string>
                {{"id", id.ToString()}};
            var urlParameters = new Dictionary<string, string>
            {
                {"append_to_response", "videos,images,release_dates,credits"},
                {"page", page.ToString()}
            };
            
            return Get<DiscoveredMovie>(MovieBase, GetRecommendations, urlSegments, urlParameters);
        }

        public static dynamic? GetShow(int id)
        {
            var urlSegments = new Dictionary<string, string>
                {{"id", id.ToString()}};
            var urlParameters = new Dictionary<string, string>
                {{"append_to_response", "videos,images,content_ratings,credits"}};
            
            var cacheKey = GenerateCacheKey(ShowBase, GetDetails, urlSegments, urlParameters);

            if (CacheGet(cacheKey, out var show)) 
                return show;
            
            show = Get<Show>(MovieBase, GetDetails, urlSegments, urlParameters);
            
            if (show is Show)
                CacheAddShowToDatabase(cacheKey, show);

            return show;
        }

        public static dynamic? GetSeason(int id, int season)
        {
            var urlSegments = new Dictionary<string, string>
            {
                {"id", id.ToString()},
                {"season", season.ToString()}
            };
            var urlParameters = new Dictionary<string, string>
                {{"append_to_response", "videos,images"}};
            
            return Get<Season>(ShowBase, GetShowSeason, urlSegments, urlParameters);
        }

        public static dynamic? GetEpisode(int id, int season, int episode)
        {
            var urlSegments = new Dictionary<string, string>
            {
                {"id", id.ToString()},
                {"season", season.ToString()},
                {"episode", episode.ToString()}
            };
            var urlParameters = new Dictionary<string, string>
                {{"append_to_response", "videos,images"}};
            
            return Get<Episode>(ShowBase, GetShowEpsiode, urlSegments, urlParameters);
        }

        public static dynamic? GetProviders(string resourceBase, int id)
        {
            var urlSegments = new Dictionary<string, string>
                {{"id", id.ToString()}};
            
            return Get<Providers>(resourceBase, GetWatchProviders, urlSegments);
        }

        public static dynamic? GetMovieReleaseDates(int id)
        {
            var urlSegments = new Dictionary<string, string>
                {{"id", id.ToString()}};
            
            return Get<MovieReleaseDates>(MovieBase, ReleaseDates, urlSegments);
        }

        public static dynamic? GetSerieContentRatings(int id)
        {
            var urlSegments = new Dictionary<string, string>
                {{"id", id.ToString()}};
            
            return Get<ShowContentRatings>(ShowBase, ContentRatings, urlSegments);
        }

        public static dynamic? Search(string query, bool adult = false)
        {
            var urlParameters = new Dictionary<string, string>
            {
                {"query", query},
                {"include_adult", adult.ToString().ToLower()}
            };

            return Get<MultiSearch>(SearchBase, SearchMulti, new Dictionary<string, string>(), urlParameters);
        }

        #region Api Get + overloaders
        public static dynamic? Get<T>(string resourceBase, string resource)
            where T : IRoot
        {
            return Get<T>(resourceBase, resource, new Dictionary<string, string>());
        }

        public static dynamic? Get<T>(string resourceBase, string resource, int id)
            where T : IRoot
        {
            var urlSegments = new Dictionary<string, string>
                {{"id", id.ToString()}};
            return Get<T>(resourceBase, resource, urlSegments);
        }

        public static dynamic? Get<T>(string resourceBase, string resource, Dictionary<string, string> urlSegments)
            where T : IRoot
        {
            return Get<T>(resourceBase, resource, urlSegments, new Dictionary<string, string>());
        }

        public static dynamic? Get<T>(string resourceBase, string resource, Dictionary<string, string> urlSegments,
            Dictionary<string, string> urlParameters)
            where T : IRoot
        {
            var cacheKey = GenerateCacheKey(resourceBase, resource, urlSegments, urlParameters);
            
            if (CacheGet(cacheKey, out var cachedResource))
                return cachedResource;
            
            var response = GenerateResponse(resourceBase + resource, urlSegments, urlParameters);
            
            if (!response.IsSuccessful)
                return ResponseToClass<Message>(response.Content);
            
            var classedResponse = ResponseToClass<T>(response.Content);
            
            CacheAddToMemory(cacheKey, classedResponse);
            
            return classedResponse;
        }

        private static IRestResponse GenerateResponse(string resource, Dictionary<string, string> urlSegments,
            Dictionary<string, string> urlParameters)
        {
            var client = new RestClient(ApiBase);

            var request = new RestRequest(resource, DataFormat.Json)
                .AddParameter("api_key", ApiKey);

            foreach (var (key, value) in urlParameters)
                request.AddParameter(key, value);

            foreach (var (key, value) in urlSegments)
                request.AddUrlSegment(key, value);

            return client.Get(request);
        }

        private static T? ResponseToClass<T>(string response) where T : IRoot
        {
            return JsonConvert.DeserializeObject<T>(response);
        }

        #endregion

        #region Cache Handling
        
        private static string GenerateCacheKey(string resourceBase, string resource, Dictionary<string, string> urlSegments, Dictionary<string, string> urlParameters)
        {
            return resourceBase + resource + string.Join("", urlSegments.Values) + string.Join("", urlParameters.Values);
        }

        private static bool CacheAddToMemory(string key, dynamic? value)
        {
            return _cache.TryAdd(key, value);
        }

        private static bool CacheAddMovieToDatabase(string key, Movie movie)
        {
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

            MovieReleaseDate releaseDate;
            try
            {
                releaseDate = movie.release_dates.results.First(result => result.iso_3166_1.Equals("NL"))
                    .release_dates.First();
            }
            catch
            {
                releaseDate = movie.release_dates.results.First().release_dates.First();
            }
            int.TryParse(releaseDate.certification, out int age);

            return Database.InsertCache<Movie>(new CacheInsert()
            {
                Id = movie.id,
                CacheKey = key,
                Title = movie.title,
                Overview = movie.overview,
                PosterPath = movie.poster_path,
                BackdropPath = movie.backdrop_path,
                TrailerUrl = YtTrailerUrl + videoKey,
                Age = age,
                Json = JsonConvert.SerializeObject(movie),
                UpdatedAt = DateTime.Now,
            });
        }

        private static bool CacheAddShowToDatabase(string key, Show value)
        {
            string videoKey;
            try
            {
                videoKey = value.videos.results
                    .First(res => res.official && res.site.Equals("YouTube") && res.type.Equals("Trailer")).key;
            }
            catch
            {
                videoKey = value.videos.results.First().key;
            }

            string ageRating;
            try
            {
                ageRating = value.content_ratings.results.First(rating => rating.iso_3166_1.Equals("NL")).rating;
            }
            catch
            {
                ageRating = value.content_ratings.results.First().rating;
            }
            int.TryParse(ageRating, out int age);
            
            return Database.InsertCache<Show>(new CacheInsert()
            {
                Id = value.id,
                CacheKey = key,
                Title = value.name,
                Overview = value.overview,
                PosterPath = value.poster_path,
                BackdropPath = value.backdrop_path,
                TrailerUrl = YtTrailerUrl + videoKey,
                Age = age,
                Json = JsonConvert.SerializeObject(value),
                UpdatedAt = DateTime.Now,
            });
        }

        private static bool CacheGet(string key, out dynamic? value)
        {
            if (CacheGetFromMemory(key, out value)) 
                return true;

            if (CacheGetFromDatabase(key, out value))
            {
                CacheAddToMemory(key, value);
                return true;
            }

            return false;
        }
        
        private static bool CacheGetFromMemory(string key, out dynamic? value)
        {
            return _cache.TryGetValue(key, out value);
        }
        
        private static bool CacheGetFromDatabase(string key, out dynamic? value)
        {
            return _cache.TryGetValue(key, out value);
        }
        
        #endregion
    }
}