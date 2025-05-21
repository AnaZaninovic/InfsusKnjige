namespace Infsus.Knjige.Models.Reviews;

public class ReviewViewModel
{
    public Guid ReviewId { get; set; }
    public string BookTitle { get; set; }
    public string AuthorName { get; set; }
    public string UserName { get; set; }
    public string Text { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
    public string UserId { get; set; }
}


