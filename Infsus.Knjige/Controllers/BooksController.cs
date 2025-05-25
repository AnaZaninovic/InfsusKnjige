using Business.UseCases.Books.Commands;
using Business.UseCases.Books.Queries;
using Infrastructure;
using Infsus.Knjige.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Infsus.Knjige.Controllers;

public class BooksController : Controller
{
    private readonly IMediator _mediator;
    private readonly ApplicationContext _context;

    public BooksController(IMediator mediator, ApplicationContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _mediator.Send(new GetBooksQuery());
        var vm = books.Select(b => new BookViewModel
        {
            BookId = b.BookId,
            Title = b.Title,
            AuthorName = b.Author?.Name ?? "Unknown"
        }).ToList();

        return View(vm);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var vm = new CreateBookViewModel
        {
            Authors = _context.Authors.Select(a => new SelectListItem { Value = a.AuthorId.ToString(), Text = $"{a.Name} {a.Surname}" }).ToList(),
            Genres = _context.Genres.Select(g => new SelectListItem { Value = g.GenreId.ToString(), Text = g.GenreName }).ToList()
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Authors = _context.Authors.Select(a => new SelectListItem { Value = a.AuthorId.ToString(), Text = $"{a.Name} {a.Surname}" }).ToList();
            model.Genres = _context.Genres.Select(g => new SelectListItem { Value = g.GenreId.ToString(), Text = g.GenreName }).ToList();
            return View(model);
        }

        try
        {
            await _mediator.Send(new CreateBookCommand(
                model.Title, model.Description, model.ISBN, model.Publisher, model.DatePublished, model.AuthorId, model.SelectedGenreIds
            ));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            model.Authors = _context.Authors.Select(a => new SelectListItem { Value = a.AuthorId.ToString(), Text = $"{a.Name} {a.Surname}" }).ToList();
            model.Genres = _context.Genres.Select(g => new SelectListItem { Value = g.GenreId.ToString(), Text = g.GenreName }).ToList();
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Books/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var book = (await _mediator.Send(new GetBooksQuery())).FirstOrDefault(b => b.BookId == id);
        if (book == null) return NotFound();

        var vm = new EditBookViewModel
        {
            BookId = book.BookId,
            Title = book.Title,
            Description = book.Description,
            ISBN = book.ISBN,
            Publisher = book.Publisher,
            DatePublished = book.DatePublished,
            AuthorId = book.AuthorId,
            SelectedGenreIds = book.BookGenres.Select(bg => bg.GenreId).ToList(),
            Authors = _context.Authors.Select(a => new SelectListItem { Value = a.AuthorId.ToString(), Text = $"{a.Name} {a.Surname}" }).ToList(),
            Genres = _context.Genres.Select(g => new SelectListItem { Value = g.GenreId.ToString(), Text = g.GenreName }).ToList()
        };

        return View(vm);
    }

    [HttpPost("Books/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id, EditBookViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Authors = _context.Authors.Select(a => new SelectListItem { Value = a.AuthorId.ToString(), Text = $"{a.Name} {a.Surname}" }).ToList();
            model.Genres = _context.Genres.Select(g => new SelectListItem { Value = g.GenreId.ToString(), Text = g.GenreName }).ToList();
            return View(model);
        }

        try
        {
            await _mediator.Send(new UpdateBookCommand(
                id, model.Title, model.Description, model.ISBN, model.Publisher, model.DatePublished, model.AuthorId, model.SelectedGenreIds
            ));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            model.Authors = _context.Authors.Select(a => new SelectListItem { Value = a.AuthorId.ToString(), Text = $"{a.Name} {a.Surname}" }).ToList();
            model.Genres = _context.Genres.Select(g => new SelectListItem { Value = g.GenreId.ToString(), Text = g.GenreName }).ToList();
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteBookCommand(id));
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var book = (await _mediator.Send(new GetBooksQuery()))
            .FirstOrDefault(b => b.BookId == id);

        if (book == null)
            return NotFound();

        var vm = new BookDetailsViewModel
        {
            BookId = book.BookId,
            Title = book.Title,
            Description = book.Description,
            ISBN = book.ISBN,
            Publisher = book.Publisher,
            DatePublished = book.DatePublished,
            AuthorName = $"{book.Author?.Name} {book.Author?.Surname}",
            Genres = book.BookGenres.Select(bg => bg.Genre.GenreName).ToList()
        };

        return View(vm);
    }
}
