using Business.UseCases.Reviews.Commands;
using Business.UseCases.Reviews.Queries;
using Domain.Entities;
using Infrastructure;
using Infsus.Knjige.Models.Reviews;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Infsus.Knjige.Controllers;

[Authorize]
public class ReviewsController : Controller
{
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationContext _context;

    public ReviewsController(
        IMediator mediator,
        UserManager<ApplicationUser> userManager,
        ApplicationContext context)
    {
        _mediator = mediator;
        _userManager = userManager;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var reviews = await _mediator.Send(new GetAllReviewsQuery());

        var vm = reviews.Select(r => new ReviewViewModel
        {
            ReviewId = r.ReviewId,
            BookTitle = r.Book.Title,
            AuthorName = r.Book.Author?.Name ?? "Unknown",
            UserName = r.User.UserName,
            Text = r.Text,
            Rating = r.Rating,
            CreatedAt = r.CreatedAt,
            UserId = r.UserId
        }).ToList();

        return View(vm);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var books = _context.Books
            .Select(b => new SelectListItem
            {
                Value = b.BookId.ToString(),
                Text = b.Title
            })
            .ToList();

        var vm = new CreateReviewViewModel
        {
            Books = books
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateReviewViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Books = _context.Books
                .Select(b => new SelectListItem
                {
                    Value = b.BookId.ToString(),
                    Text = b.Title
                }).ToList();

            return View(model);
        }

        var userId = _userManager.GetUserId(User);

        await _mediator.Send(new CreateReviewCommand(
            model.BookId,
            userId,
            model.Text,
            model.Rating
        ));

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var review = await _mediator.Send(new GetReviewByIdQuery(id));
        var currentUserId = _userManager.GetUserId(User);

        if (review == null || review.UserId != currentUserId)
            return Forbid();

        var vm = new EditReviewViewModel
        {
            ReviewId = review.ReviewId,
            BookId = review.BookId,
            Text = review.Text,
            Rating = review.Rating
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Guid id, EditReviewViewModel model)
    {
        var review = await _mediator.Send(new GetReviewByIdQuery(id));
        var currentUserId = _userManager.GetUserId(User);

        if (review == null || review.UserId != currentUserId)
            return Forbid();

        await _mediator.Send(new UpdateReviewCommand(id, model.Text, model.Rating));

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        var review = await _mediator.Send(new GetReviewByIdQuery(id));
        var currentUserId = _userManager.GetUserId(User);

        if (review == null || review.UserId != currentUserId)
            return Forbid();

        await _mediator.Send(new DeleteReviewCommand(id));

        return RedirectToAction("Index");
    }
}
