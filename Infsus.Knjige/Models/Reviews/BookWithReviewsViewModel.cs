namespace Infsus.Knjige.Models.Reviews;

public class BookWithReviewsViewModel
{
    public Guid BookId { get; set; }
    public string Title { get; set; }
    public string AuthorName { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public string Publisher { get; set; }
    public string DatePublished { get; set; }

    public List<ReviewViewModel> Reviews { get; set; } = new();
}

