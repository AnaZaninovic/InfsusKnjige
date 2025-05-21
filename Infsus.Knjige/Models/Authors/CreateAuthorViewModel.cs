using System.ComponentModel.DataAnnotations;

namespace Infsus.Knjige.Models.Authors;

public class CreateAuthorViewModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Surname { get; set; }

    public string Biography { get; set; }
}
