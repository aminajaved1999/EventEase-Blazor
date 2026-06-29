using EventEase.Models;

namespace EventEase.Services;

/// <summary>
/// Manages event registrations and attendance tracking.
/// </summary>
public class AttendanceService
{
    private readonly List<Registration> _registrations = new();
    private int _nextId = 1;

    public event Action? OnAttendanceChanged;

    /// <summary>Returns all registrations for a specific event.</summary>
    public List<Registration> GetRegistrationsForEvent(int eventId) =>
        _registrations.Where(r => r.EventId == eventId).ToList();

    /// <summary>Returns all registrations (for admin view).</summary>
    public List<Registration> GetAllRegistrations() =>
        _registrations.OrderByDescending(r => r.RegisteredAt).ToList();

    /// <summary>Registers a user for an event. Returns the created registration.</summary>
    public Registration Register(Registration registration)
    {
        // Validate no duplicate email per event
        var duplicate = _registrations.Any(r =>
            r.EventId == registration.EventId &&
            r.Email.Equals(registration.Email, StringComparison.OrdinalIgnoreCase));

        if (duplicate)
            throw new InvalidOperationException(
                "This email address is already registered for this event.");

        registration.Id = _nextId++;
        registration.RegisteredAt = DateTime.UtcNow;
        registration.IsConfirmed = true;

        _registrations.Add(registration);
        OnAttendanceChanged?.Invoke();
        return registration;
    }

    /// <summary>Returns the count of registrations for a given event.</summary>
    public int GetAttendanceCount(int eventId) =>
        _registrations.Count(r => r.EventId == eventId);

    /// <summary>Checks if an email is already registered for an event.</summary>
    public bool IsAlreadyRegistered(int eventId, string email) =>
        _registrations.Any(r =>
            r.EventId == eventId &&
            r.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

    /// <summary>Cancels a registration by ID.</summary>
    public bool CancelRegistration(int registrationId)
    {
        var reg = _registrations.FirstOrDefault(r => r.Id == registrationId);
        if (reg is null) return false;

        _registrations.Remove(reg);
        OnAttendanceChanged?.Invoke();
        return true;
    }
}
