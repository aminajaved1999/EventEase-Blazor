using System.ComponentModel.DataAnnotations;

namespace EventEase.Models;

/// <summary>
/// Represents a user's registration for an event.
/// </summary>
public class Registration
{
    public int Id { get; set; }

    public int EventId { get; set; }

    [Required(ErrorMessage = "Full name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required.")]
    [Phone(ErrorMessage = "Please enter a valid phone number.")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select a ticket type.")]
    public string TicketType { get; set; } = "Standard";

    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

    public bool IsConfirmed { get; set; } = false;
}
