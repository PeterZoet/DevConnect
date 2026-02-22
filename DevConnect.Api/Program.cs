using DevConnect.Api.Services;
using DevConnect.Api.Services.Interfaces;
using System.Reflection;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

// Detect if running in Docker
var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
if (isDocker)
{
    // Bind to all interfaces inside container
    builder.WebHost.UseUrls("http://+:5000");
}

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "DevConnect API", Version = "v1" });

    // Load XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    // Group endpoints by controller name
    c.TagActionsBy(api => new[] { api.GroupName ?? api.ActionDescriptor.RouteValues["controller"] });

    // Enable annotations
    c.EnableAnnotations();
});

// Register services
builder.Services.AddSingleton<IArticleService, ArticleService>();

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// Use CORS
app.UseCors();

// Swagger middleware (development only)
if (app.Environment.IsDevelopment() || isDocker)
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevConnect API V1");
        c.RoutePrefix = string.Empty; // Open at root
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
        c.DefaultModelsExpandDepth(1);
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Print URLs on startup
Console.WriteLine("=====================================");
Console.WriteLine($"API Swagger: {(isDocker ? "http://localhost:5265 (host)" : "https://localhost:5265")}");
Console.WriteLine("=====================================");

app.Run();