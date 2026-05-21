using Infrastructure.DTOs.Post;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostService _service;

    public PostController(IPostService service)
    {
        _service = service;
    }

    // GET: api/post
    [HttpGet]
    public async Task<ActionResult<List<PostDto>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    // GET: api/post/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<PostDto?>> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound("Post not found");

        return Ok(result);
    }

    // GET: api/post/user/{userId}
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<PostDto>>> GetByUserId(int userId)
    {
        var result = await _service.GetByUserIdAsync(userId);
        return Ok(result);
    }

    // POST: api/post
    [HttpPost]
    public async Task<IActionResult> Create(CreatePostDto dto)
    {
        var result = await _service.CreateAsync(dto);

        if (result == 0)
            return BadRequest("Post not created");

        return Ok(new { Id = result, Message = "Post created successfully" });
    }

    // PUT: api/post/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdatePostDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);

        if (!result)
            return BadRequest("Post not updated");

        return Ok("Post updated successfully");
    }

    // DELETE: api/post/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);

        if (!result)
            return NotFound("Post not found");

        return Ok("Post deleted successfully");
    }
}