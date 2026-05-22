using Infrastructure.DTOs.Regsiration;
using Infrastructure.Interfaces;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service;

public class UserAnalyticalService : IUserAnalyticsService
{
    private readonly AppDbContext _context;

    public UserAnalyticalService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<NewRegistrationDto>> GetNewRegistrationsAsync()
    {
        var date = DateTime.UtcNow.AddDays(-14);

        var users = await _context.Users
            .Where(u => u.JoinDate >= date)
            .Select(u => new NewRegistrationDto
            {
                Username = u.Username,
                Email = u.Email,
                JoinDate = u.JoinDate
            })
            .ToListAsync();

        return users;
    }

   
    public async Task<List<ActivePosterDto>> GetActivePostersAsync()
    {
        var users = await _context.Users
            .Where(u => u.Posts.Count > 0)
            .Select(u => new ActivePosterDto
            {
                Username = u.Username,
                PostCount = u.Posts.Count
            })
            .ToListAsync();

        return users;
    }

  
    public async Task<List<RecentlyActiveUserDto>> GetRecentlyActiveAsync()
    {
        var date = DateTime.UtcNow.AddDays(-7);

        var users = await _context.Users
            .Where(u => u.Posts.Any(p => p.CreatedAt >= date))
            .Select(u => new RecentlyActiveUserDto
            {
                Username = u.Username,
                LastPostDate = u.Posts.Max(p => p.CreatedAt),
                PostCount = u.Posts.Count(p => p.CreatedAt >= date)
            })
            .ToListAsync();

        return users;
    }

    public async Task<List<TopCreatorDto>> GetTopCreatorsAsync()
    {
        var users = await _context.Users
            .OrderByDescending(u => u.Posts.Count)
            .Take(5)
            .Select(u => new TopCreatorDto
            {
                Username = u.Username,
                PostCount = u.Posts.Count
            })
            .ToListAsync();

        return users;
    }

  
    public async Task<List<HighInteractionUserDto>> GetHighInteractionUsersAsync()
    {
        var users = await _context.Users
            .Where(u => u.Posts.Count > 0)
            .Select(u => new HighInteractionUserDto
            {
                Username = u.Username,
                PostCount = u.Posts.Count,
                AvgCommentsPerPost = u.Posts.Average(p => p.Comments.Count)
            })
            .Where(x => x.AvgCommentsPerPost > 5)
            .ToListAsync();

        return users;
    }
}