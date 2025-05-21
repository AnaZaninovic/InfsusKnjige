using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Books.Queries;

public record GetBooksQuery : IRequest<List<Book>>;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<Book>>
{
    private readonly IBookRepository _repo;

    public GetBooksQueryHandler(IBookRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetAllAsync();
    }
}

