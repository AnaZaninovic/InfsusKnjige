using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Authors.Queries;

public record GetAuthorByIdQuery(Guid AuthorId) : IRequest<Author?>;

public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Author?>
{
    private readonly IAuthorRepository _repo;

    public GetAuthorByIdQueryHandler(IAuthorRepository repo) => _repo = repo;

    public async Task<Author?> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetByIdAsync(request.AuthorId);
    }
}
