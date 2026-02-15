using DevConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace DevConnect.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<ArticleComment> ArticleComments => Set<ArticleComment>();
    public DbSet<ForumThread> ForumThreads => Set<ForumThread>();
    public DbSet<ForumComment> ForumComments => Set<ForumComment>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<User> Users => Set<User>();
}
