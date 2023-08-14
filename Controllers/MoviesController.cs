using Microsoft.AspNetCore.Mvc;
using MovieApi.Models;
using MovieApi.Repositories;

namespace MovieApi.Controllers
{
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly MoviesRepo _moviesRepo;

        public MoviesController(MoviesRepo moviesRepo)
        {
            _moviesRepo = moviesRepo;
        }

        // GET: api/movies
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            return await _moviesRepo.GetMoviesAsync();
        }

        // GET: api/movies/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Movie>> GetMovie(Guid id)
        {
           return await _moviesRepo.GetMovieAsync(id);
        }

         // POST: api/movies
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Guid>> CreateMovie([FromBody] Movie movie)
        {
           return await _moviesRepo.CreateMovieAsync(movie.Title, movie.Genre, movie.Year, movie.Rating);
        }

        // PUT: api/movies/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task UpdateMovie([FromRoute]Guid id, [FromBody] Movie movie)
        {
             await _moviesRepo.UpdateMovieAsync(id, movie.Title, movie.Genre, movie.Year, movie.Rating);
        }

        // DELETE: api/movies/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task DeleteMovie(Guid id)
        {
            await _moviesRepo.DeleteMovieAsync(id);
        }
      
    }

    }
