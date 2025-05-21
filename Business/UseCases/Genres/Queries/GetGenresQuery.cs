using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Genres.Queries;

public record GetGenresQuery : IRequest<List<Genre>>;

public class GetGenresQueryHandler : IRequestHandler<GetGenresQuery, List<Genre>>
{
    private readonly IGenreRepository _repository;

    public GetGenresQueryHandler(IGenreRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Genre>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
