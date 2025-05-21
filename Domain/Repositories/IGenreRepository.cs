using Domain.Entities;

namespace Domain.Repositories;

public interface IGenreRepository
{
    Task<List<Genre>> GetAllAsync();
    Task<Genre?> GetByIdAsync(Guid id);
    Task CreateAsync(Genre genre);
    Task DeleteAsync(Guid id);
}