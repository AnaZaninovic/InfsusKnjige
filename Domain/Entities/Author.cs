namespace Domain.Entities;

public class Author
{
    public Guid AuthorId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Biography { get; set; }
}