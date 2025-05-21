using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Lists.Commands;

public record DeleteListCommand(Guid ListId) : IRequest;

public class DeleteListCommandHandler : IRequestHandler<DeleteListCommand>
{
    private readonly IListRepository _repo;

    public DeleteListCommandHandler(IListRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(DeleteListCommand request, CancellationToken cancellationToken)
    {
        await _repo.DeleteAsync(request.ListId);
    }
}
