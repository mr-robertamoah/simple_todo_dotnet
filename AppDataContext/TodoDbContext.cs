using Microsoft.AspNetCore.Identity;
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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Todo>()
                .ToTable("Todos")
                .HasKey(t => t.Id);

            modelBuilder.Entity<Todo>()
                .HasOne<User>(t => t.User)
                .WithMany(u => u.Todos)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<User>()
                .HasMany<Todo>(u => u.Todos)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique(true);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(true);

            var hasher = new PasswordHasher<User>();

            // Seed data
            var user = new User
            {
                Id = "1",
                UserName = "mr_robertamoah",
                NormalizedUserName = "MR_ROBERTAMOAH",
                Email = "mr_robertamoah@example.com",
                NormalizedEmail = "MR_ROBERTAMOAH@EXAMPLE.COM",
                IsAdmin = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Hash the password
            user.PasswordHash = hasher.HashPassword(user, "password");

            modelBuilder.Entity<User>().HasData(user);

            modelBuilder.Entity<Todo>().HasData(new Todo
            {
                Id = Guid.NewGuid(),
                UserId = "1",
                Title = "Default Todo",
                Description = "This is a default todo item.",
                IsComplete = false
            });
        }
    }
}