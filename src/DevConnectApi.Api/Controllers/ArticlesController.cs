using DevConnect.DTOs.Articles;
using DevConnect.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevConnect.Controllers;

/// <summary>
/// Provides endpoints for creating, reading, updating, and deleting knowledge base articles.
/// </summary>
[ApiController]
[Route("api/articles")]
[Produces("application/json")]
public class ArticlesController(IArticleService articleService) : ControllerBase
{
    /// <summary>
    /// Returns all articles currently available in DevConnect.
    /// </summary>
    /// <returns>A list of article summaries.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ArticleResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ArticleResponseDto>>> GetAll()
    {
        var articles = await articleService.GetAllAsync();
        return Ok(articles);
    }

    /// <summary>
    /// Returns one article by its identifier.
    /// </summary>
    /// <param name="id">The unique article identifier.</param>
    /// <returns>The full article details.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ArticleResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ArticleResponseDto>> GetById(Guid id)
    {
        var article = await articleService.GetByIdAsync(id);
        return article is null ? NotFound() : Ok(article);
    }

    /// <summary>
    /// Creates a new article.
    /// </summary>
    /// <param name="dto">The article payload to create.</param>
    /// <returns>The created article.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ArticleResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ArticleResponseDto>> Create([FromBody] CreateArticleDto dto)
    {
        var created = await articleService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Updates an existing article.
    /// </summary>
    /// <param name="id">The unique article identifier.</param>
    /// <param name="dto">The fields to update on the article.</param>
    /// <returns>The updated article if found.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ArticleResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ArticleResponseDto>> Update(Guid id, [FromBody] UpdateArticleDto dto)
    {
        var updated = await articleService.UpdateAsync(id, dto);
        return updated is null ? NotFound() : Ok(updated);
    }

    /// <summary>
    /// Deletes an article.
    /// </summary>
    /// <param name="id">The unique article identifier.</param>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await articleService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
