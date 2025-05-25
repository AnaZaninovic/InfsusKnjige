using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Genres.Commands;

public record UpdateGenreCommand(Guid GenreId, string GenreName) : IRequest;

public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand>
{
    private readonly IGenreRepository _repository;

    public UpdateGenreCommandHandler(IGenreRepository repository)
    {
        _repository = repository;
    }
    
    public async Task Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await _repository.GetByIdAsync(request.GenreId);
        if (genre == null)
            return;

        var exists = await _repository.ExistsByNameAsync(request.GenreName, request.GenreId);
        if (exists)
            throw new InvalidOperationException("A genre with this name already exists.");

        genre.GenreName = request.GenreName;
        await _repository.UpdateAsync(genre);
    }
}