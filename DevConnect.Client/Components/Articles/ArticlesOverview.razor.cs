using DevConnect.Shared.Models;

namespace DevConnect.Client.Components.Articles
{
    public partial class ArticlesOverview
    {
        private List<Article> Articles = new();
        private Article? SelectedArticle;

        private bool ShowForm = false;
        private string FormTitle = "New Article";
        private Article EditingArticle = new Article();

        private bool ShowDelete = false;
        private Article? ArticleToDelete;

        protected override void OnInitialized()
        {
            Articles = new List<Article>
    {
        new Article
        {
            Id = 1,
            Title = "Getting Started with DevConnect",
            Content = "DevConnect is a modern intranet platform for developers. Learn how to set up your profile, create articles, and participate in forums. This article is short and introductory.",
            Author = "Alice Johnson",
            PublishedDate = DateTime.Now.AddDays(-10)
        },
        new Article
        {
            Id = 2,
            Title = "Advanced Features of DevConnect",
            Content = @"Once you're familiar with the basics, explore advanced features such as real-time notifications, article tagging, collaborative editing, and moderated forums. 
This article includes medium-length explanations and tips for power users to get the most out of DevConnect.",
            Author = "Bob Smith",
            PublishedDate = DateTime.Now.AddDays(-7)
        },
        new Article
        {
            Id = 3,
            Title = "Forum Tips and Tricks",
            Content = @"The DevConnect forum is designed to foster collaboration. This article explains post creation, comment threading, upvoting, and moderation tools. 
It also includes strategies for engaging respectfully with the community and contributing knowledge effectively.",
            Author = "Charlie Lee",
            PublishedDate = DateTime.Now.AddDays(-5)
        },
        new Article
        {
            Id = 4,
            Title = "Managing Your Articles Efficiently",
            Content = @"Learn how to manage multiple articles, organize them by tags or categories, and update content seamlessly. 
This article is longer and provides step-by-step guidance on keeping content structured and readable for other developers. 
It also explains the versioning system in DevConnect to track edits over time and collaborate with multiple authors on the same content.",
            Author = "Dana White",
            PublishedDate = DateTime.Now.AddDays(-3)
        },
        new Article
        {
            Id = 5,
            Title = "Customizing DevConnect Themes",
            Content = @"DevConnect allows you to switch themes for a personalized experience. In this article, we dive into theme files, how to override colors, 
and how to add your own custom themes. This article is medium length, mostly technical, and includes examples of light, dark, and custom themes.",
            Author = "Evan Green",
            PublishedDate = DateTime.Now.AddDays(-2)
        },
        new Article
        {
            Id = 6,
            Title = "Full Guide to Collaboration in DevConnect",
            Content = @"This is a long-form article that covers almost every aspect of collaboration within DevConnect. 
It includes topics such as: real-time messaging, notifications, forum moderation, article collaboration, theme management, 
and integrating external tools. It's intended for developers who want to fully utilize the platform to work efficiently 
and share knowledge with their team. With examples, screenshots, and detailed instructions, it spans several paragraphs and demonstrates the depth of DevConnect's features.",
            Author = "Fiona Black",
            PublishedDate = DateTime.Now.AddDays(-1)
        }
    };
        }

        private void SelectArticle(Article article)
        {
            SelectedArticle = article;
        }

        private void ShowCreateForm()
        {
            EditingArticle = new Article { PublishedDate = DateTime.Now };
            FormTitle = "New Article";
            ShowForm = true;
        }

        private void EditArticle(Article article)
        {
            EditingArticle = new Article
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                Author = article.Author,
                PublishedDate = article.PublishedDate
            };
            FormTitle = "Edit Article";
            ShowForm = true;
        }

        private void CancelForm()
        {
            ShowForm = false;
        }

        private void SaveArticle()
        {
            if (EditingArticle.Id == 0)
            {
                EditingArticle.Id = Articles.Any() ? Articles.Max(a => a.Id) + 1 : 1;
                Articles.Add(EditingArticle);
            }
            else
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

            ShowForm = false;
        }

        private void ConfirmDelete(Article article)
        {
            ArticleToDelete = article;
            ShowDelete = true;
        }

        private void CancelDelete()
        {
            ShowDelete = false;
            ArticleToDelete = null;
        }

        private void DeleteArticleConfirmed()
        {
            if (ArticleToDelete != null)
            {
                Articles.Remove(ArticleToDelete);
                if (SelectedArticle == ArticleToDelete)
                    SelectedArticle = null;
            }
            CancelDelete();
        }
    }
}
