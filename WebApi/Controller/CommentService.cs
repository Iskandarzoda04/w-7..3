using Infrastructure.DTOs.Comment;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _service;

    public CommentController(ICommentService service)
    {
        _service = service;
    }

    
    [HttpGet]
    public async Task<List<CommentDto>> GetAll()
    {
        return await _service.GetAllAsync();
    }

    
    [HttpGet("{id}")]
    public async Task<CommentDto?> GetById(int id)
    {
        return await _service.GetByIdAsync(id);
    }

  
    [HttpGet("post/{postId}")]
    public async Task<List<CommentDto>> GetByPostId(int postId)
    {
        return await _service.GetByPostIdAsync(postId);
    }


    [HttpGet("user/{userId}")]
    public async Task<List<CommentDto>> GetByUserId(int userId)
    {
        return await _service.GetByUserIdAsync(userId);
    }


    [HttpPost]
    public async Task<int> Create(CreateCommentDto dto)
    {
        return await _service.CreateAsync(dto);
    }

    
    [HttpPut("{id}")]
    public async Task<bool> Update(int id, UpdateCommentDto dto)
    {
        return await _service.UpdateAsync(id, dto);
    }

    
    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
        return await _service.DeleteAsync(id);
    }
}