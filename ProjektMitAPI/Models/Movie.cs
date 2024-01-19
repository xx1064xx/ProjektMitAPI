using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;

namespace ProjektMitAPI.Models
{
    public class Movie
    {
        public long Id { get; set; }
        public string? title { get; set; }
        public string? identifier { get; set; }
        public int? rating { get; set; }
        public string? genre { get; set; }
        public int duration { get; set; }
        public string? releaseDate { get; set; }
        public string? streamingPlatform { get; set; }
        public string? streamingLink { get; set; }
        public string? trailerLink { get; set; }

        public ICollection<User> UsersLiked { get; set; }
        public Collection<Review> Reviews { get; set; }


        public Movie() {
            // movieReviews = new List<Review>();
        }
    }
}
