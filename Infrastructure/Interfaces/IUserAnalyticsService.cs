using Infrastructure.DTOs.Regsiration;

namespace Infrastructure.Interfaces;

public interface IUserAnalyticsService
{
      Task<List<NewRegistrationDto>> GetNewRegistrationsAsync();
    Task<List<ActivePosterDto>> GetActivePostersAsync();
    Task<List<RecentlyActiveUserDto>> GetRecentlyActiveAsync();
    Task<List<TopCreatorDto>> GetTopCreatorsAsync();
    Task<List<HighInteractionUserDto>> GetHighInteractionUsersAsync();
}
