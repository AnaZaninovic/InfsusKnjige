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
        var exists = await _repository.ExistsByNameAsync(request.GenreName);
        if (exists)
            throw new InvalidOperationException("A genre with this name already exists.");

        var genre = new Genre
        {
            GenreId = Guid.NewGuid(),
            GenreName = request.GenreName
        };

        await _repository.CreateAsync(genre);
    }
}


