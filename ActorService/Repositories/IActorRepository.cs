using ActorService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ActorService.Repositories
{
    public interface IActorRepository
    {
        Task<IEnumerable<Actor>> GetAllAsync();
        Task<Actor> GetByIdAsync(int id);
        Task AddAsync(Actor actor);
        Task UpdateAsync(Actor actor);
        Task DeleteAsync(int id);
        Task<bool> ActorExistsAsync(int id);
    }
}