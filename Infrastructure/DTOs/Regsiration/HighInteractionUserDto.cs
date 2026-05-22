namespace Infrastructure.DTOs.Regsiration;

public class HighInteractionUserDto
{
    public string Username {get; set;} = null!;
    public int PostCount {get; set;}
    public double AvgCommentsPerPost {get; set;}
}
