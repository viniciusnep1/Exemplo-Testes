using entities.entities;
using Microsoft.EntityFrameworkCore;

namespace entities
{
    public class EFAppContext : DbContext
    {
        public EFAppContext(DbContextOptions<EFAppContext> options)
          : base(options)
        { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

}
