using System.ComponentModel.DataAnnotations.Schema;

namespace LibreBorr.Services.Models;

public class WishlistItem
{
    public int Id { get; set; }
    [ForeignKey("Book")]
    public int? BookId { get; set; }
    public virtual Book? Book { get; set; }
}