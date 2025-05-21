using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Reviews.Queries;

public record GetReviewsForBookQuery(Guid BookId) : IRequest<List<Review>>;

public class GetReviewsForBookQueryHandler : IRequestHandler<GetReviewsForBookQuery, List<Review>>
{
    private readonly IReviewRepository _repo;

    public GetReviewsForBookQueryHandler(IReviewRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Review>> Handle(GetReviewsForBookQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetByBookIdAsync(request.BookId);
    }
}
