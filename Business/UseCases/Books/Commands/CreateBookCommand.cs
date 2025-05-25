using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Books.Commands;

public record CreateBookCommand(
    string Title,
    string Description,
    string ISBN,
    string Publisher,
    string DatePublished,
    Guid AuthorId,
    List<Guid> Genres) : IRequest;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand>
{
    private readonly IBookRepository _repo;

    public CreateBookCommandHandler(IBookRepository repo)
    {
        _repo = repo;
    }
    
    public async Task Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var exists = await _repo.ExistsByTitleAndAuthorAsync(request.Title, request.AuthorId);
        if (exists)
            throw new Exception("A book with the same title and author already exists.");

        var book = new Book
        {
            BookId = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            ISBN = request.ISBN,
            Publisher = request.Publisher,
            DatePublished = request.DatePublished,
            AuthorId = request.AuthorId,
            BookGenres = request.Genres.Select(id => new BookGenre
            {
                GenreId = id,
            }).ToList()
        };

        await _repo.AddAsync(book);
    }
}