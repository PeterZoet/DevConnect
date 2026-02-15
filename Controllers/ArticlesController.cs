using DevConnect.DTOs.Articles;
using DevConnect.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevConnect.Controllers;

[ApiController]
[Route("api/articles")]
public class ArticlesController(IArticleService articleService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ArticleResponseDto>>> GetAll()
    {
        var articles = await articleService.GetAllAsync();
        return Ok(articles);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ArticleResponseDto>> GetById(Guid id)
    {
        var article = await articleService.GetByIdAsync(id);
        return article is null ? NotFound() : Ok(article);
    }

    [HttpPost]
    public async Task<ActionResult<ArticleResponseDto>> Create([FromBody] CreateArticleDto dto)
    {
        var created = await articleService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ArticleResponseDto>> Update(Guid id, [FromBody] UpdateArticleDto dto)
    {
        var updated = await articleService.UpdateAsync(id, dto);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await articleService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
