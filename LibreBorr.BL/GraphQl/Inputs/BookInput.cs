namespace LibreBorr.BL.GraphQl.Inputs;

public class BookInput
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public int? Year { get; set; }
    public string? Isbn { get; set; }
    public string? Description { get; set; }
    public virtual string? Image { get; set; }
    public virtual string? Authors { get; set; }
    public virtual string? Genres { get; set; }
    public virtual string? Tags { get; set; }
}
