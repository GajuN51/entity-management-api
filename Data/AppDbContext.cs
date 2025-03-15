using EntityAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Entity> Entities { get; set; }
    }
}
