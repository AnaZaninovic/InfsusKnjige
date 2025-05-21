using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Books.Commands;

public record UpdateBookCommand(
    Guid Id,
    string Title,
    string Description,
    string ISBN,
    string Publisher,
    string DatePublished,
    Guid AuthorId,
    List<Guid> Genres) : IRequest;


public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IBookRepository _repository;

    public UpdateBookCommandHandler(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetByIdAsync(request.Id);
        if (book == null) throw new Exception("Book not found");

        book.Title = request.Title;
        book.Description = request.Description;
        book.ISBN = request.ISBN;
        book.Publisher = request.Publisher;
        book.DatePublished = request.DatePublished;
        book.AuthorId = request.AuthorId;
        
        book.BookGenres.Clear();
        book.BookGenres = request.Genres.Select(id => new BookGenre
        {
            GenreId = id,
        }).ToList();
        
        await _repository.UpdateAsync(book);
    }
}
