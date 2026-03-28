using Microsoft.EntityFrameworkCore;
using ActorService.Data;
using ActorService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ActorService.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly ActorDbContext _context;

        public ActorRepository(ActorDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            return await _context.Actors.ToListAsync();
        }

        public async Task<Actor> GetByIdAsync(int id)
        {
            return await _context.Actors.FindAsync(id);
        }

        public async Task AddAsync(Actor actor)
        {
            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Actor actor)
        {
            _context.Entry(actor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor != null)
            {
                _context.Actors.Remove(actor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ActorExistsAsync(int id)
        {
            return await _context.Actors.AnyAsync(e => e.Id == id);
        }
    }
}