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
    [MaxLength(200)]
    public string? Bio {get; set;}
    public DateTime JoinDate {get; set;}

    // navigation
    public List<Post> Posts {get; set;} = new();
     public List<Comment> Comments {get; set;} = new();
}

