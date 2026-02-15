using System.Reflection;
using DevConnect.Data;
using DevConnect.Middleware;
using DevConnect.Services;
using DevConnect.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

const string BlazorClientCorsPolicy = "BlazorClient";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DevConnect API",
        Version = "v1",
        Description = "REST API for DevConnect articles and forum discussions."
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(BlazorClientCorsPolicy, policy =>
    {
        policy
            .WithOrigins("https://localhost:7062", "http://localhost:5062")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("DevConnectDb"));

builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IForumService, ForumService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedData.Initialize(context);
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "DevConnect API v1");
    options.RoutePrefix = "swagger";
});

app.UseRequestLogging();
app.UseHttpsRedirection();
app.UseCors(BlazorClientCorsPolicy);
app.MapControllers();

app.Run();
