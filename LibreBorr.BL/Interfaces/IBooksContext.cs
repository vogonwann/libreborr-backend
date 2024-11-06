using LibreBorr.BL.Models;

namespace LibreBorr.BL.Interfaces;

public interface IBooksContext
{
    Task<List<Book>> GetBooks();
    Task CreateBook(Book book);
    Task UpdateBook(Book book);
    Task<Book?> GetBook(int id);
    Task DeleteBook(Book blBook);
    Task AddToWishlist(Book blBook);
    Task<List<Book>?> GetWishlistBooks();
}