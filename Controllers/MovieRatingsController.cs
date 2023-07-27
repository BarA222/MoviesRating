using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [Route("[controller]")]
    public class MovieRatingsController : ControllerBase
    {
       private readonly MovieContext _context;

        public MovieRatingsController(MovieContext context)
        {
            _context = context;
        }

        // POST: api/movieratings/rate/5
        [HttpPost("rate/{id}")]
        public async Task<IActionResult> RateMovie(int id, [FromBody] double rating)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            movie.Rating = rating;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}