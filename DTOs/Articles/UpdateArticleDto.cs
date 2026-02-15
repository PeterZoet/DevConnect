namespace DevConnect.DTOs.Articles;

public class UpdateArticleDto
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public Guid? CategoryId { get; set; }
}
