using System;

namespace Stores
{
    public static class DetailViewStore
    {
        public static string MediaType;
        public static int Id;

        public static void Clear()
        {
            MediaType = string.Empty;
            Id = int.MinValue;
        }
    }
}