using System.ComponentModel.DataAnnotations;

namespace Domain.Entites;

public class Comment
{
      [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    [Required]
    public int PostId { get; set; }
    [Required, MaxLength(200)]
    public string Text { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // navigation properties
    public User User { get; set; } = null!;
    public Post Post { get; set; } = null!;
}
