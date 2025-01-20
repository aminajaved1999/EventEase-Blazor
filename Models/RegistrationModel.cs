using System.ComponentModel.DataAnnotations;

namespace EventEase.Models
{

public class RegistrationModel
{
    [Required(ErrorMessage = "Username is required.")]
    public required string UserName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
    public required string Password { get; set; }
}

}
