using System.ComponentModel.DataAnnotations;

namespace Domain.Entites;

public class Post
{
    [Key]
    public int Id {get; set;}
    [Required, MaxLength(10)]
    public string Title {get; set;} = null!;
    [Required, MaxLength(50)]
    public string Content {get; set;} = null!;
     public DateTime CreatedAt {get; set;}
    public int UserId {get; set;}
    public User User {get; set;} = null!;
     public List<Comment> Comments { get; set; } =[];

}

