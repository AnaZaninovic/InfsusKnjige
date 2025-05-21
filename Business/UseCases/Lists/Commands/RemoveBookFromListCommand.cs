using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Lists.Commands;

public record RemoveBookFromListCommand(Guid ListId, Guid BookId) : IRequest;

public class RemoveBookFromListCommandHandler : IRequestHandler<RemoveBookFromListCommand>
{
    private readonly IListRepository _repo;

    public RemoveBookFromListCommandHandler(IListRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(RemoveBookFromListCommand request, CancellationToken cancellationToken)
    {
        await _repo.RemoveBookAsync(request.ListId, request.BookId);
    }
}
