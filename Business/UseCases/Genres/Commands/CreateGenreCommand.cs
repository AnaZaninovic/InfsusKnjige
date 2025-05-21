using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Genres.Commands;

public record CreateGenreCommand(string GenreName) : IRequest;

public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand>
{
    private readonly IGenreRepository _repository;

    public CreateGenreCommandHandler(IGenreRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = new Genre
        {
            GenreId = Guid.NewGuid(),
            GenreName = request.GenreName
        };

        await _repository.CreateAsync(genre);
        return ;
    }
}
