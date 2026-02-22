using DevConnect.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace DevConnect.Client.Components.Common
{
    public partial class Breadcrumb
    {
        [Parameter] public Article? CurrentArticle { get; set; } // optional

        private List<BreadcrumbItem> Breadcrumbs = new();

        protected override void OnInitialized()
        {
            Navigation.LocationChanged += (_, __) => UpdateBreadcrumbs();
            UpdateBreadcrumbs();
        }

        private void UpdateBreadcrumbs()
        {
            Breadcrumbs.Clear();

            var uri = Navigation.Uri;
            var baseUri = Navigation.BaseUri;

            // Normalize route
            var relativePath = uri.Replace(baseUri, "").TrimEnd('/').ToLowerInvariant();

            // Root breadcrumb
            Breadcrumbs.Add(new BreadcrumbItem("Articles", "/", relativePath == "articles"));

            // Detail / optional article title
            if (relativePath.StartsWith("articles/detail"))
            {
                var title = CurrentArticle?.Title ?? "Detail";
                Breadcrumbs.Add(new BreadcrumbItem(title, "/articles/detail", true));
            }

            StateHasChanged();
        }

        private void Navigate(string url)
        {
            Navigation.NavigateTo(url);
        }

        private class BreadcrumbItem
        {
            public string Title { get; set; }
            public string Url { get; set; }
            public bool IsCurrent { get; set; }

            public BreadcrumbItem(string title, string url, bool isCurrent)
            {
                Title = title;
                Url = url;
                IsCurrent = isCurrent;
            }
        }
    }
}
