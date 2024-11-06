using LibreBorr.Services.Models;

namespace LibreBorr.Services.Interfaces;

public interface IBookService
{
    Task<List<Book>> GetBooks();
    Task CreateBook(Book? book);
    Task UpdateBook(Book bookRequest);
    Task<Book?> GetBook(int id);
    void DeleteBook(Book book);
    Task AddToWishlist(Book book);

    Task<List<Book>> GetWishlist();
}