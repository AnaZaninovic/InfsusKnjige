using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Reviews.Commands;

public record UpdateReviewCommand(Guid ReviewId, string Text, int Rating) : IRequest;

public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand>
{
    private readonly IReviewRepository _repository;

    public UpdateReviewCommandHandler(IReviewRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _repository.GetByIdAsync(request.ReviewId);
        if (review == null)
            return;

        review.Text = request.Text;
        review.Rating = request.Rating;
        review.CreatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(review);
    }
}