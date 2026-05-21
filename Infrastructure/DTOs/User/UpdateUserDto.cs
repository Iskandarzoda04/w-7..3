namespace Infrastructure.DTOs.User;

public class UpdateUserDto
{
     public string Username {get; set;} = null!;
    public string Email {get; set;} = null!;
    public string? Bio {get; set;}
}
