using DevConnect.Data;
using DevConnect.DTOs.Articles;
using DevConnect.Models;
using DevConnect.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevConnect.Services;

public class ArticleService(AppDbContext context) : IArticleService
{
    public async Task<IReadOnlyList<ArticleResponseDto>> GetAllAsync()
    {
        return await context.Articles
            .OrderByDescending(a => a.CreatedAt)
            .Select(Map)
            .ToListAsync();
    }

    public async Task<ArticleResponseDto?> GetByIdAsync(Guid id)
    {
        return await context.Articles
            .Where(a => a.Id == id)
            .Select(Map)
            .FirstOrDefaultAsync();
    }

    public async Task<ArticleResponseDto> CreateAsync(CreateArticleDto dto)
    {
        var article = new Article
        {
            Title = dto.Title,
            Content = dto.Content,
            CategoryId = dto.CategoryId,
            AuthorId = dto.AuthorId
        };

        context.Articles.Add(article);
        await context.SaveChangesAsync();

        return ToDto(article);
    }

    public async Task<ArticleResponseDto?> UpdateAsync(Guid id, UpdateArticleDto dto)
    {
        var article = await context.Articles.FindAsync(id);
        if (article is null)
        {
            return null;
        }

        article.Title = dto.Title ?? article.Title;
        article.Content = dto.Content ?? article.Content;
        article.CategoryId = dto.CategoryId ?? article.CategoryId;
        article.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
        return ToDto(article);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var article = await context.Articles.FindAsync(id);
        if (article is null)
        {
            return false;
        }

        context.Articles.Remove(article);
        await context.SaveChangesAsync();
        return true;
    }

    private static ArticleResponseDto ToDto(Article article) => new()
    {
        Id = article.Id,
        Title = article.Title,
        Content = article.Content,
        CategoryId = article.CategoryId,
        AuthorId = article.AuthorId,
        CreatedAt = article.CreatedAt,
        UpdatedAt = article.UpdatedAt
    };

    private static readonly Func<Article, ArticleResponseDto> Map = a => new ArticleResponseDto
    {
        Id = a.Id,
        Title = a.Title,
        Content = a.Content,
        CategoryId = a.CategoryId,
        AuthorId = a.AuthorId,
        CreatedAt = a.CreatedAt,
        UpdatedAt = a.UpdatedAt
    };
}
