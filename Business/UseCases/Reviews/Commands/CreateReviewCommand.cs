using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Reviews.Commands;

public record CreateReviewCommand(Guid BookId, string UserId, string Text, int Rating) : IRequest;


public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand>
{
    private readonly IReviewRepository _repo;

    public CreateReviewCommandHandler(IReviewRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = new Review
        {
            ReviewId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Text = request.Text,
            Rating = request.Rating,
            BookId = request.BookId,
            UserId = request.UserId
        };

        await _repo.CreateAsync(review);
    }
}

