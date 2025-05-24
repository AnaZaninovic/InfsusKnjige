using AutoFixture;
using AutoFixture.AutoMoq;
using Business.UseCases.Authors.Commands;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Tests.Business.UseCases.Authors.Commands;

[TestFixture]
public class CreateAuthorCommandHandlerTests
{
    private IFixture _fixture;
    private Mock<IAuthorRepository> _repoMock;
    private CreateAuthorCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        
        _repoMock = _fixture.Freeze<Mock<IAuthorRepository>>();
        _handler = _fixture.Create<CreateAuthorCommandHandler>();
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesAuthor()
    {
        // Arrange
        var command = new CreateAuthorCommand("Ivo", "Andrić", "Nobelovac");
        
        // Act
        await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        _repoMock.Verify(x => x.CreateAsync(It.Is<Author>(a =>
            a.Name == command.Name &&
            a.Surname == command.Surname &&
            a.Biography == command.Biography
        )), Times.Once);
    }
}