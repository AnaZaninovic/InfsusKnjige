using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Lists.Commands;

public record CreateListCommand(string ListName, string Description, string UserId) : IRequest;

public class CreateListCommandHandler : IRequestHandler<CreateListCommand>
{
    private readonly IListRepository _repo;

    public CreateListCommandHandler(IListRepository repo) => _repo = repo;

    public async Task Handle(CreateListCommand request, CancellationToken cancellationToken)
    {
        var list = new Domain.Entities.List
        {
            ListId = Guid.NewGuid(),
            ListName = request.ListName,
            Description = request.Description,
            UserId = request.UserId
        };

        await _repo.CreateAsync(list);
    }
}
