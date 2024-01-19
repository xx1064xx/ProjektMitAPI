using ProjektMitAPI.Models;

namespace ProjektMitAPI.Data
{
    public class DbInitializer
    {
        private readonly MovieAppContext _context;
        public DbInitializer(MovieAppContext context)
        {
            _context = context;
        }
        public void Run()
        {
            if (_context.Database.EnsureCreated())
            {
                string salt;
                string pwHash = HashGenerator.GenerateHash("admin", out salt);
                _context.Users.Add(
                    new User
                    {
                        firstName = "admin",
                        familyName = "admin",
                        username = "admin",
                        email = "admin@admin.com",
                        password = pwHash,
                        Salt = salt
                    });


                // add intial data (seed data)
                _context.Movies.Add(
                    new Movie
                    {
                        title = "Tenet",
                        identifier = "Tenet",
                        rating = 4,
                        genre = "Action / SciFi",
                        duration = 150,
                        releaseDate = "2020, 7, 12",
                        streamingPlatform = "Netflix",
                        streamingLink = "https://www.netflix.com/ch/title/81198930?source=35",
                        trailerLink = "https://www.youtube.com/embed/LdOM0x0XDMo?si=fMw8xPQs23nz_FOI",

                    });
                _context.Movies.Add(
                    new Movie
                    {
                        title = "Iron Man",
                        identifier = "Iron_Man",
                        rating = 4,
                        genre = "Action / Adventure",
                        duration = 126,
                        releaseDate = "2008, 3, 30",
                        streamingPlatform = "Disney+",
                        streamingLink = "https://www.disneyplus.com/de-de/movies/iron-man/6aM2a8mZATiu",
                        trailerLink = "https://www.youtube.com/embed/jK2VROKKTSQ?si=g7-3Sbst4LGnk5pR",
                    });
                _context.Movies.Add(
                    new Movie
                    {
                        title = "The Batman",
                        identifier = "TheBatman",
                        rating = 5,
                        genre = "Action / Crime",
                        duration = 176,
                        releaseDate = "2022, 2, 2",
                        streamingPlatform = null,
                        streamingLink = null,
                        trailerLink = "https://www.youtube.com/embed/mqqft2x_Aa4?si=OSn_6Y7nSvwnK3oC",
                    });
                _context.Movies.Add(
                    new Movie
                    {
                        title = "Inception",
                        identifier = "Inception",
                        rating = 5,
                        genre = "Action / Adventure / SciFi",
                        duration = 148,
                        releaseDate = "2010, 6, 8",
                        streamingPlatform = "Netflix",
                        streamingLink = "https://www.netflix.com/watch/70131314?source=35",
                        trailerLink = "https://www.youtube.com/embed/YoHD9XEInc0?si=9jz5nrDS87MGmAwW",
                    });
                _context.Movies.Add(
                    new Movie
                    {
                        title = "Now You See Me",
                        identifier = "NowYouSeeMe",
                        rating = 4,
                        genre = "Crime / Mystery / Thriller",
                        duration = 115,
                        releaseDate = "2013, 5, 31",
                        streamingPlatform = null,
                        streamingLink = null,
                        trailerLink = "https://www.youtube.com/embed/p-pVxwaFuBs?si=k3YiIacHj-nwvjF9",
                    });
                _context.Movies.Add(
                    new Movie
                    {
                        title = "Madame Web",
                        identifier = "madame_Web",
                        rating = 0,
                        genre = "SciFi - Action",
                        duration = 0,
                        releaseDate = "2024, 02, 14",
                        streamingPlatform = null,
                        streamingLink = null,
                        trailerLink = "https://www.youtube.com/embed/s_76M4c4LTo?si=kaPigKyjcCsFpHD4"
                    });
                _context.Movies.Add(
                    new Movie
                    {
                        title = "GODZILLA X KONG: The New Empire",
                        identifier = "gozilla_x_kong_the_new_empire",
                        rating = 0,
                        genre = "Action - Fantasy",
                        duration = 0,
                        releaseDate = "2024, 04, 12",
                        streamingPlatform = null,
                        streamingLink = null,
                        trailerLink = "https://www.youtube.com/embed/wcGqrBTxNmg?si=CzT8Ri1xVw7Mg_z-"
                    });








                // store everything to database
                _context.SaveChanges();
            }
        }
    }
}

