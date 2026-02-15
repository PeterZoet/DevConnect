using DevConnect.DTOs.Forums;

namespace DevConnect.Services.Interfaces;

public interface IForumService
{
    Task<IReadOnlyList<ThreadResponseDto>> GetThreadsAsync();
    Task<ThreadResponseDto> CreateThreadAsync(CreateThreadDto dto);
    Task<bool> AddCommentAsync(Guid threadId, CreateComentDto dto);
}
