namespace Infrastructure.DTOs.Post;

public class CreatePostDo
{
     public int UserId {get; set;}
    public string Content {get; set;} = null!;
}
