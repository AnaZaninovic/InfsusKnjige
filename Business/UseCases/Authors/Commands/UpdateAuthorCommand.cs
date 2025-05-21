using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Authors.Commands;

public record UpdateAuthorCommand(Guid AuthorId, string Name, string Surname, string Biography) : IRequest;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
{
    private readonly IAuthorRepository _repo;

    public UpdateAuthorCommandHandler(IAuthorRepository repo) => _repo = repo;

    public async Task Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _repo.GetByIdAsync(request.AuthorId);
        if (author == null) return;

        author.Name = request.Name;
        author.Surname = request.Surname;
        author.Biography = request.Biography;

        await _repo.UpdateAsync(author);
    }
}
