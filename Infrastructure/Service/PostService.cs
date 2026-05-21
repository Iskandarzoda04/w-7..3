using Domain.Entites;
using Infrastructure.DTOs.Post;
using Infrastructure.Interfaces;
using Infrastructure.Persistence.DataContext;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service;

public class PostService(AppDbContext context, ILogger<PostService> logger) : IPostService
{

         private readonly AppDbContext _context = context;
      private readonly ILogger<PostService> _logger = logger;

    public async Task<int> CreateAsync(CreatePostDo dto)
    {
        try
        {
            if (dto.UserId <= 0)
            {
                _logger.LogWarning("Invalid UserId");
                return 0;
            }

            if (string.IsNullOrWhiteSpace(dto.Content))
            {
                _logger.LogWarning("Content is required");
                return 0;
            }

            if (dto.Content.Length > 500)
            {
                _logger.LogWarning("Content max length is 500");
                return 0;
            }

            
           
            
                _logger.LogWarning("User not found");
                return 0;
            

            var post = new Post
            {
                UserId = dto.UserId,
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Post created with Id: {Id}", post.Id);

            return post.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating post");
            return 0;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
    {
        var author = await _context.Posts.FindAsync(id);

        if (author == null)
        {
            _logger.LogWarning("Post not found: {Id}", id);
            return false;
        }

        _context.Posts.Remove(author);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Post deleted: {Id}", id);

        return true;
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error while deleting author: {Id}", id);
        return false;
    }
    }

   public async Task<List<PostDto>> GetAllAsync()
{
    try
    {
        var posts = await _context.Posts.ToListAsync();

        return posts.Select(p => new PostDto
        {
            Id = p.Id,
            UserId = p.UserId,
            Content = p.Content,
            CreatedAt = p.CreatedAt,

        }).ToList();
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error while getting posts");
        return new List<PostDto>();
    }
}
    public async Task<PostDto?> GetByIdAsync(int id)
{
    try
    {
        var post = await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
        {
            _logger.LogWarning("Post not found: {Id}", id);
            return null;
        }

        return new PostDto
        {
            Id = post.Id,
            UserId = post.UserId,
            Content = post.Content,
            CreatedAt = post.CreatedAt,
        };
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error while getting post");
        return null;
    }
}

public async Task<List<PostDto>> GetByUserIdAsync(int userId)
{
    try
    {
        var posts = await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Comments)
            .Where(p => p.UserId == userId)
            .ToListAsync();

        return posts.Select(p => new PostDto
        {
            Id = p.Id,
            UserId = p.UserId,
            Content = p.Content,
            CreatedAt = p.CreatedAt,
        }).ToList();
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error while getting user posts");
        return new List<PostDto>();
    }
}
public async Task<bool> UpdateAsync(int id, UpdatePostDto dto)
{
    try
    {
        var post = await _context.Posts
            .FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
        {
            _logger.LogWarning("Post not found: {Id}", id);
            return false;
        }

        if (string.IsNullOrWhiteSpace(dto.Content))
        {
            _logger.LogWarning("Content is required");
            return false;
        }

        if (dto.Content.Length > 500)
        {
            _logger.LogWarning("Content max length is 500");
            return false;
        }

        post.Content = dto.Content;

        await _context.SaveChangesAsync();

        _logger.LogInformation("Post updated: {Id}", id);

        return true;
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error while updating post");
        return false;
    }
}

}
