using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class List
{
    public Guid ListId { get; set; }
    public string ListName { get; set; }
    public string Description { get; set; }
    
    public string UserId { get; set; }
    public ApplicationUser User { get; set; } = default!;
    
    public ICollection<BookList> BookLists { get; set; } = default!;
}