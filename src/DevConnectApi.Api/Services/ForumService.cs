using DevConnect.Data;
using DevConnect.DTOs.Forums;
using DevConnect.Models;
using DevConnect.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevConnect.Services;

public class ForumService(AppDbContext context) : IForumService
{
    public async Task<IReadOnlyList<ThreadResponseDto>> GetThreadsAsync()
    {
        return await context.ForumThreads
            .Include(t => t.Comments)
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new ThreadResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Content = t.Content,
                CategoryId = t.CategoryId,
                AuthorId = t.AuthorId,
                CreatedAt = t.CreatedAt,
                CommentCount = t.Comments.Count
            })
            .ToListAsync();
    }

    public async Task<ThreadResponseDto> CreateThreadAsync(CreateThreadDto dto)
    {
        var thread = new ForumThread
        {
            Title = dto.Title,
            Content = dto.Content,
            CategoryId = dto.CategoryId,
            AuthorId = dto.AuthorId
        };

        context.ForumThreads.Add(thread);
        await context.SaveChangesAsync();

        return new ThreadResponseDto
        {
            Id = thread.Id,
            Title = thread.Title,
            Content = thread.Content,
            CategoryId = thread.CategoryId,
            AuthorId = thread.AuthorId,
            CreatedAt = thread.CreatedAt,
            CommentCount = 0
        };
    }

    public async Task<bool> AddCommentAsync(Guid threadId, CreateComentDto dto)
    {
        var threadExists = await context.ForumThreads.AnyAsync(t => t.Id == threadId);
        if (!threadExists)
        {
            return false;
        }

        context.ForumComments.Add(new ForumComment
        {
            ThreadId = threadId,
            AuthorId = dto.AuthorId,
            Content = dto.Content
        });

        await context.SaveChangesAsync();
        return true;
    }
}
