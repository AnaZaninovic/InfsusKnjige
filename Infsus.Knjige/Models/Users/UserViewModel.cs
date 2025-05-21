using System.ComponentModel.DataAnnotations;

namespace Infsus.Knjige.Models;

public class UserViewModel
{
    public Guid UserId { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100)]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}