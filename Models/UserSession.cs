namespace EventEase.Models;

/// <summary>
/// Tracks the current user's session across pages.
/// </summary>
public class UserSession
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsLoggedIn { get; set; } = false;
    public DateTime SessionStarted { get; set; } = DateTime.UtcNow;
    public List<int> ViewedEventIds { get; set; } = new();
    public List<int> RegisteredEventIds { get; set; } = new();
}
