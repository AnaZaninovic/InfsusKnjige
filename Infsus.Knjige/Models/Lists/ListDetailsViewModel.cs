using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Infsus.Knjige.Models.Lists;

public class ListDetailsViewModel
{
    public Guid ListId { get; set; }
    public string ListName { get; set; }
    public string Description { get; set; }

    public List<BookViewModel> Books { get; set; } = new();
    public List<Book> AvailableBooks { get; set; } = new();
}

public class BookViewModel
{
    public Guid BookId { get; set; }
    public string Title { get; set; }
}
