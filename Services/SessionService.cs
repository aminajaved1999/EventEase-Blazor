using EventEase.Models;

namespace EventEase.Services;

/// <summary>
/// Singleton service that manages user session state across the application.
/// Implements the observer pattern via Action so components can subscribe to changes.
/// </summary>
public class SessionService
{
    public UserSession CurrentSession { get; private set; } = new();

    /// <summary>
    /// Subscribe to this event to get notified when session state changes.
    /// Components call StateHasChanged() in their handler.
    /// </summary>
    public event Action? OnSessionChanged;

    public bool IsLoggedIn => CurrentSession.IsLoggedIn;

    /// <summary>Logs the user in and starts their session.</summary>
    public void Login(string userName, string email)
    {
        if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Username and email are required to log in.");

        CurrentSession = new UserSession
        {
            UserName = userName,
            Email = email,
            IsLoggedIn = true,
            SessionStarted = DateTime.UtcNow
        };

        NotifyStateChanged();
    }

    /// <summary>Logs the user out and clears the session.</summary>
    public void Logout()
    {
        CurrentSession = new UserSession();
        NotifyStateChanged();
    }

    /// <summary>Records that the user viewed an event (for analytics/history).</summary>
    public void TrackEventView(int eventId)
    {
        if (!CurrentSession.ViewedEventIds.Contains(eventId))
        {
            CurrentSession.ViewedEventIds.Add(eventId);
            NotifyStateChanged();
        }
    }

    /// <summary>Records that the user registered for an event.</summary>
    public void TrackRegistration(int eventId)
    {
        if (!CurrentSession.RegisteredEventIds.Contains(eventId))
        {
            CurrentSession.RegisteredEventIds.Add(eventId);
            NotifyStateChanged();
        }
    }

    public bool HasRegisteredFor(int eventId) =>
        CurrentSession.RegisteredEventIds.Contains(eventId);

    private void NotifyStateChanged() => OnSessionChanged?.Invoke();
}
