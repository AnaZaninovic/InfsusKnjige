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
        if (!ModelState.IsValid) return View(model);

        try
        {
            await _mediator.Send(new CreateGenreCommand(model.GenreName));
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(model);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var genre = await _mediator.Send(new GetGenreByIdQuery(id));
        if (genre == null) return NotFound();

        var vm = new EditGenreViewModel
        {
            GenreId = genre.GenreId,
            GenreName = genre.GenreName
        };

        return View(vm);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, EditGenreViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        try
        {
            await _mediator.Send(new UpdateGenreCommand(id, model.GenreName));
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(model);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteGenreCommand(id));
        return RedirectToAction(nameof(Index));
    }
}
