using LibreBorr.BL.Interfaces;
using LibreBorr.BL.Models;

[ExtendObjectType(Name = "Query")]
public class BookQuery(IBooksContext booksContext, ILogger<BookQuery> logger, IHttpClientFactory httpClientFactory)
{
    private readonly IBooksContext _booksContext = booksContext;
    private readonly ILogger _logger = logger;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    public async Task<List<Book>?> GetBooks()
    {
        _logger.LogInformation("Trying to get all books");
        try
        {
            var result = await _booksContext.GetBooks();
            _logger.LogInformation("Got all books successfully.");

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("{error}", ex.Message);
            return null;
        }
    }

    public async Task<Book?> GetBook(int id)
    {
        _logger.LogInformation("Trying to get book with id {id}", id);
        try
        {
            var result = await _booksContext.GetBook(id);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("{error}", ex.Message);
            return null;
        }
    }

    public async Task<List<Book>?> GetWishlistBooks()
    {
        _logger.LogInformation("Trying to get wishlist books");
        try
        {
            var result = await _booksContext.GetWishlistBooks();
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("{error}", ex.Message);
            return Enumerable.Empty<Book>().ToList();
        }
    }
}