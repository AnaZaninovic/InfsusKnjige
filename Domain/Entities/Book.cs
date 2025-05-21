namespace Domain.Entities;

public class Book
{
    public Guid BookId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public string Publisher { get; set; }
    public string DatePublished { get; set; }
    
    public Guid AuthorId { get; set; }
    public Author? Author { get; set; } = null!;
    
    public ICollection<BookGenre> BookGenres { get; set; } = default!;
    public ICollection<BookList> BookLists { get; set; } = default!;
}