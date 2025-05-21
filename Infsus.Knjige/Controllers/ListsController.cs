using Business.UseCases.Books.Queries;
using Business.UseCases.Lists.Commands;
using Business.UseCases.Lists.Queries;
using Domain.Entities;
using Infsus.Knjige.Models.Lists;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Infsus.Knjige.Controllers;

public class ListsController : Controller
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;

    public ListsController(IMediator mediator, UserManager<ApplicationUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        var lists = await _mediator.Send(new GetUserListsQuery(userId));

        var vm = lists.Select(l => new ListViewModel
        {
            ListId = l.ListId,
            ListName = l.ListName,
            Description = l.Description
        }).ToList();

        return View(vm);
    }

    [HttpGet]
    public IActionResult Create() => View(new CreateListViewModel());

    [HttpPost]
    public async Task<IActionResult> Create(CreateListViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var userId = _userManager.GetUserId(User);
        await _mediator.Send(new CreateListCommand(model.ListName, model.Description, userId));

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Lists/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var list = await _mediator.Send(new GetListWithBooksQuery(id));
        var books = await _mediator.Send(new GetBooksQuery());
        if (list == null) return NotFound();

        var vm = new ListDetailsViewModel
        {
            ListId = list.ListId,
            ListName = list.ListName,
            Description = list.Description,
            Books = list.BookLists.Select(bl => new BookViewModel
            {
                BookId = bl.BookId,
                Title = bl.Book.Title
            }).ToList(),
            AvailableBooks = books
        };

        return View(vm);
    }


    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteListCommand(id));
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    public async Task<IActionResult> AddBook(Guid ListId, Guid BookId)
    {
        await _mediator.Send(new AddBookToListCommand(ListId, BookId));
        return RedirectToAction(nameof(Details), new { id = ListId });
    }
    
    [HttpPost]
    public async Task<IActionResult> RemoveBook(Guid ListId, Guid BookId)
    {
        await _mediator.Send(new RemoveBookFromListCommand(ListId, BookId));
        return RedirectToAction(nameof(Details), new { id = ListId });
    }
}
