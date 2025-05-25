using Domain.Entities;

namespace Domain.Repositories;

public interface IBookRepository
{
    Task<List<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(Guid id);
    Task AddAsync(Book book);
    Task DeleteAsync(Guid id);
    
    Task UpdateAsync(Book book);
    Task<bool> ExistsByTitleAndAuthorAsync(string title, Guid authorId, Guid? excludeBookId = null);

}

