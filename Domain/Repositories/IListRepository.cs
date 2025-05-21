namespace Domain.Repositories;

public interface IListRepository
{
    Task<List<Domain.Entities.List>> GetUserListsAsync(string userId);
    Task<Domain.Entities.List?> GetByIdWithBooksAsync(Guid id);
    Task CreateAsync(Domain.Entities.List list);
    Task DeleteAsync(Guid listId);
    Task AddBookAsync(Guid listId, Guid bookId);
    Task RemoveBookAsync(Guid listId, Guid bookId);
}
