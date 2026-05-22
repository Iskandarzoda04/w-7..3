using Domain.Entites;
using Infrastructure.DTOs.Comment;
using Infrastructure.Interfaces;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Service;

public class CommentService(AppDbContext context, ILogger<CommentService> logger) : ICommentService
{
    private readonly AppDbContext _context = context;
    private readonly ILogger<CommentService> _logger = logger;

    public async Task<int> CreateAsync(CreateCommentDto dto)
    {
        try
        {
            if (dto.UserId <= 0 || dto.PostId <= 0)
            {
                _logger.LogWarning("Invalid UserId or PostId");
                return 0;
            }

            if (string.IsNullOrWhiteSpace(dto.Text))
            {
                _logger.LogWarning("Text is required");
                return 0;
            }

            if (dto.Text.Length > 300)
            {
                _logger.LogWarning("Text max length is 300");
                return 0;
            }

        

            var comment = new Comment
            {
                UserId = dto.UserId,
                PostId = dto.PostId,
                Text = dto.Text,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Comment created with Id: {Id}", comment.Id);

            return comment.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating comment");
            return 0;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                _logger.LogWarning("Comment not found: {Id}", id);
                return false;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Comment deleted: {Id}", id);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while deleting comment");
            return false;
        }
    }

    public async Task<List<CommentDto>> GetAllAsync()
    {
        try
        {
            var comments = await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Post)
                .ToListAsync();

            return comments.Select(c => new CommentDto
            {
                Id = c.Id,
                UserId = c.UserId,
                PostId = c.PostId,
                Text = c.Text,
                CreatedAt = c.CreatedAt
            }).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting comments");
            return new List<CommentDto>();
        }
    }

    public async Task<CommentDto?> GetByIdAsync(int id)
    {
        try
        {
            var comment = await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Post)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                _logger.LogWarning("Comment not found: {Id}", id);
                return null;
            }

            return new CommentDto
            {
                Id = comment.Id,
                UserId = comment.UserId,
                PostId = comment.PostId,
                Text = comment.Text,
                CreatedAt = comment.CreatedAt
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting comment");
            return null;
        }
    }

    public async Task<List<CommentDto>> GetByPostIdAsync(int postId)
    {
        try
        {
            var comments = await _context.Comments
                .Include(c => c.User)
                .Where(c => c.PostId == postId)
                .ToListAsync();

            return comments.Select(c => new CommentDto
            {
                Id = c.Id,
                UserId = c.UserId,
                PostId = c.PostId,
                Text = c.Text,
                CreatedAt = c.CreatedAt
            }).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting comments by post");
            return new List<CommentDto>();
        }
    }

    public async Task<List<CommentDto>> GetByUserIdAsync(int userId)
    {
        try
        {
            var comments = await _context.Comments
                .Include(c => c.User)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            return comments.Select(c => new CommentDto
            {
                Id = c.Id,
                UserId = c.UserId,
                PostId = c.PostId,
                Text = c.Text,
                CreatedAt = c.CreatedAt
            }).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting user comments");
            return new List<CommentDto>();
        }
    }

    public async Task<bool> UpdateAsync(int id, UpdateCommentDto dto)
    {
        try
        {
            var comment = await _context.Comments
                .FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                _logger.LogWarning("Comment not found: {Id}", id);
                return false;
            }

            if (string.IsNullOrWhiteSpace(dto.Text))
            {
                _logger.LogWarning("Text is required");
                return false;
            }

            if (dto.Text.Length > 300)
            {
                _logger.LogWarning("Text max length is 300");
                return false;
            }

            comment.Text = dto.Text;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Comment updated: {Id}", id);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while updating comment");
            return false;
        }
    }
}