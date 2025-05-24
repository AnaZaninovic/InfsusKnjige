using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Tests.Infrastructure.Repositories;

[TestFixture]
public class AuthorRepositoryTests
{
    private ApplicationContext _context;
    private AuthorRepository _repository;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: "TestDB")
            .Options;

        _context = new ApplicationContext(options);
        _repository = new AuthorRepository(_context);
    }

    [TearDown]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task CreateAsync_ValidAuthor_SavesToDatabase()
    {
        // Arrange
        var author = new Author 
        { 
            AuthorId = Guid.NewGuid(),
            Name = "Mato",
            Surname = "Lovrak",
            Biography = "Dječji pisac"
        };

        // Act
        await _repository.CreateAsync(author);

        // Assert
        var result = await _context.Authors.FirstOrDefaultAsync();
    
        Assert.That(result, Is.Not.Null, "Autor nije pronađen u bazi"); 
    
        Assert.Multiple(() =>
        {
            Assert.That(result!.Surname, Is.EqualTo("Lovrak"));
            Assert.That(_context.Authors.Count(), Is.EqualTo(1));
        });
    }
}