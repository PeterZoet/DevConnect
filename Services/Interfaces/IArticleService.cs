using DevConnect.DTOs.Articles;

namespace DevConnect.Services.Interfaces;

public interface IArticleService
{
    Task<IReadOnlyList<ArticleResponseDto>> GetAllAsync();
    Task<ArticleResponseDto?> GetByIdAsync(Guid id);
    Task<ArticleResponseDto> CreateAsync(CreateArticleDto dto);
    Task<ArticleResponseDto?> UpdateAsync(Guid id, UpdateArticleDto dto);
    Task<bool> DeleteAsync(Guid id);
}
