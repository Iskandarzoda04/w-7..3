using System.ComponentModel.DataAnnotations;

namespace Domain.Entites;

public class Comment
{
     [Key]
     public int Id {get; set;}
    public int UserId {get; set;}
    public int PostId {get; set;}
    [Required, MaxLength(200)]
    public string Text {get; set;} = null!;
    public DateTime CreatedAt {get; set;}
    public User User {get; set;} = null!;
    public Post Post {get; set;} = null!;
}
