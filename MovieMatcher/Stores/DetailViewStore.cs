namespace MovieMatcher.Stores
{
    public static class DetailViewStore
    {
        public static MediaTypes MediaType = MediaTypes.Show;
        public static int Id = 127235;
    }

    public enum MediaTypes
    {
        Movie,
        Show
    }
}