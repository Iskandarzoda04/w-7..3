using Infrastructure.DTOs.User;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController
{
    private readonly IUserservice _service;

    public UserController(IUserservice service)
    {
        _service = service;
    }

   
    [HttpGet]
    public async Task<List<UserDto>> GetAll()
    {
        return await _service.GetAllAsync();
    }

   
    [HttpGet("{id}")]
    public async Task<UserDto?> GetById(int id)
    {
        return await _service.GetByIdAsync(id);
    }

    // POST: api/user
    [HttpPost]
    public async Task<int> Create(CreateUserDto dto)
    {
        return await _service.CreateAsync(dto);
    }

  
    [HttpPut("{id}")]
    public async Task<bool> Update(int id, UpdateUserDto dto)
    {
        return await _service.UpdateAsync(id, dto);
    }

    
    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
        return await _service.DeleteAsync(id);
    }
}