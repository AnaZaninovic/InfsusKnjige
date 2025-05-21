namespace Domain.Entities;

public class Genre
{
    public Guid GenreId { get; set; }
    public string GenreName { get; set; }

    public ICollection<BookGenre> BookGenres { get; set; } = default!;
}