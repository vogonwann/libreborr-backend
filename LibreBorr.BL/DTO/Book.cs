namespace LibreBorr.BL.Models;

public class Book
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public byte[] Image { get; set; }
    public string? Authors { get; set; }
    public string? Genres { get; set; }
    public string? Isbn { get; set; }
    public int? Year { get; set; }
    public List<Tag>? Tags { get; set; }
}