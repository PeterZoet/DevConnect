using DevConnect.Client.Services;
using DevConnect.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Linq;

namespace DevConnect.Client.Components.Articles
{
    public partial class ArticlesOverview
    {
        [Inject] private ArticleApiService ArticleService { get; set; } = null!;
        [Inject] private NavigationManager Navigation { get; set; } = null!;

        private List<Article> Articles = new();
        private Article? SelectedArticle;
        private bool IsDetailsVisible = false;

        private Article EditingArticle = new Article();
        private Article? ArticleToDelete;

        private int CurrentPage = 1;
        private int PageSize = 5;
        private int TotalPages => (int)Math.Ceiling((double)Articles.Count / PageSize);

        protected override async Task OnInitializedAsync()
        {
            Articles = await ArticleService.GetAllAsync();
        }

        // Create or edit via modal
        private async Task SaveArticleAsync()
        {
            if (EditingArticle.Id == 0)
            {
                var created = await ArticleService.CreateAsync(EditingArticle);
                if (created != null)
                    Articles.Add(created);
            }
            else
            {
                var success = await ArticleService.UpdateAsync(EditingArticle) != null;
                if (success)
                {
                    var existing = Articles.FirstOrDefault(a => a.Id == EditingArticle.Id);
                    if (existing != null)
                    {
                        existing.Title = EditingArticle.Title;
                        existing.Content = EditingArticle.Content;
                        existing.Author = EditingArticle.Author;
                        existing.PublishedDate = EditingArticle.PublishedDate;
                    }
                }
            }

            EditingArticle = new Article(); // reset
        }

        // Update an existing article directly
        private async Task SaveArticleAsync(Article article)
        {
            var success = await ArticleService.UpdateAsync(article) != null;
            if (success)
            {
                var existing = Articles.FirstOrDefault(a => a.Id == article.Id);
                if (existing != null)
                {
                    existing.Title = article.Title;
                    existing.Content = article.Content;
                    existing.Author = article.Author;
                    existing.PublishedDate = article.PublishedDate;
                }
            }
        }

        private async Task DeleteArticleConfirmedAsync(Article article)
        {
            await ArticleService.DeleteAsync(article.Id);

            // Remove from local list
            Articles.Remove(article);
            if (SelectedArticle == article)
                SelectedArticle = null;
            ArticleToDelete = null;
        }

        private void NavigateToArticleDetail(Article article)
        {
            // Example using a shared state service
            ArticleDetailService.CurrentArticle = article;
            Navigation.NavigateTo("/articles/detail", forceLoad: false);
        }

        // Pagination methods
        private void GoToPage(int page) => CurrentPage = page;
        private void PrevPage() { if (CurrentPage > 1) CurrentPage--; }
        private void NextPage() { if (CurrentPage < TotalPages) CurrentPage++; }
    }
}