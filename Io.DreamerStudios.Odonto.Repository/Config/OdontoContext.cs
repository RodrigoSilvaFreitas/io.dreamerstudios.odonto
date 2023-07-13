using Io.DreamerStudios.Odonto.Core.DTO;
using Microsoft.EntityFrameworkCore;

namespace Io.DreamerStudios.Odonto.Repository.Config
{
    public class OdontoContext : DbContext
    {
        public OdontoContext(DbContextOptions<OdontoContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }

    }
}
