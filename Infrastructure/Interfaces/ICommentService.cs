using Infrastructure.DTOs.Comment;

namespace Infrastructure.Interfaces;

public interface ICommentService
{
     Task<List<CommentDto>> GetAllAsync();
    Task<CommentDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreateCommentDto dto);
    Task<bool> UpdateAsync(int id, UpdateCommentDto dto);

    Task<bool> DeleteAsync(int id);
    Task<List<CommentDto>> GetByPostIdAsync(int postId);
    Task<List<CommentDto>> GetByUserIdAsync(int userId);
}
