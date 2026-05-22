using Infrastructure.DTOs.Regsiration;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersAnalyticsController : ControllerBase
{
    private readonly IUserAnalyticsService _service;

    public UsersAnalyticsController(IUserAnalyticsService service)
    {
        _service = service;
    }

    [HttpGet("new-registrations")]
    public async Task<ActionResult<List<NewRegistrationDto>>> GetNewRegistrations()
    {
        return await _service.GetNewRegistrationsAsync();
    }

    [HttpGet("active-posters")]
    public async Task<ActionResult<List<ActivePosterDto>>> GetActivePosters()
    {
        return await _service.GetActivePostersAsync();
    }

    [HttpGet("recently-active")]
    public async Task<ActionResult<List<RecentlyActiveUserDto>>> GetRecentlyActive()
    {
        return await _service.GetRecentlyActiveAsync();
    }

    [HttpGet("top-creators")]
    public async Task<ActionResult<List<TopCreatorDto>>> GetTopCreators()
    {
        return await _service.GetTopCreatorsAsync();
    }

    [HttpGet("high-interaction")]
    public async Task<ActionResult<List<HighInteractionUserDto>>> GetHighInteraction()
    {
        return await _service.GetHighInteractionUsersAsync();
    }
}