using FakingDataDatabaseSeed.Data;
using Microsoft.EntityFrameworkCore;

namespace FakingDataDatabaseSeed
{
    public class CourseManagerContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Your setup here...

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Your table seeding here...
        }
    }
}
