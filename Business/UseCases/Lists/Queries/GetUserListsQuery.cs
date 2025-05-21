using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Lists.Queries;

public record GetUserListsQuery(string UserId) : IRequest<List<Domain.Entities.List>>;

public class GetUserListsQueryHandler : IRequestHandler<GetUserListsQuery, List<Domain.Entities.List>>
{
    private readonly IListRepository _repo;

    public GetUserListsQueryHandler(IListRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Domain.Entities.List>> Handle(GetUserListsQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetUserListsAsync(request.UserId);
    }
}
