using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Genres.Queries;

public record GetGenreByIdQuery(Guid Id) : IRequest<Genre>;

public class GetGenreByIdQueryHandler : IRequestHandler<GetGenreByIdQuery, Genre>
{
    private readonly IGenreRepository _repository;

    public GetGenreByIdQueryHandler(IGenreRepository repository)
    {
        _repository = repository;
    }

    public async Task<Genre> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}