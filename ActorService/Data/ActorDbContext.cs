using Microsoft.EntityFrameworkCore;
using ActorService.Models;

namespace ActorService.Data
{
    public class ActorDbContext : DbContext
    {
        public ActorDbContext(DbContextOptions<ActorDbContext> options) : base(options)
        {
        }
        public DbSet<Actor> Actors { get; set; }
    }
}