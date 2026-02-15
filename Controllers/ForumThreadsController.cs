using DevConnect.DTOs.Forums;
using DevConnect.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevConnect.Controllers;

[ApiController]
[Route("api/forums")]
public class ForumThreadsController(IForumService forumService) : ControllerBase
{
    [HttpGet("threads")]
    public async Task<ActionResult<IReadOnlyList<ThreadResponseDto>>> GetThreads()
    {
        var threads = await forumService.GetThreadsAsync();
        return Ok(threads);
    }

    [HttpPost("threads")]
    public async Task<ActionResult<ThreadResponseDto>> CreateThread([FromBody] CreateThreadDto dto)
    {
        var created = await forumService.CreateThreadAsync(dto);
        return CreatedAtAction(nameof(GetThreads), new { id = created.Id }, created);
    }

    [HttpPost("threads/{threadId:guid}/comments")]
    public async Task<IActionResult> AddComment(Guid threadId, [FromBody] CreateComentDto dto)
    {
        var result = await forumService.AddCommentAsync(threadId, dto);
        return result ? NoContent() : NotFound();
    }
}
