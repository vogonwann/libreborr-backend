using AutoMapper;
using LibreBorr.BL.Interfaces;
using LibreBorr.BL.Models;
using LibreBorr.Services.Interfaces;
using Services = LibreBorr.Services.Models;

namespace LibreBorr.BL;

public class BooksContext : IBooksContext
{   
    private readonly IBookService _bookService;
    private readonly IMapper _mapper;

    public BooksContext(IBookService bookService, IMapper mapper)
    {
        _bookService = bookService;
        _mapper = mapper;
    }

    public async Task<Book> CreateBook(Book book)
    {
        var bookRequest = _mapper.Map<Services.Models.Book>(book);
        var bookCreated = await _bookService.CreateBook(bookRequest);
        return _mapper.Map<Book>(bookCreated);
    }

    public async Task<Book> UpdateBook(Book book)
    {
        var bookRequest = _mapper.Map<Services.Models.Book>(book);
        var updatedBook = await _bookService.UpdateBook(bookRequest);
        return _mapper.Map<Book>(updatedBook);
    }

    public async Task<Book?> GetBook(int id)
    {
        var book = await _bookService.GetBook(id);
        
        return _mapper.Map<Book>(book);
    }

    public async Task<int?> DeleteBook(Book blBook)
    {
        _bookService.DeleteBook(_mapper.Map<Services.Models.Book>(blBook));
        return blBook.Id;
    }

    public async Task AddToWishlist(Book blBook)
    {
        await _bookService.AddToWishlist(_mapper.Map<Services.Models.Book>(blBook));
    }

    public async Task<List<Book>?> GetWishlistBooks()
    {
        var books = await _bookService.GetWishlist();
        return _mapper.Map<List<Book>>(books);
    }

    public async Task<List<Book>> GetBooks()
    {
        var books =  await _bookService.GetBooks();
        return _mapper.Map<List<Book>>(books);
    }
}