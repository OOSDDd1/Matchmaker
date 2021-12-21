using System;

namespace MovieMatcher.Models.Database
{
    public class CacheInsert
    {
        public int Id;
        public string CacheKey;
        public string Title;
        public string Overview;
        public string PosterPath;
        public string BackdropPath;
        public string TrailerUrl;
        public int Age;
        public string json;
        public DateTime UpdatedAt;
    }
}