namespace Infrastructure.DTOs.Regsiration;

public class NewRegistrationDto
{
  public string Username {get; set;} = null!;
    public string Email {get; set;} = null!;
    public DateTime JoinDate {get; set;}
}
