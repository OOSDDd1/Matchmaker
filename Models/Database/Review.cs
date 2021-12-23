using System;

namespace Models.Database
{
    public class Review
    {
        public int ContentId { get; set; }
        public int UserId { get; set; }
        public bool Liked { get; set; }
        public bool Watched { get; set; }
        public bool IsShow { get; set; }

        public DateTime DateChanged { get; set; }

        public Review(int id, int uId, bool liked, bool watched, bool isShow, DateTime dateChanged)
        {
            ContentId = id;
            UserId = uId;
            Liked = liked;
            Watched = watched;
            IsShow = isShow;
            DateChanged = dateChanged;
        }
    }
}
