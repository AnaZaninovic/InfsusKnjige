using AutoFixture;
using AutoFixture.AutoMoq;
using Business.UseCases.Books.Commands;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Tests.Business.UseCases.Books.Commands;

public class UpdateBookCommandHandlerTests
{
    private UpdateBookCommandHandler _handler; 
    
    private IFixture _fixture;
    private Mock<IBookRepository> _bookRepositoryMock;
    
    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _fixture.Behaviors
            .OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        
        _bookRepositoryMock = _fixture.Freeze<Mock<IBookRepository>>();
        
        _handler = _fixture.Create<UpdateBookCommandHandler>();
    }

    [Test]
    public void UpdateBookCommandHandler_Throws_When_BookNotFound()
    {
        // Arrange
        _bookRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as Book);
        
        var command = _fixture.Create<UpdateBookCommand>();
        
        // Act
        var act = () => _handler.Handle(command, CancellationToken.None);
        
        // Assert
        act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Book not found");
    }
    
    [Test]
    public async Task UpdateBookCommandHandler_UpdatesBook()
    {
        // Arrange
        var command = _fixture.Create<UpdateBookCommand>();
        var book = _fixture.Create<Book>();
        
        _bookRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(book);
        
        // Act
        await _handler.Handle(command, CancellationToken.None);
        
        
        // Assert
        _bookRepositoryMock
            .Verify(x => x.UpdateAsync(It.Is<Book>(b =>
                b.Title == command.Title &&
                b.Description == command.Description &&
                b.ISBN == command.ISBN &&
                b.Publisher == command.Publisher &&
                b.DatePublished == command.DatePublished &&
                b.AuthorId == command.AuthorId &&
                b.BookGenres.Select(bg => bg.GenreId).ToList().SequenceEqual(command.Genres)
            )), Times.Once);
    }
}