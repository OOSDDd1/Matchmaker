using System;
using System.Collections.Generic;
using System.Linq;
using Models.Api;
using Models.Api.Components;
using Models.Database;
using Newtonsoft.Json;
using RestSharp;
using Season = Models.Api.Season;

namespace Services
{
    /*
     * Get providers from movie Eternals (524434).
     *      Api.Get<Movie>(Api.MovieBase, Api.GetWatchProviders, 524434);
     *
     * Get show Arcane (Id 94605).
     *      Api.GetShow(94605);
     *
     * ApiService Methods are returning false on error (true on success) this is when the out parameter will be empty.
     */
    public static class ApiService
    {
        /*
         * These strings are used to compile an url which will be used to create a request to the movie database.
         */
        #region Api strings

        private static readonly string ApiKey = ConfigService.Get["api-key"];
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
        
        private static readonly Dictionary<string, dynamic> Cache = new();

        /*
         * These functions are specific implementations of the Get<T> function.
         *  Eg. GetMovie uses Get but with all the parameters filled in. The user only passes an id.
         */
        #region Non generalized api calls
        
        public static bool GetMovie(int id, out Movie? movie)
        {
            var urlSegments = new Dictionary<string, string>
                { { "id", id.ToString() } };
            var urlParameters = new Dictionary<string, string>
                { { "append_to_response", "videos,images,release_dates,credits" } };

            var cacheKey = GenerateCacheKey(MovieBase, GetDetails, urlSegments, urlParameters);

            if (CacheGetFromMemory(cacheKey, out movie))
                return true;

            if (CacheGetFromDatabase<Movie>(cacheKey, out movie))
            {
                CacheAddToMemory(cacheKey, movie); // CacheGetFromDatabase returns true -> movie is not null
                return true;
            }

            if (!Get<Movie>(MovieBase, GetDetails, urlSegments, urlParameters, out movie))
                return false;

            CacheAddMovieToDatabase(cacheKey, movie);  // Get returns true -> movie is not null

            return true;
        }

        public static bool GetTrending(string time, out MultiSearch? response)
        {
            var urlSegments = new Dictionary<string, string>
                { { "media_type", "all" }, { "time_window", time } };

            return Get<MultiSearch>(TrendingBase, GetTrendingList, urlSegments, out response);
        }

        public static bool GetDiscoveredMovies(int page, out DiscoveredMovie? response)
        {
            var urlSegments = new Dictionary<string, string> { };
            var urlParameters = new Dictionary<string, string>
            {
                { "append_to_response", "videos,images,release_dates,credits" },
                { "page", page.ToString() }
            };

            return Get<DiscoveredMovie>(string.Empty, GetRandomMovie, urlSegments, urlParameters, out response);
        }

        // Gets recommended movies based on a movie's id
        public static bool GetRecommendedMovies(int id, int page, out DiscoveredMovie? response)
        {
            var urlSegments = new Dictionary<string, string>
                { { "id", id.ToString() } };
            var urlParameters = new Dictionary<string, string>
            {
                { "append_to_response", "videos,images,release_dates,credits" },
                { "page", page.ToString() }
            };

            return Get<DiscoveredMovie>(MovieBase, GetRecommendations, urlSegments, urlParameters, out response);
        }

        public static bool GetMovieReleaseDates(int id, out MovieReleaseDates? response)
        {
            var urlSegments = new Dictionary<string, string>
                { { "id", id.ToString() } };

            return Get<MovieReleaseDates>(MovieBase, ReleaseDates, urlSegments, out response);
        }

        public static bool GetShow(int id, out Show? response)
        {
            var urlSegments = new Dictionary<string, string>
                { { "id", id.ToString() } };
            var urlParameters = new Dictionary<string, string>
                { { "append_to_response", "videos,images,content_ratings,credits" } };

            var cacheKey = GenerateCacheKey(ShowBase, GetDetails, urlSegments, urlParameters);

            if (CacheGetFromMemory(cacheKey, out response))
                return true;

            if (CacheGetFromDatabase<Show>(cacheKey, out response))
            {
                CacheAddToMemory(cacheKey, response); // CacheGetFromDatabase returns true -> response is not null
                return true;
            }

            if (!Get<Show>(ShowBase, GetDetails, urlSegments, urlParameters, out response))
                return false;
            
            CacheAddShowToDatabase(cacheKey, response); // Get returns true -> response is not null
            
            return true;
        }

        public static bool GetSeason(int id, int season, out Season? response)
        {
            var urlSegments = new Dictionary<string, string>
            {
                { "id", id.ToString() },
                { "season", season.ToString() }
            };
            var urlParameters = new Dictionary<string, string>
                { { "append_to_response", "videos,images,content_ratings,credits" } };

            return Get<Season>(ShowBase, GetShowSeason, urlSegments, urlParameters, out response);
        }

        public static bool GetEpisode(int id, int season, int episode, out Episode? response)
        {
            var urlSegments = new Dictionary<string, string>
            {
                { "id", id.ToString() },
                { "season", season.ToString() },
                { "episode", episode.ToString() }
            };
            var urlParameters = new Dictionary<string, string>
                { { "append_to_response", "videos,images,content_ratings,credits" } };

            return Get<Episode>(ShowBase, GetShowEpsiode, urlSegments, urlParameters, out response);
        }

        public static bool GetSerieContentRatings(int id, out ShowContentRatings? response)
        {
            var urlSegments = new Dictionary<string, string>
                { { "id", id.ToString() } };

            return Get<ShowContentRatings>(ShowBase, ContentRatings, urlSegments, out response);
        }

        public static bool GetProviders(string resourceBase, int id, out Providers? response)
        {
            var urlSegments = new Dictionary<string, string>
                { { "id", id.ToString() } };

            return Get<Providers>(resourceBase, GetWatchProviders, urlSegments, out response);
        }

        public static bool Search(string query, out MultiSearch? response, bool adult = false)
        {
            var urlParameters = new Dictionary<string, string>
            {
                { "query", query },
                { "include_adult", adult.ToString().ToLower() }
            };

            return Get<MultiSearch>(SearchBase, SearchMulti, new Dictionary<string, string>(), urlParameters, out response);
        }

        #endregion
        
        /*
         * These functions communicate and transforms the requests to the movie database.
         * The Api returns a json which is transformed to a class using an external library.
         */
        #region General Api Get + overloader

        public static bool Get<T>(string resourceBase, string resource, Dictionary<string, string> urlSegments, out T? response)
            where T : IRoot
        {
            return Get<T>(resourceBase, resource, urlSegments, new Dictionary<string, string>(), out response);
        }

        /**
         * resourceBase defines if it is a movie or show(tv). Eg. `movie/`.
         * resource is what information you want. Eg. `{id}/watch/providers` to get where the movie is available.
         * urlSegments is the variable inside the resource. Eg. `movie/{id}/watch/providers` contains the variable id.
         * urlParameters are the variables behind the question mark. Eg. `movie/5678?api-key=abc123`.
         * response is what the Api returns and can be empty.
         * If an item is returned from the Api this function returns true, false otherwise.
         */
        public static bool Get<T>(string resourceBase, string resource, Dictionary<string, string> urlSegments,
            Dictionary<string, string> urlParameters, out T? response)
            where T : IRoot
        {
            var cacheKey = GenerateCacheKey(resourceBase, resource, urlSegments, urlParameters);

            if (CacheGetFromMemory<T>(cacheKey, out response))
                return true;

            var apiResponse = GenerateResponse(resourceBase + resource, urlSegments, urlParameters);

            if (!apiResponse.IsSuccessful)
                return false;

            if (!ResponseToClass<T>(apiResponse.Content, out response))
                return false;

            CacheAddToMemory(cacheKey, response); // ResponseToClass returns true -> response is not null

            return true;
        }

        /**
         * Resource is where the call is made to. Eg. `movie/trending`.
         * urlSegments is the variables inside the resource. Eg. `movie/{id}` contains the variable id.
         * urlParameters are the variables behind the question mark. Eg. `movie/5678?api-key=abc123`.
         */
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

        private static bool ResponseToClass<T>(string response, out T? classifiedResponse) where T : IRoot
        {
            classifiedResponse = JsonConvert.DeserializeObject<T>(response);
            return classifiedResponse != null;
        }

        #endregion

        /*
         * Every call to the api is costly in time (the api itself is free), this is why an cache is a must have.
         * We have a memory and database cache.
         * Every request is stored in memory.
         * Only Movies and Shows are stored in the database, these are used for the statistics page.
         */
        #region Cache Handling

        private static string GenerateCacheKey(string resourceBase, string resource,
            Dictionary<string, string> urlSegments, Dictionary<string, string> urlParameters)
        {
            return resourceBase + resource + string.Join("", urlSegments.Values) +
                   string.Join("", urlParameters.Values);
        }
        
        private static bool CacheAddToMemory(string key, dynamic value)
        {
            return Cache.TryAdd(key, value);
        }

        private static bool CacheAddMovieToDatabase(string key, Movie? movie)
        {
            if (movie == null)
                return false;

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

            string releaseDate;
            try
            {
                releaseDate = movie.releaseDates.results.First(result => result.iso31661.Equals("NL"))
                    .releaseDates.First().certification;
            }
            catch
            {
                releaseDate = "0";
            }

            int.TryParse(releaseDate, out int age);

            List<Cast> actors = movie.credits.cast.OrderBy(person => person.order)
                .Where(person => person.knownForDepartment.Equals("Acting")).ToList();

            return DatabaseService.InsertCache(new CacheInsert()
            {
                Id = movie.id,
                CacheKey = key,
                Title = movie.title,
                Overview = movie.overview,
                PosterPath = movie.posterPath,
                BackdropPath = movie.backdropPath,
                TrailerUrl = YtTrailerUrl + videoKey,
                Age = age,
                Actors = actors,
                Genres = movie.genres,
                Json = JsonConvert.SerializeObject(movie),
                UpdatedAt = DateTime.Now,
                IsShow = false,
            });
        }

        private static bool CacheAddShowToDatabase(string key, Show? show)
        {
            if (show == null)
                return false;
            
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

            string ageRating;
            try
            {
                ageRating = show.contentRatings.results.First(rating => rating.iso31661.Equals("NL")).rating;
            }
            catch
            {
                ageRating = "0";
            }

            int.TryParse(ageRating, out int age);

            List<Cast> actors = show.credits.cast.OrderBy(person => person.order)
                .Where(person => person.knownForDepartment.Equals("Acting")).ToList();

            return DatabaseService.InsertCache(new CacheInsert()
            {
                Id = show.id,
                CacheKey = key,
                Title = show.name,
                Overview = show.overview,
                PosterPath = show.posterPath,
                BackdropPath = show.backdropPath,
                TrailerUrl = YtTrailerUrl + videoKey,
                Age = age,
                Actors = actors,
                Genres = show.genres,
                Json = JsonConvert.SerializeObject(show),
                UpdatedAt = DateTime.Now,
                IsShow = true,
            });
        }

        private static bool CacheGetFromMemory<T>(string key, out T? value) where T : IRoot
        {
            Cache.TryGetValue(key, out var root);
            
            // Cast dynamic to generic T. Only classes inheriting IRoot can be stored and restored.
            if (root != null)
            {
                value = (T) root;
                return true;
            }

            // The out variable must be set.
            value = default;
            return false;
        }

        private static bool CacheGetFromDatabase<T>(string key, out T? value) where T : IRoot
        {
            return ResponseToClass<T>(
                DatabaseService.GetCache(key),
                out value
            );
        }

        #endregion
    }
}