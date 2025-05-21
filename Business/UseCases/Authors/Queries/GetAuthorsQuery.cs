using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Authors.Queries;

public record GetAuthorsQuery : IRequest<List<Author>>;

public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, List<Author>>
{
    private readonly IAuthorRepository _repo;

    public GetAuthorsQueryHandler(IAuthorRepository repo) => _repo = repo;

    public async Task<List<Author>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetAllAsync();
    }
}
