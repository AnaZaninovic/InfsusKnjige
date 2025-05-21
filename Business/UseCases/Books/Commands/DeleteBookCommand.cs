using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Books.Commands;

public record DeleteBookCommand(Guid BookId) : IRequest;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IBookRepository _repo;

    public DeleteBookCommandHandler(IBookRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        await _repo.DeleteAsync(request.BookId);
        return;
    }
}
