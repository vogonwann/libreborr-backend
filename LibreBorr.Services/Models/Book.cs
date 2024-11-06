using System.ComponentModel.DataAnnotations.Schema;

namespace LibreBorr.Services.Models;

public class Book 
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int? Year { get; set; }
    public string? Isbn { get; set; }
    public string? Description { get; set; }
    
    [ForeignKey("Friend")]
    public int? FriendId { get; set; }
    public virtual Friend? Friend { get; set; }
    
    public int? BookImageId { get; set; }
    public virtual BookImage? Image { get; set; }
    
    public virtual List<Author>? Authors { get; set; }
    public virtual List<Genre>? Genres { get; set; }
    public virtual List<Tag>? Tags { get; set; }
}
