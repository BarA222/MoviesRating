using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieApi.Models;
using MovieApi.Repositories;

namespace MovieApi.Controllers
{
    [Route("[controller]")]
    public class MovieRatingsController : ControllerBase
    {
       private readonly MoviesRepo _moviesRepo;

        public MovieRatingsController(MoviesRepo moviesRepo)
        {
            _moviesRepo = moviesRepo;
        }

        // POST: api/movieratings/rate/5
        [HttpPost("rate/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task RateMovie([FromRoute]Guid id, [FromBody] Movie movie)
        {
            await _moviesRepo.RateMovieAsync(id, movie.Rating);
        }
    }
}