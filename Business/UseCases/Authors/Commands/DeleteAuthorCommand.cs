using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Authors.Commands;

public record DeleteAuthorCommand(Guid AuthorId) : IRequest;

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
{
    private readonly IAuthorRepository _repo;

    public DeleteAuthorCommandHandler(IAuthorRepository repo) => _repo = repo;

    public async Task Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        await _repo.DeleteAsync(request.AuthorId);
    }
}
