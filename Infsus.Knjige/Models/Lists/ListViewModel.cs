namespace Infsus.Knjige.Models.Lists;

public class ListViewModel
{
    public Guid ListId { get; set; }
    public string ListName { get; set; }
    public string Description { get; set; }
    public List<string> BookTitles { get; set; } = new();
}
