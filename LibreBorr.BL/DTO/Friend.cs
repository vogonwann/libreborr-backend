namespace LibreBorr.BL.Models;

public class Friend
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Nickname { get; set; }
    public virtual List<Book>? Books { get; set; }
}