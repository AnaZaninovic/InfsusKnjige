using Business.UseCases.Genres.Commands;
using Business.UseCases.Genres.Queries;
using Infsus.Knjige.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Infsus.Knjige.Controllers;

public class GenresController : Controller
{
    private readonly IMediator _mediator;

    public GenresController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var genres = await _mediator.Send(new GetGenresQuery());
        var vm = genres.Select(g => new GenreViewModel
        {
            GenreId = g.GenreId,
            GenreName = g.GenreName
        }).ToList();

        return View(vm);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateGenreViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateGenreViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _mediator.Send(new CreateGenreCommand(model.GenreName));
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteGenreCommand(id));
        return RedirectToAction(nameof(Index));
    }
}
