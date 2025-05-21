using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Reviews.Commands;

public record DeleteReviewCommand(Guid ReviewId) : IRequest;

public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand>
{
    private readonly IReviewRepository _repository;

    public DeleteReviewCommandHandler(IReviewRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.ReviewId);
    }
}