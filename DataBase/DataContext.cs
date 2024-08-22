using Microsoft.EntityFrameworkCore;
using MyWebAPI.Models;

namespace MyWebAPI.DataBase
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
            modelBuilder.Entity<User>().HasIndex(s => s.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
