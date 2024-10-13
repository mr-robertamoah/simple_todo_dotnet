using System.ComponentModel.DataAnnotations;

public class UserRegisterRequest
{
    [Required]
    [MaxLength(256)]
    public string Username { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [MaxLength(256)]
    public string Name { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password Confirmation")]
    [Compare("Password", ErrorMessage =  "The password and confirm password do not match")]

    public string PasswordConfirmation { get; set; }
}