namespace Infrastructure.DTOs.Regsiration;

public class RecentlyActiveUserDto
{
     public string Username {get; set;} = null!;
    public DateTime LastPostDate {get; set;}
    public int PostCount {get; set;}
}
