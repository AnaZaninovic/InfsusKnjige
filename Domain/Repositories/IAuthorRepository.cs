using Domain.Entities;

namespace Domain.Repositories;

public interface IAuthorRepository
{
    Task<List<Author>> GetAllAsync();
    Task<Author?> GetByIdAsync(Guid id);
    Task CreateAsync(Author author);
    Task UpdateAsync(Author author);
    Task DeleteAsync(Guid id);
}
