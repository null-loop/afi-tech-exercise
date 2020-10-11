using Microsoft.EntityFrameworkCore;

namespace AFIExercise.Data
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
        {

        }

        public DbSet<CustomerRegistration> CustomerRegistrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerRegistration>().ToTable("CustomerRegistrations");
        }
    }
}
