namespace DevConnect.DTOs.Forums;

public class CreateComentDto
{
    public Guid AuthorId { get; set; }
    public string Content { get; set; } = string.Empty;
}
