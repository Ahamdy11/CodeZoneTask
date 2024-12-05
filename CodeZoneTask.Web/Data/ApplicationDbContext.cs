using CodeZoneTask.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeZoneTask.Web.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Store> Stores { get; set; }
    }
}
