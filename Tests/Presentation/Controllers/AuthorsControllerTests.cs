using Microsoft.AspNetCore.Mvc;
using Moq;
using MediatR;
using Business.UseCases.Authors.Commands;
using Infsus.Knjige.Controllers;
using Infsus.Knjige.Models.Authors;

namespace Tests.Presentation.Controllers;

[TestFixture]
public class AuthorsControllerTests : IDisposable
{
    private Mock<IMediator> _mediatorMock;
    private AuthorsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new AuthorsController(_mediatorMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        _controller.Dispose();
    }

    public void Dispose()
    {
        TearDown();
        GC.SuppressFinalize(this);
    }

    [Test]
    public async Task Create_ValidModel_RedirectsToIndex()
    {
        // Arrange
        var viewModel = new CreateAuthorViewModel 
        { 
            Name = "Ivo", 
            Surname = "Andrić", 
            Biography = "Nobelovac" 
        };

        // Act
        var result = await _controller.Create(viewModel);

        // Assert
        _mediatorMock.Verify(
            x => x.Send(
                It.Is<CreateAuthorCommand>(c => 
                    c.Name == "Ivo" &&
                    c.Surname == "Andrić" &&
                    c.Biography == "Nobelovac"),
                It.IsAny<CancellationToken>()),
            Times.Once
        );
    
        Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
    }

    [Test]
    public async Task Create_InvalidModel_ReturnsView()
    {
        // Arrange
        _controller.ModelState.AddModelError("Name", "Obavezno polje");
        var invalidModel = new CreateAuthorViewModel();

        // Act
        var result = await _controller.Create(invalidModel);

        // Assert
        Assert.That(result, Is.InstanceOf<ViewResult>());
    }
    [Test]
    public async Task Delete_ValidId_RedirectsToIndex()
    {
        // Arrange
        var authorId = Guid.NewGuid();

        // Act
        var result = await _controller.Delete(authorId);

        // Assert
        _mediatorMock.Verify(x => x.Send(
                It.Is<DeleteAuthorCommand>(c => c.AuthorId == authorId),
                It.IsAny<CancellationToken>()),
            Times.Once
        );
        Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
    }



    [Test]
    public async Task Edit_InvalidModel_ReturnsViewWithModel()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        _controller.ModelState.AddModelError("Name", "Obavezno polje");
        var invalidModel = new CreateAuthorViewModel();

        // Act
        var result = await _controller.Edit(authorId, invalidModel);

        // Assert
        Assert.That(result, Is.InstanceOf<ViewResult>());
    }
}