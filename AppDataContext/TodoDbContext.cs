using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TodoAPIDotNet.Models;

namespace TodoAPIDotNet.AppDataContext
{
    public class TodoDbContext : IdentityDbContext<User>
    {
        private readonly DbSettings _dbSettings;
        public TodoDbContext(IOptions<DbSettings> options)
        {
            _dbSettings = options.Value;
        }

        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbSettings.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>()
                .ToTable("Todos")
                .HasKey(t => t.Id);

            modelBuilder.Entity<Todo>()
                .HasOne<User>(t => t.User)
                .WithMany(u => u.Todos)
                .HasForeignKey(t => t.UserId);
        }
    }
}