using Domain.Entites;
using Infrastructure.DTOs.User;
using Infrastructure.Interfaces;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Service;

public class UserService(AppDbContext context, ILogger<UserService> logger) : IUserservice
{
    private readonly AppDbContext _context = context;
    private readonly ILogger<UserService> _logger = logger;

    public async Task<int> CreateAsync(CreateUserDto dto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.Username))
            {
                _logger.LogWarning("Username is required");
                return 0;
            }

            if (dto.Username.Length > 50)
            {
                _logger.LogWarning("Username max length is 50");
                return 0;
            }

            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                _logger.LogWarning("Email is required");
                return 0;
            }

            if (dto.Email.Length > 100)
            {
                _logger.LogWarning("Email max length is 100");
                return 0;
            }

            if (!string.IsNullOrWhiteSpace(dto.Bio) && dto.Bio.Length > 200)
            {
                _logger.LogWarning("Bio max length is 200");
                return 0;
            }

            var us = await _context.Users
                .AnyAsync(u => u.Username == dto.Username);

            if (us)
            {
                _logger.LogWarning("Username already exists");
                return 0;
            }

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Bio = dto.Bio
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation("User created with Id: {Id}", user.Id);

            return user.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating user");
            return 0;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                _logger.LogWarning("User not found: {Id}", id);
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation("User deleted: {Id}", id);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while deleting user");
            return false;
        }
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        try
        {
            var users = await _context.Users
                .Include(u => u.Posts)
                .ToListAsync();

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
            }).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting users");
            return new List<UserDto>();
        }
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        try
        {
            var user = await _context.Users
                .Include(u => u.Posts)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                _logger.LogWarning("User not found: {Id}", id);
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting user");
            return null;
        }
    }

    public async Task<bool> UpdateAsync(int id, UpdateUserDto dto)
    {
        try
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                _logger.LogWarning("User not found: {Id}", id);
                return false;
            }

            if (string.IsNullOrWhiteSpace(dto.Username))
            {
                _logger.LogWarning("Username is required");
                return false;
            }

            if (dto.Username.Length > 50)
            {
                _logger.LogWarning("Username max length is 50");
                return false;
            }

            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                _logger.LogWarning("Email is required");
                return false;
            }

            if (dto.Email.Length > 100)
            {
                _logger.LogWarning("Email max length is 100");
                return false;
            }

            if (!string.IsNullOrWhiteSpace(dto.Bio) && dto.Bio.Length > 200)
            {
                _logger.LogWarning("Bio max length is 200");
                return false;
            }

            user.Username = dto.Username;
            user.Email = dto.Email;
            user.Bio = dto.Bio;

            await _context.SaveChangesAsync();

            _logger.LogInformation("User updated: {Id}", id);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while updating user");
            return false;
        }
    }
}