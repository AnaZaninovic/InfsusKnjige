using System.ComponentModel.DataAnnotations;

namespace Infsus.Knjige.Models;

public class CreateGenreViewModel
{
    [Required]
    [Display(Name = "Genre Name")]
    public string GenreName { get; set; }
}
