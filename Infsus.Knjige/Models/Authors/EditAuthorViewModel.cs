using System.ComponentModel.DataAnnotations;

namespace Infsus.Knjige.Models.Authors;

public class EditAuthorViewModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Surname { get; set; }

    public string Biography { get; set; }
}
