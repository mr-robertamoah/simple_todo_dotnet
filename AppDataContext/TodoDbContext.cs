using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TodoAPIDotNet.Models;

namespace TodoAPIDotNet.AppDataContext
{
    public class TodoDbContext : DbContext
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
                .ToTable("TodoAPI")
                .HasKey(t => t.Id);
        }
    }
}