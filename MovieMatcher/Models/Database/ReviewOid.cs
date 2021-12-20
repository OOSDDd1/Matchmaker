using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieMatcher.Models.Database
{
    public class ReviewOid
    {
        public int ContentId { get; set; }
        public int UserId { get; set; }
        public bool Liked { get; set; }
        public bool Watched { get; set; }
        public bool IsShow { get; set; }

        public DateTime DateChanged { get; set; }

        public ReviewOid(int id, int uId, bool liked, bool watched, bool isShow, DateTime dateChanged)
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
