using Domain.Entities;

public class Review
{
    public Guid ReviewId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Text { get; set; }
    public int Rating { get; set; }

    public Guid BookId { get; set; }
    public Book Book { get; set; } = default!;

    public string UserId { get; set; }
    public ApplicationUser User { get; set; } = default!;
}
