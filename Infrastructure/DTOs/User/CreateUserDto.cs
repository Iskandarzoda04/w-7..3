namespace Infrastructure.DTOs.User;

public class CreateUserDto
{
     public string Username {get; set;} = null!;
    public string Email {get; set;} = null!;
    public string? Bio {get; set;} 
}
