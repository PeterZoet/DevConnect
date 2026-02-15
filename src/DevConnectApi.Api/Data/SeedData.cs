using DevConnect.Models;

namespace DevConnect.Data;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (context.Categories.Any())
        {
            return;
        }

        var categories = new List<Category>
        {
            new() { Name = "Architecture" },
            new() { Name = "Backend" },
            new() { Name = "DevOps" }
        };

        var user = new User { Name = "DevConnect Admin", Email = "admin@devconnect.local" };

        context.Categories.AddRange(categories);
        context.Users.Add(user);
        context.SaveChanges();
    }
}
