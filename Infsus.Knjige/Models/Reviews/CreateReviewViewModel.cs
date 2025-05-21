using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Infsus.Knjige.Models.Reviews;

public class CreateReviewViewModel
{
    [Required]
    public Guid BookId { get; set; }

    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }

    [Required]
    public string Text { get; set; }

    public List<SelectListItem> Books { get; set; } = new();
}


