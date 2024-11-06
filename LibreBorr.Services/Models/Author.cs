namespace LibreBorr.Services.Models;

public class Author
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public virtual List<Book>? Books { get; set;}
}