using Business.UseCases.Authors.Commands;
using Business.UseCases.Authors.Queries;
using Infsus.Knjige.Models.Authors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Infsus.Knjige.Controllers;

public class AuthorsController : Controller
{
    private readonly IMediator _mediator;

    public AuthorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var authors = await _mediator.Send(new GetAuthorsQuery());
        var vm = authors.Select(a => new AuthorViewModel
        {
            AuthorId = a.AuthorId,
            Name = a.Name,
            Surname = a.Surname
        }).ToList();

        return View(vm);
    }

    [HttpGet]
    public IActionResult Create() => View(new CreateAuthorViewModel());

    [HttpPost]
    public async Task<IActionResult> Create(CreateAuthorViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        await _mediator.Send(new CreateAuthorCommand(model.Name, model.Surname, model.Biography));
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var author = await _mediator.Send(new GetAuthorByIdQuery(id));
        if (author == null) return NotFound();

        var vm = new CreateAuthorViewModel
        {
            Name = author.Name,
            Surname = author.Surname,
            Biography = author.Biography
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, CreateAuthorViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        await _mediator.Send(new UpdateAuthorCommand(id, model.Name, model.Surname, model.Biography));
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteAuthorCommand(id));
        return RedirectToAction(nameof(Index));
    }
}
