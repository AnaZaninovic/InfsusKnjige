using System.ComponentModel.DataAnnotations;

namespace Infsus.Knjige.Models.Lists;

public class CreateListViewModel
{
    [Required]
    public string ListName { get; set; }

    public string Description { get; set; }
}
