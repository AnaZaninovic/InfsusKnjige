using Domain.Repositories;
using MediatR;

namespace Business.UseCases.Reviews.Queries;

public record GetAllReviewsQuery : IRequest<List<Review>>;

public class GetAllReviewsQueryHandler : IRequestHandler<GetAllReviewsQuery, List<Review>>
{
    private readonly IReviewRepository _repo;

    public GetAllReviewsQueryHandler(IReviewRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Review>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetAllAsync();
    }
}
