using DevConnect.Data;
using DevConnect.Middleware;
using DevConnect.Services;
using DevConnect.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLogging();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
