﻿namespace ProjektMitAPI.Models
{
    public class User
    {
        public long Id { get; set; }
        public string? firstName { get; set; }
        public string? familyName { get; set; }
        public string? username { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public string? Salt { get; set; }

        public ICollection<MovieUserLiked> likes { get; set; }
    }
}
