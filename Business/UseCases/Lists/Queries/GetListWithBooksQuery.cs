using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Lists.Queries;

public record GetListWithBooksQuery(Guid ListId) : IRequest<Domain.Entities.List?>;

public class GetListWithBooksQueryHandler : IRequestHandler<GetListWithBooksQuery, Domain.Entities.List?>
{
    private readonly IListRepository _repo;

    public GetListWithBooksQueryHandler(IListRepository repo)
    {
        _repo = repo;
    }

    public async Task<Domain.Entities.List?> Handle(GetListWithBooksQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetByIdWithBooksAsync(request.ListId);
    }
}
