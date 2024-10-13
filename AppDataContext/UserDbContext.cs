using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TodoAPIDotNet.Models;

namespace TodoAPIDotNet.AppDataContext
{
    public class UserDbContext : DbContext
    {
        private readonly DbSettings _dbSettings;

        public UserDbContext(IOptions<DbSettings> options)
        {
            _dbSettings = options.Value;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbSettings.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasMany<Todo>(u => u.Todos)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique(true);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(true);
        }
    }    
}