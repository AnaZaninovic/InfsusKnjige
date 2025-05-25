using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Business.UseCases.Authors.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Domain.Repositories;
using Infrastructure.Repositories;
namespace Tests.Integration;

[TestFixture]
public class AuthorIntegrationTests
{
    private ApplicationContext _context;
    private IMediator _mediator;

    [SetUp]
    public void Setup()
    {
        var services = new ServiceCollection()
            .AddDbContext<ApplicationContext>(opts => 
                opts.UseInMemoryDatabase("TestDB"))
            .AddScoped<IAuthorRepository, AuthorRepository>()
            .AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(typeof(CreateAuthorCommand).Assembly))
            .BuildServiceProvider();

        _context = services.GetRequiredService<ApplicationContext>();
        _mediator = services.GetRequiredService<IMediator>();
    }

    [TearDown]
    public void Cleanup()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task CreateAuthor_ShouldPersistInDatabase()
    {
        // Arrange
        var command = new CreateAuthorCommand("Mato", "Lovrak", "Dječji pisac");
        
        // Act
        await _mediator.Send(command);
        
        // Assert
        var author = await _context.Authors.FirstOrDefaultAsync();
        Assert.That(author?.Surname, Is.EqualTo("Lovrak"));
    }
}