using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Lists.Commands;

public record AddBookToListCommand(Guid ListId, Guid BookId) : IRequest;

public class AddBookToListCommandHandler : IRequestHandler<AddBookToListCommand>
{
    private readonly IListRepository _repo;

    public AddBookToListCommandHandler(IListRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(AddBookToListCommand request, CancellationToken cancellationToken)
    {
        await _repo.AddBookAsync(request.ListId, request.BookId);
    }
}
