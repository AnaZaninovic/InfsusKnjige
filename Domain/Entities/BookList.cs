namespace Domain.Entities;

public class BookList
{
    public Guid BookId { get; set; }
    public Book Book { get; set; } = null!;
    
    public Guid ListId { get; set; }
    public List List { get; set; } = null!;
}