using Infrastructure.DTOs.Post;

namespace Infrastructure.Interfaces;

public interface IPostService
{
     Task<List<PostDto>> GetAllAsync();
    Task<PostDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CreatePostDo dto);
    Task<bool> UpdateAsync(int id, UpdatePostDto dto);
    Task<bool> DeleteAsync(int id);
    Task<List<PostDto>> GetByUserIdAsync(int userId);
}

