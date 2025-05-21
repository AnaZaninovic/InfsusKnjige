using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ListRepository : IListRepository
{
    private readonly ApplicationContext _context;

    public ListRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<Domain.Entities.List>> GetUserListsAsync(string userId) =>
        await _context.Booklists.Where(l => l.UserId == userId).ToListAsync();

    public async Task<Domain.Entities.List?> GetByIdWithBooksAsync(Guid id) =>
        await _context.Booklists
            .Include(l => l.BookLists)
            .ThenInclude(bl => bl.Book)
            .FirstOrDefaultAsync(l => l.ListId == id);

    public async Task CreateAsync(Domain.Entities.List list)
    {
        _context.Booklists.Add(list);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid listId)
    {
        var list = await _context.Booklists.FindAsync(listId);
        if (list != null)
        {
            _context.Booklists.Remove(list);
            await _context.SaveChangesAsync();
        }
    }

    public async Task AddBookAsync(Guid listId, Guid bookId)
    {
        var exists = await _context.Set<BookList>().AnyAsync(bl => bl.ListId == listId && bl.BookId == bookId);
        if (!exists)
        {
            _context.Set<BookList>().Add(new BookList { ListId = listId, BookId = bookId });
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveBookAsync(Guid listId, Guid bookId)
    {
        var bookList = await _context.Set<BookList>().FirstOrDefaultAsync(bl => bl.ListId == listId && bl.BookId == bookId);
        if (bookList != null)
        {
            _context.Set<BookList>().Remove(bookList);
            await _context.SaveChangesAsync();
        }
    }
}
