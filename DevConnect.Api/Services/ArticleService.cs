using DevConnect.Api.Services.Interfaces;
using DevConnect.Shared.Models;
using System.Text.Json;

namespace DevConnect.Api.Services
{
    public class ArticleService : IArticleService
    {
        private List<Article> _articles = new();

        public ArticleService()
        {
            LoadArticlesAsync();
        }

        public async void LoadArticlesAsync()
        {
            var jsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "Articles.json");
            if (!File.Exists(jsonPath))
                throw new FileNotFoundException("Articles.json not found.", jsonPath);

            var json = await File.ReadAllTextAsync(jsonPath);
            _articles = JsonSerializer.Deserialize<List<Article>>(json) ?? new List<Article>();
        }

        public Task<List<Article>> GetAllAsync() => Task.FromResult(_articles);

        public Task<Article?> GetByIdAsync(int id) => Task.FromResult(_articles.FirstOrDefault(a => a.Id == id));

        public Task<Article> CreateAsync(Article article)
        {
            article.Id = _articles.Any() ? _articles.Max(a => a.Id) + 1 : 1;
            _articles.Add(article);
            return Task.FromResult(article);
        }

        public Task<bool> UpdateAsync(int id, Article article)
        {
            var existing = _articles.FirstOrDefault(a => a.Id == id);
            if (existing == null) return Task.FromResult(false);

            existing.Title = article.Title;
            existing.Author = article.Author;
            existing.Content = article.Content;
            existing.PublishedDate = article.PublishedDate;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var existing = _articles.FirstOrDefault(a => a.Id == id);
            if (existing == null) return Task.FromResult(false);
            _articles.Remove(existing);
            return Task.FromResult(true);
        }
    }
}
