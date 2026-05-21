namespace Infrastructure.DTOs.User;

public class UserDto
{

    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Bio { get; set; }
}

