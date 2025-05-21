using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Genres.Commands;

public record DeleteGenreCommand(Guid Id) : IRequest;

public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand>
{
    private readonly IGenreRepository _repository;

    public DeleteGenreCommandHandler(IGenreRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
    }
}
