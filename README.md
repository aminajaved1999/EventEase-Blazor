# 🎫 EventEase — Blazor WebAssembly Event Management App

> **Blazor for Front-End Development — Activities 1, 2 & 3**
> Built with C# · Blazor WebAssembly · .NET 8 · Microsoft Copilot

---

## 📋 Project Overview

EventEase is a corporate and social event management web application built with **Blazor WebAssembly**. Users can browse events, view event details, register with validated forms, and track attendance — all without leaving a single page.

This project is the capstone for the *Blazor for Front-End Development* course and covers all three activities:

| Activity | Deliverable |
|---|---|
| **Activity 1** | EventCard component, two-way data binding, routing |
| **Activity 2** | Debugging, input validation, error handling, performance |
| **Activity 3** | Registration form, session state management, attendance tracker |

---

## 🏗️ Architecture

```
EventEase/
├── Models/
│   ├── Event.cs              # Event entity with DataAnnotations
│   ├── Registration.cs       # Registration form model with validation
│   └── UserSession.cs        # Session state model
├── Services/
│   ├── EventService.cs       # Event data & filtering (mock dataset)
│   ├── SessionService.cs     # Singleton session state with Observer pattern
│   └── AttendanceService.cs  # Registration CRUD & attendance tracking
├── Shared/
│   ├── MainLayout.razor      # App shell (header, nav, footer)
│   ├── EventCard.razor       # ⭐ Activity 1 — Reusable event card component
│   └── RegistrationForm.razor # ⭐ Activity 3 — Validated registration form
├── Pages/
│   ├── Index.razor           # Home page (featured events, stats)
│   ├── Events.razor          # Event listing with search & category filters
│   ├── EventDetail.razor     # Dynamic route /events/{id}
│   ├── Register.razor        # Registration page /events/{id}/register
│   ├── Attendance.razor      # ⭐ Activity 3 — Attendance tracker dashboard
│   └── Login.razor           # User session login
├── wwwroot/
│   ├── index.html
│   ├── css/app.css
│   └── appsettings.json
├── App.razor                 # Router with 404 fallback
├── _Imports.razor
└── Program.cs                # DI registrations
```

---

## ✅ Activity 1 — EventCard Component & Routing

**Deliverables:**
- `Shared/EventCard.razor` — Reusable component with `[Parameter]` for Event data, `EventCallback<Event>` for parent notifications, and capacity bar driven by live data binding
- Two-way data binding via `@bind` on search/filter inputs (`Events.razor`)
- Routing defined with `@page` directives and dynamic segments (`/events/{Id:int}`, `/events/{Id:int}/register`)
- `App.razor` includes a `<NotFound>` fallback to handle invalid routes gracefully

**Key Blazor concepts demonstrated:**
```razor
<!-- Parameter passing (parent → child) -->
<EventCard Event="evt" AttendanceCount="@count" IsHighlighted="true" />

<!-- EventCallback (child → parent) -->
[Parameter] public EventCallback<Event> OnSelect { get; set; }

<!-- Two-way data binding -->
<input @bind="_searchTerm" @bind:event="oninput" @oninput="ApplyFilters" />

<!-- Dynamic routing -->
@page "/events/{Id:int}"
[Parameter] public int Id { get; set; }
```

---

## ✅ Activity 2 — Debugging & Optimization

Issues identified and resolved:

### 1. Null Reference Exceptions
All route parameters are validated before use — `EventService.GetEventById()` returns `Event?` and every consumer null-checks before rendering.

```csharp
// EventDetail.razor
@if (_event is null)
{
    <div class="not-found"><h2>Event Not Found</h2>...</div>
}
```

### 2. Data Binding & Input Validation
`RegistrationForm.razor` uses `<EditForm>` with `<DataAnnotationsValidator />` and `<ValidationSummary />`. Invalid submits are caught by `OnInvalidSubmit`, and duplicate email registrations throw a descriptive `InvalidOperationException`.

```csharp
private void HandleInvalidSubmit()
{
    _errorMessage = "Please correct the errors below before submitting.";
}
```

### 3. Invalid Route Handling
`App.razor` wraps routing in `<NotFound>` to display a user-friendly 404 page instead of crashing:

```razor
<NotFound>
    <LayoutView Layout="@typeof(MainLayout)">
        <div class="not-found">
            <h2>404 — Page Not Found</h2>
            <a href="/" class="btn-primary">Back to Home</a>
        </div>
    </LayoutView>
</NotFound>
```

### 4. Performance Optimization
- Filtering logic runs in `EventService.GetEvents()` using LINQ (server-side-friendly)
- `StateHasChanged()` is called via `InvokeAsync()` in service subscriptions (thread-safe)
- Unnecessary re-renders minimized: components only subscribe to the events they need
- `ShouldRender` pattern available on `EventCard` for future optimization

---

## ✅ Activity 3 — Advanced Features

### Registration Form with Validation
`Shared/RegistrationForm.razor`:
- Full DataAnnotations validation (`[Required]`, `[EmailAddress]`, `[Phone]`, `[StringLength]`)
- Pre-fills name/email from session when user is signed in
- Async submission (simulates API call with `Task.Delay`)
- Duplicate registration detection
- Graceful success state with confirmation ID

### User Session State Management
`Services/SessionService.cs`:
- **Singleton** service available application-wide via DI
- Implements the **Observer pattern**: components subscribe to `OnSessionChanged` and call `StateHasChanged()` on update
- Tracks `ViewedEventIds` and `RegisteredEventIds` across pages
- `MainLayout.razor` reacts to session changes to show/hide the nav greeting in real-time

```csharp
// Subscribe in component
protected override void OnInitialized() =>
    SessionService.OnSessionChanged += OnSessionStateChanged;

// Notify on change (thread-safe)
private void OnSessionStateChanged() =>
    InvokeAsync(StateHasChanged);

// Always unsubscribe (IDisposable)
public void Dispose() =>
    SessionService.OnSessionChanged -= OnSessionStateChanged;
```

### Attendance Tracker
`Pages/Attendance.razor`:
- Real-time dashboard that reacts to new registrations via `AttendanceService.OnAttendanceChanged`
- Per-event breakdown table with fill percentage and visual mini-bar
- Full registration log with status badges
- Summary stats: total registrations, events with attendees, average capacity fill

---

## 🤖 How Microsoft Copilot Assisted

### Activity 1 — Code Generation
Copilot generated the initial `EventCard.razor` component structure and suggested the correct Blazor syntax for:
- `[Parameter]` and `[EditorRequired]` attributes
- `EventCallback<T>` pattern for child→parent communication
- `@bind` and `@oninput` directives for real-time search binding
- Route template syntax (`@page "/events/{Id:int}"`)

### Activity 2 — Debugging & Optimization
Copilot identified:
- Missing null checks on nullable route-bound model (`Event?`)
- The correct pattern for `OnInvalidSubmit` to surface validation state
- Using `InvokeAsync(StateHasChanged)` instead of direct `StateHasChanged()` in async callbacks (avoids threading issues in WASM)
- Suggested `IDisposable` on all components subscribing to service events to prevent memory leaks

### Activity 3 — Advanced Features
Copilot suggested:
- The `Action` event-based observer pattern for `SessionService` instead of polling
- `[EditorRequired]` on parameters that must be set by the parent
- Pre-populating registration form fields from session state in `OnParametersSet`
- The `record` type for the `EventRow` projection in `Attendance.razor` to reduce boilerplate

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio Code](https://code.visualstudio.com/) + C# extension

### Run Locally
```bash
git clone https://github.com/YOUR_USERNAME/EventEase.git
cd EventEase
dotnet restore
dotnet run
```

Open `https://localhost:5001` in your browser.

---

## 📦 Technologies Used

| Technology | Purpose |
|---|---|
| Blazor WebAssembly | SPA framework — runs C# in the browser via WASM |
| .NET 8 | Runtime & SDK |
| DataAnnotations | Form validation (`[Required]`, `[EmailAddress]`, etc.) |
| Dependency Injection | Singleton services for state management |
| SignalR-free | Pure WASM — no server round-trips after initial load |
| CSS (vanilla) | Custom design system — no external UI library needed |

---

## 📄 License
MIT — free to use and adapt for educational purposes.
