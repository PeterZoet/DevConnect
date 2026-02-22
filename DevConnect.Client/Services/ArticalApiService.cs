using DevConnect.Shared.Models;
using System.Net.Http.Json;

namespace DevConnect.Client.Services;

public class ArticleApiService
{
    private readonly HttpClient _http;

    public ArticleApiService(HttpClient http)
    {
        _http = http;
    }

    // GET all articles
    public async Task<List<Article>> GetAllAsync()
    {
        // Use internal Docker network URL
        return await _http.GetFromJsonAsync<List<Article>>("api/articles") ?? new List<Article>();
    }

    // GET article by id
    public async Task<Article?> GetByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<Article>($"api/articles/{id}");
    }

    // POST create
    public async Task<Article> CreateAsync(Article article)
    {
        var response = await _http.PostAsJsonAsync("api/articles", article);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Article>()!;
    }

    // PUT update
    public async Task<bool> UpdateAsync(Article article)
    {
        var response = await _http.PutAsJsonAsync($"api/articles/{article.Id}", article);
        return response.IsSuccessStatusCode;
    }

    // DELETE
    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/articles/{id}");
        return response.IsSuccessStatusCode;
    }
}