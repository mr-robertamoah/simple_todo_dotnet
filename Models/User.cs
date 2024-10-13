#nullable enable
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TodoAPIDotNet.Models
{
    public class User : IdentityUser
    {
        [Key]
        public override string Id { get; set; }
        [Required]
        [MaxLength(256)]
        public override string? UserName { get; set; }
        [MaxLength(256)]
        public string Name;
        [Required]
        [EmailAddress]
        public override string? Email { get; set; }
        [Required]
        [MinLength(8)]
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public  ICollection<Todo> Todos { get; set; }


    }
}