using DevConnect.Shared.Models;

namespace DevConnect.Api.Services.Interfaces
{
    public interface IArticleService
    {
        Task<List<Article>> GetAllAsync();
        Task<Article?> GetByIdAsync(int id);
        Task<Article> CreateAsync(Article article);
        Task<bool> UpdateAsync(int id, Article article);
        Task<bool> DeleteAsync(int id);
    }
}
