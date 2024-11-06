using LibreBorr.Services.Models;

namespace LibreBorr.Services.Interfaces;

public interface IBookService
{
    Task<List<Book>> GetBooks();
    Task<Book> CreateBook(Book? book);
    Task<Book?> UpdateBook(Book bookRequest);
    Task<Book?> GetBook(int id);
    Task<int> DeleteBook(Book book);
    Task AddToWishlist(Book book);

    Task<List<Book>> GetWishlist();
}