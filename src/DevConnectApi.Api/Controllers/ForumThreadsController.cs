using DevConnect.DTOs.Forums;
using DevConnect.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevConnect.Controllers;

/// <summary>
/// Provides endpoints for forum thread browsing, creation, and commenting.
/// </summary>
[ApiController]
[Route("api/forums")]
[Produces("application/json")]
public class ForumThreadsController(IForumService forumService) : ControllerBase
{
    /// <summary>
    /// Returns all forum threads sorted by newest first.
    /// </summary>
    /// <returns>A list of threads with comments.</returns>
    [HttpGet("threads")]
    [ProducesResponseType(typeof(IReadOnlyList<ThreadResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ThreadResponseDto>>> GetThreads()
    {
        var threads = await forumService.GetThreadsAsync();
        return Ok(threads);
    }

    /// <summary>
    /// Creates a new thread in the forum.
    /// </summary>
    /// <param name="dto">The thread title, author, and content.</param>
    /// <returns>The newly created thread.</returns>
    [HttpPost("threads")]
    [ProducesResponseType(typeof(ThreadResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ThreadResponseDto>> CreateThread([FromBody] CreateThreadDto dto)
    {
        var created = await forumService.CreateThreadAsync(dto);
        return CreatedAtAction(nameof(GetThreads), new { id = created.Id }, created);
    }

    /// <summary>
    /// Adds a comment to an existing thread.
    /// </summary>
    /// <param name="threadId">The thread identifier.</param>
    /// <param name="dto">The comment payload.</param>
    [HttpPost("threads/{threadId:guid}/comments")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddComment(Guid threadId, [FromBody] CreateComentDto dto)
    {
        var result = await forumService.AddCommentAsync(threadId, dto);
        return result ? NoContent() : NotFound();
    }
}
