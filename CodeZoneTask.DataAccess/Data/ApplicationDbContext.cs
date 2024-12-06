using CodeZoneTask.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeZoneTask.DataAccess
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Store> Stores { get; set; }
    }
}
