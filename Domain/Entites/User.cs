using System.ComponentModel.DataAnnotations;

namespace Domain.Entites;

public class User
{
    [Key]
    public int Id {get; set;}
    [Required, MaxLength(100)]
    public string Username {get; set;} = null!;
     [Required, MaxLength(100)]
    public string Email {get; set;} = null!;
     [Required, MaxLength(100)]
    public string? Bio {get; set;}
    public List<Post> Posts {get; set;} = [];
}

