using MovieTheater.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTheater.Repositories
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