using Infrastructure.DTOs.User;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserservice _service;

    public UserController(IUserservice service)
    {
        _service = service;
    }

    // GET: api/user
    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    // GET: api/user/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto?>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound("User not found");

        return Ok(result);
    }

    // POST: api/user
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        var result = await _service.CreateAsync(dto);

        if (result == 0)
            return BadRequest("User not created");

        return Ok(new { Id = result, Message = "User created successfully" });
    }

    // PUT: api/user/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateUserDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);

        if (!result)
            return BadRequest("User not updated");

        return Ok("User updated successfully");
    }

    // DELETE: api/user/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);

        if (!result)
            return NotFound("User not found");

        return Ok("User deleted successfully");
    }
}