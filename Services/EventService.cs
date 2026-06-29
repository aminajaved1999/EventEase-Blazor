using EventEase.Models;

namespace EventEase.Services;

/// <summary>
/// Provides event data and CRUD operations.
/// Uses an in-memory mock dataset (replace with API calls in production).
/// </summary>
public class EventService
{
    private readonly List<Event> _events;

    public EventService()
    {
        // Seed mock data — representative of what would come from an API/database
        _events = new List<Event>
        {
            new Event
            {
                Id = 1,
                Name = "Annual Tech Summit 2025",
                Date = new DateTime(2025, 9, 15),
                Location = "Grand Hyatt, New York",
                Description = "A premier gathering of technology leaders discussing innovation, AI, and digital transformation.",
                Category = "Corporate",
                Capacity = 500,
                ImageUrl = "https://images.unsplash.com/photo-1540575467063-178a50c2df87?w=600"
            },
            new Event
            {
                Id = 2,
                Name = "Startup Networking Mixer",
                Date = new DateTime(2025, 10, 3),
                Location = "WeWork HQ, San Francisco",
                Description = "Connect with founders, investors, and mentors shaping the next wave of startups.",
                Category = "Networking",
                Capacity = 150,
                ImageUrl = "https://images.unsplash.com/photo-1511795409834-ef04bbd61622?w=600"
            },
            new Event
            {
                Id = 3,
                Name = "Corporate Gala Dinner",
                Date = new DateTime(2025, 11, 20),
                Location = "The Ritz-Carlton, Chicago",
                Description = "An elegant evening celebrating milestones, partnerships, and achievements.",
                Category = "Social",
                Capacity = 200,
                ImageUrl = "https://images.unsplash.com/photo-1519167758481-83f550bb49b3?w=600"
            },
            new Event
            {
                Id = 4,
                Name = "Leadership & Innovation Workshop",
                Date = new DateTime(2025, 12, 5),
                Location = "Marriott Downtown, Boston",
                Description = "An intensive workshop on modern leadership principles and fostering innovation at scale.",
                Category = "Workshop",
                Capacity = 80,
                ImageUrl = "https://images.unsplash.com/photo-1524178232363-1fb2b075b655?w=600"
            },
            new Event
            {
                Id = 5,
                Name = "Product Launch: Horizon X",
                Date = new DateTime(2026, 1, 10),
                Location = "Convention Center, Austin",
                Description = "Be the first to experience Horizon X — a product set to redefine its category.",
                Category = "Corporate",
                Capacity = 1000,
                ImageUrl = "https://images.unsplash.com/photo-1505373877841-8d25f7d46678?w=600"
            },
            new Event
            {
                Id = 6,
                Name = "Women in Business Forum",
                Date = new DateTime(2026, 2, 14),
                Location = "Hilton Midtown, Seattle",
                Description = "Empowering women in business with keynotes, panels, and peer mentoring sessions.",
                Category = "Networking",
                Capacity = 300,
                ImageUrl = "https://images.unsplash.com/photo-1591115765373-5207764f72e7?w=600"
            }
        };
    }

    /// <summary>Returns all events, optionally filtered by category or search term.</summary>
    public List<Event> GetEvents(string? searchTerm = null, string? category = null)
    {
        var query = _events.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var lower = searchTerm.ToLower();
            query = query.Where(e =>
                e.Name.ToLower().Contains(lower) ||
                e.Location.ToLower().Contains(lower) ||
                e.Description.ToLower().Contains(lower));
        }

        if (!string.IsNullOrWhiteSpace(category) && category != "All")
            query = query.Where(e => e.Category == category);

        return query.OrderBy(e => e.Date).ToList();
    }

    /// <summary>Returns a single event by ID, or null if not found.</summary>
    public Event? GetEventById(int id) =>
        _events.FirstOrDefault(e => e.Id == id);

    /// <summary>Returns all unique categories.</summary>
    public List<string> GetCategories() =>
        _events.Select(e => e.Category).Distinct().OrderBy(c => c).ToList();
}
