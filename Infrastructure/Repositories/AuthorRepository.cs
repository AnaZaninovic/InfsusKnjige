﻿using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly ApplicationContext _context;

    public AuthorRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<List<Author>> GetAllAsync() => await _context.Authors.ToListAsync();

    public async Task<Author?> GetByIdAsync(Guid id) => await _context.Authors.FindAsync(id);

    public async Task CreateAsync(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Author author)
    {
        _context.Authors.Update(author);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author != null)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
}
