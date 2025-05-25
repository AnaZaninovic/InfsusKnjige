using Domain.Entities;
using Domain.Repositories;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

public class BookRepository : IBookRepository
{
    private readonly ApplicationContext _context;

    public BookRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetAllAsync() =>
        await _context.Books
            .Include(b => b.Author)
            .Include(b => b.BookGenres)
            .ThenInclude(bg => bg.Genre)
            .ToListAsync();

    public async Task<Book?> GetByIdAsync(Guid id) =>
        await _context.Books
            .Include(b => b.Author)
            .Include(b => b.BookGenres)
            .ThenInclude(bg => bg.Genre)
            .FirstOrDefaultAsync(b => b.BookId == id);

    public async Task AddAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }
    
    public async Task<bool> ExistsByTitleAndAuthorAsync(string title, Guid authorId, Guid? excludeBookId = null)
    {
        return await _context.Books.AnyAsync(b =>
            b.Title == title &&
            b.AuthorId == authorId &&
            (!excludeBookId.HasValue || b.BookId != excludeBookId));
    }
}