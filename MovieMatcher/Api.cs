using System.Collections.Generic;
using MovieMatcher.Models.Api;
using Newtonsoft.Json;
using RestSharp;

namespace MovieMatcher
{
    /**
     * Get providers from movie: 370172.
     *      Api.Get<Movie>(Api.MovieBase, Api.GetWatchProviders, 370172);
     *
     * Get show: 94605.
     *      Api.GetShow(94605);
     */
    public static class Api
    {
        private static readonly string ApiKey = MainWindow.Config["api-key"];
        private const string ApiBase = "https://api.themoviedb.org/3/";
        public const string ImageBase = "https://image.tmdb.org/t/p/";

        public const string MovieBase = "movie/";
        public const string ShowBase = "tv/";
        public const string SearchBase = "search/";

        // For both movies and shows
        public const string GetDetails = "{id}";
        public const string GetImages = "{id}/images";
        public const string GetVideos = "{id}/videos";
        public const string GetWatchProviders = "{id}/watch/providers";
        public const string GetSimilar = "{id}/similar";
        public const string GetRecommendations = "{id}/recommendations";

        // Just for Movies
        public const string GetUpcoming = "upcoming";
        public const string GetNowPlaying = "now_playing";

        // Just for Series
        public const string GetShowSeason = "{id}/season/{season}";
        public const string GetShowEpsiode = "{id}/season/{season}/episode/{episode}";

        //Search
        public const string SearchMulti = "multi";

        //SearchSizes
        public const string W185 = "w185";

        public static Movie? GetMovie(int id)
        {
            var urlSegments = new Dictionary<string, string>
                {{"id", id.ToString()}};
            var urlParameters = new Dictionary<string, string>
                {{"append_to_response", "videos,images"}};
            return Get<Movie>(MovieBase, GetDetails, urlSegments, urlParameters);
        }

        public static Show? GetShow(int id)
        {
            var urlSegments = new Dictionary<string, string>
                {{"id", id.ToString()}};
            var urlParameters = new Dictionary<string, string>
                {{"append_to_response", "videos,images"}};
            return Get<Show>(ShowBase, GetDetails, urlSegments, urlParameters);
        }

        public static Season? GetSeason(int id, int season)
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

        public static Episode? GetEpisode(int id, int season, int episode)
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

        public static T? GetProviders<T>(string resourceBase, int id)
            where T : IRoot
        {
            var urlSegments = new Dictionary<string, string>
                {{"id", id.ToString()}};
            return Get<T>(resourceBase, GetWatchProviders, urlSegments);
        }

        public static MultiSearch? Search(string query)
        {
            var urlParameters = new Dictionary<string, string> { { "query", query } };
            return Get<MultiSearch>(SearchBase, SearchMulti, new Dictionary<string, string>(), urlParameters);
        }

        public static T? Get<T>(string resourceBase, string resource)
            where T : IRoot
        {
            return Get<T>(resourceBase, resource, new Dictionary<string, string>());
        }

        public static T? Get<T>(string resourceBase, string resource, int id)
            where T : IRoot
        {
            var urlSegments = new Dictionary<string, string>
                {{"id", id.ToString()}};
            return Get<T>(resourceBase, resource, urlSegments);
        }

        public static T? Get<T>(string resourceBase, string resource, Dictionary<string, string> urlSegments)
            where T : IRoot
        {
            return Get<T>(resourceBase, resource, urlSegments, new Dictionary<string, string>());
        }

        public static T? Get<T>(string resourceBase, string resource, Dictionary<string, string> urlSegments,
            Dictionary<string, string> urlParameters)
            where T : IRoot
        {
            var response = GenerateResponse(resourceBase + resource, urlSegments, urlParameters);

            return ResponseToClass<T>(response.Content);
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
    }
}