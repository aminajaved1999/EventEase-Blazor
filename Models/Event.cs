using System.ComponentModel.DataAnnotations;

namespace EventEase.Models;

/// <summary>
/// Represents a corporate or social event managed in EventEase.
/// </summary>
public class Event
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Event name is required.")]
    [StringLength(100, ErrorMessage = "Event name cannot exceed 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Date is required.")]
    public DateTime Date { get; set; } = DateTime.Today.AddDays(7);

    [Required(ErrorMessage = "Location is required.")]
    [StringLength(200, ErrorMessage = "Location cannot exceed 200 characters.")]
    public string Location { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = "Corporate";

    public int Capacity { get; set; } = 100;

    public string ImageUrl { get; set; } = "/images/default-event.jpg";
}
