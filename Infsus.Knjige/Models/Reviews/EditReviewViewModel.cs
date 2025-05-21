using System.ComponentModel.DataAnnotations;

namespace Infsus.Knjige.Models.Reviews;

public class EditReviewViewModel
{
    public Guid ReviewId { get; set; }
    public Guid BookId { get; set; }

    [Required]
    public string Text { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }
}
