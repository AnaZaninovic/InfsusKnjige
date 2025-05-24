using AutoFixture;
using AutoFixture.AutoMoq;
using Business.UseCases.Genres.Commands;
using Domain.Repositories;
using Moq;

namespace Tests.Business.UseCases.Genres.Commands;

[TestFixture]
public class DeleteGenreCommandHandlerTests
{
    private IFixture _fixture;
    private Mock<IGenreRepository> _repoMock;
    private DeleteGenreCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _repoMock = _fixture.Freeze<Mock<IGenreRepository>>();
        _handler = _fixture.Create<DeleteGenreCommandHandler>();
    }

    [Test]
    public async Task Handle_ValidCommand_DeletesGenre()
    {
        // Arrange
        var command = new DeleteGenreCommand(Guid.NewGuid());
        
        // Act
        await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        _repoMock.Verify(x => x.DeleteAsync(command.Id), Times.Once);
    }
}