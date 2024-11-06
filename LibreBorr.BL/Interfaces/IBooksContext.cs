using LibreBorr.BL.Models;

namespace LibreBorr.BL.Interfaces;

public interface IBooksContext
{
    Task<List<Book>> GetBooks();
    Task<Book> CreateBook(Book book);
    Task<Book> UpdateBook(Book book);
    Task<Book?> GetBook(int id);
    Task<int?> DeleteBook(Book blBook);
    Task AddToWishlist(Book blBook);
    Task<List<Book>?> GetWishlistBooks();
}