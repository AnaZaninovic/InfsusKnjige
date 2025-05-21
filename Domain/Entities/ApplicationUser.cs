using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public ICollection<List> Lists { get; set; } = new List<List>();
}
