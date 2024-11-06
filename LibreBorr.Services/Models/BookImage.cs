using System.ComponentModel.DataAnnotations.Schema;

namespace LibreBorr.Services.Models;

public class BookImage
{
    public int BookImageId { get; set; }
    [ForeignKey("Book")]
    public int? BookId { get; set; }
    public virtual Book Book { get; set; }
    public byte[] Image { get; set; }
}