using DevConnect.Client.Services;
using DevConnect.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace DevConnect.Client.Components.Articles
{
    public partial class ArticleDetail
    {
        [Parameter] public Article? Item { get; set; }

        protected override void OnInitialized()
        {
            Item = ArticleDetailService.CurrentArticle;
            if (Item == null)
            {
                Navigation.NavigateTo("/articles"); // fallback
            }
        }
    }
}
