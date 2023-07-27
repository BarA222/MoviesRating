using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        // GET: api/movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

         // POST: api/movies
        [HttpPost]
        public async Task<ActionResult<Movie>> CreateMovie([FromBody] Movie movie)
        {
            var movieExist = await _context.Movies
                .Where(x => x.Title == movie.Title)
                .FirstOrDefaultAsync();

            if(movieExist != null) return new ConflictResult();


            var newMovie = new Movie
            {
                Id = Guid.NewGuid(),
                Genre = movie.Genre,
                Rating = movie.Rating,
                Title = movie.Title,
                Year = movie.Year,
            };

            _context.Movies.Add(newMovie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovie), new { id = newMovie.Id }, $"Id: {newMovie.Id}" );
        }

        // PUT: api/movies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(
            [FromRoute]Guid id, [FromBody] Movie movie)
        {
                var movieExist = await _context.Movies
                .Where(x => x.Title == movie.Title)
                .FirstOrDefaultAsync();

            if(movieExist != null) return new ConflictResult();
            
           var movieResult = await _context.Movies.FindAsync(id);

            if (movieResult == null) return new NotFoundResult();

            movieResult.Genre =movie.Genre;
            movieResult.Rating = movie.Rating;
            movieResult.Title = movie.Title;
            movieResult.Year = movie.Year;

            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return Ok();
        }

      
    }

    }
