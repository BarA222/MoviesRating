using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi.Repositories
{
    public class MoviesRepo
    {
        private readonly MovieContext _context;

        public MoviesRepo(MovieContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie> GetMovieAsync(Guid id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                throw new Exception("Movie not found");
            }

            return movie;
        }

        public async Task<Guid> CreateMovieAsync(string title, string genre, int year, double rating)
        {
            var movieExist = await _context.Movies
                .Where(x => x.Title == title)
                .FirstOrDefaultAsync();

            if(movieExist != null) throw new Exception("Movie already exist");


            var newMovie = new Movie
            {
                Id = Guid.NewGuid(),
                Genre = genre,
                Rating = rating,
                Title = title,
                Year = year,
            };

            _context.Movies.Add(newMovie);
            await _context.SaveChangesAsync();

            return newMovie.Id;
        }

        public async Task UpdateMovieAsync(Guid id, string title, string genre, int year, double rating)
        {
               var movie = await _context.Movies
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (movie == null) throw new Exception("Movie is not found");

            movie.Genre = genre;
            movie.Rating = rating;
            movie.Title = title;
            movie.Year = year;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync(Guid id)
        {
                var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                throw new Exception("Movie is not found");
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public async Task RateMovieAsync(Guid id, double rating)
        {
            var movie = await _context.Movies.FindAsync(id);
            
            if (movie == null) throw new Exception("Movie not foumd");

            movie.Rating = rating;
            await _context.SaveChangesAsync();
        }

    }
}