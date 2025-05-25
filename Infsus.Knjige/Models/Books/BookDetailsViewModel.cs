namespace Infsus.Knjige.Models;

public class BookDetailsViewModel
{
    public Guid BookId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public string Publisher { get; set; }
    public string DatePublished { get; set; }

    public string AuthorName { get; set; }
    public List<string> Genres { get; set; } = new();
}