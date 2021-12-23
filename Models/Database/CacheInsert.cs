using System;
using System.Collections.Generic;
using Models.Api.Components;

namespace Models.Database
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
        public List<Cast> Actors;
        public List<Genre> Genres;
        public string Json;
        public DateTime UpdatedAt;
        public bool IsShow;
    }
}