namespace Domain.Repositories;

public interface IReviewRepository
{
    Task<List<Review>> GetByBookIdAsync(Guid bookId);
    Task<Review?> GetByIdAsync(Guid reviewId); 
    Task CreateAsync(Review review);
    Task UpdateAsync(Review review);
    Task DeleteAsync(Guid reviewId);
    Task<List<Review>> GetAllAsync();
}
