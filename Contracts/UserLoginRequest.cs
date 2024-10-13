using System.ComponentModel.DataAnnotations;

public class UserLoginRequest
{
    [MaxLength(256)]
    public string Username { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MinLength(8)]
    public string Password { get; set; }
}