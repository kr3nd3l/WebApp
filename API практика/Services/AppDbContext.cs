using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API_практика.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Client> clients { get; set; }
        public DbSet<Country> countries { get; set; }
        public DbSet<Tour> tours { get; set; }
    }
}
