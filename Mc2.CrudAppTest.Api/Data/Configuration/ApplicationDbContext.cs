

using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudAppTest.Api.Data.Configuration
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Customer> Customers => Set<Customer>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasIndex(e => new { e.FirstName, e.LastName, e.DateOfBirth }, "Unique_First_LastName_Birth").IsUnique();
            modelBuilder.Entity<Customer>().HasIndex(e => e.Email).IsUnique();
        }

    }
}
