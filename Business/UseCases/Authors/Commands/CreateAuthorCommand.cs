using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Authors.Commands;

public record CreateAuthorCommand(string Name, string Surname, string Biography) : IRequest;

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand>
{
    private readonly IAuthorRepository _repo;

    public CreateAuthorCommandHandler(IAuthorRepository repo) => _repo = repo;

    public async Task Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = new Author
        {
            AuthorId = Guid.NewGuid(),
            Name = request.Name,
            Surname = request.Surname,
            Biography = request.Biography
        };

        await _repo.CreateAsync(author);
    }
}
