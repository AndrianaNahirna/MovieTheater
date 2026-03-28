using Microsoft.AspNetCore.Mvc;
using MovieService.Models;
using MovieService.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieService.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesApiController : ControllerBase
    {
        private readonly IMovieRepository _repository;

        // Підключаємо наш Репозиторій
        public MoviesApiController(IMovieRepository repository)
        {
            _repository = repository;
        }

        // GET: api/MoviesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            var movies = await _repository.GetAllAsync();
            return Ok(movies);
        }

        // GET: api/MoviesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _repository.GetByIdAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // POST: api/MoviesApi
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            await _repository.AddAsync(movie);
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        // PUT: api/MoviesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateAsync(movie);
            }
            catch
            {
                if (!await _repository.MovieExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/MoviesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (!await _repository.MovieExistsAsync(id))
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}