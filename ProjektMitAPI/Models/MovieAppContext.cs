using Microsoft.EntityFrameworkCore;

namespace ProjektMitAPI.Models
{
    public class MovieAppContext : DbContext
    {
        public MovieAppContext(DbContextOptions<MovieAppContext> options)
            : base(options) { }
        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<MovieUserLiked> MoviesUserLiked { get; set; } = null!;

    }
}
