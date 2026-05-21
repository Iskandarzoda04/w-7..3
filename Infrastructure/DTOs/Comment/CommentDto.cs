namespace Infrastructure.DTOs.Comment;

public class CommentDto
{
     public int Id {get; set;}
    public int UserId {get; set;}
    public int PostId {get; set;}
    public string Text {get; set;} = null!;
    public DateTime CreatedAt {get; set;}
}
