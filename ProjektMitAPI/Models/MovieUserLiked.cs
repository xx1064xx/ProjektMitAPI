﻿namespace ProjektMitAPI.Models
{
    public class MovieUserLiked
    {
        public long Id { get; set; }

        public long MovieId { get; set; }
        public Movie Movie { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
    }
}
