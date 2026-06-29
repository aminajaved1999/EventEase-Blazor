# EventEase

Blazor WebAssembly event-management single-page app targeting .NET 8 (see `README.md`).

## Cursor Cloud specific instructions

- This is a single-project Blazor WebAssembly app (`EventEase.csproj`). There is no separate test project, so there are no automated tests to run; the build is the primary correctness check.
- The .NET 8 SDK is preinstalled in the VM snapshot and `dotnet` is on the global PATH. The update script only runs `dotnet restore`.
- Standard commands (run from the repo root):
  - Build / compile check: `dotnet build`
  - Run dev server: `dotnet run`
- `dotnet run` defaults to the ports in `Properties/launchSettings.json` (`https://localhost:51894;http://localhost:51895`). For browser-based testing in the cloud VM, bind to a plain HTTP port to avoid dev-cert trust issues, e.g. `dotnet run --urls "http://0.0.0.0:5080"`, then open `http://localhost:5080/`.
- Being a WASM app, the first page load fetches the .NET runtime and can take several seconds before the SPA replaces the "Loading EventEase…" spinner.
- All state (events, registrations, session) is in-memory mock data in `Services/`; there is no database or external dependency, and data resets on every reload.
- `Shared/RegistrationForm.razor` emits a pre-existing harmless `CS0414` warning (unused `_attemptedSubmit` field); it does not affect the build.
