using Login_service.Models;
using Microsoft.EntityFrameworkCore;

namespace Login_service.Database
{
    public class UsersDb: DbContext
    {
        public DbSet<User> user { get; set; }
        public UsersDb(DbContextOptions db): base(db) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(u => u.name).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.email).IsUnique();
        }
    }
}
