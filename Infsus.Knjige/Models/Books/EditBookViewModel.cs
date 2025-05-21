using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Infsus.Knjige.Models;

public class EditBookViewModel
{
    [Required]
    public Guid BookId { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string ISBN { get; set; }

    [Required]
    public string Publisher { get; set; }

    [Required]
    public string DatePublished { get; set; }

    [Required]
    [Display(Name = "Author")]
    public Guid AuthorId { get; set; }

    public List<SelectListItem> Authors { get; set; } = new();
    
    [Required]
    [Display(Name = "Genre")]
    public List<Guid> SelectedGenreIds { get; set; }
    public List<SelectListItem> Genres { get; set; } = new();
}