using Microsoft.AspNetCore.Mvc;
using MovieTheater.Models;
using MovieTheater.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsApiController : ControllerBase
    {
        private readonly IActorRepository _repository;

        public ActorsApiController(IActorRepository repository)
        {
            _repository = repository;
        }

        // GET: api/ActorsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> GetActors()
        {
            var actors = await _repository.GetAllAsync();
            return Ok(actors);
        }

        // GET: api/ActorsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Actor>> GetActor(int id)
        {
            var actor = await _repository.GetByIdAsync(id);

            if (actor == null)
            {
                return NotFound();
            }

            return Ok(actor);
        }

        // POST: api/ActorsApi
        [HttpPost]
        public async Task<ActionResult<Actor>> PostActor(Actor actor)
        {
            await _repository.AddAsync(actor);
            return CreatedAtAction(nameof(GetActor), new { id = actor.Id }, actor);
        }

        // PUT: api/ActorsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(int id, Actor actor)
        {
            if (id != actor.Id)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateAsync(actor);
            }
            catch
            {
                if (!await _repository.ActorExistsAsync(id))
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

        // DELETE: api/ActorsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            if (!await _repository.ActorExistsAsync(id))
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}