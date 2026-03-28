using MovieService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieService.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie> GetByIdAsync(int id);
        Task AddAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(int id);
        Task<bool> MovieExistsAsync(int id);
    }
}