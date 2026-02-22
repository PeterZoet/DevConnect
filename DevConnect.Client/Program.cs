using DevConnect.Client.Components;
using DevConnect.Client.Services;

var builder = WebApplication.CreateBuilder(args);

// Detect if running in Docker
var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
if (isDocker)
{
    // Bind to all interfaces inside container
    builder.WebHost.UseUrls("http://+:5001");
}

// API base URL
var apiBaseUrl = isDocker
    ? "http://devconnect-api:5000/"   // Docker network service name and container port
    : "https://localhost:5265/";     // Local development fallback

// Register HttpClient and API service
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });
builder.Services.AddScoped<ArticleApiService>();

// Add Razor Components / Interactive Server
builder.Services.AddRazorComponents()
       .AddInteractiveServerComponents();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

// Print URL info
Console.WriteLine("=====================================");
Console.WriteLine($"Blazor App: {(isDocker ? "http://localhost:5000 (host)" : "https://localhost:5000")}");
Console.WriteLine("=====================================");

app.Run();