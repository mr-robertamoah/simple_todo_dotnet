using System.ComponentModel.DataAnnotations;

public class UserLoginRequest
{
    [MaxLength(256)]
    [AtLeastOneRequired("Email")]
    public string Username { get; set; }
    [EmailAddress]
    [AtLeastOneRequired("Username")]
    public string Email { get; set; }
    [Required]
    [MinLength(8)]
    public string Password { get; set; }
}

public class AtLeastOneRequiredAttribute : ValidationAttribute
{
    private readonly string _otherProperty;

    public AtLeastOneRequiredAttribute(string otherProperty)
    {
        _otherProperty = otherProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var otherPropertyValue = validationContext.ObjectType.GetProperty(_otherProperty)
            .GetValue(validationContext.ObjectInstance, null);

        if (value == null && otherPropertyValue == null)
        {
            return new ValidationResult($"Either {validationContext.DisplayName} or {_otherProperty} is required.");
        }

        return ValidationResult.Success;
    }
}