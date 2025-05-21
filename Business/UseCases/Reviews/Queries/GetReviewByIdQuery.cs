using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Reviews.Queries;

public record GetReviewByIdQuery(Guid ReviewId) : IRequest<Review?>;

public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, Review?>
{
    private readonly IReviewRepository _repository;

    public GetReviewByIdQueryHandler(IReviewRepository repository)
    {
        _repository = repository;
    }

    public async Task<Review?> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.ReviewId);
    }
}
