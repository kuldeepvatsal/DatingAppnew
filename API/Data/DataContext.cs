using API.Entites;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AppUser> AppUsers { get; set; }
        //change the name of table
        // protected override void OnModelCreating(ModelBuilder  builder)
        // {
        //   builder.Entity<AppUser>().ToTable("AppUser");
        // }
    }
}