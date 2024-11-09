using AutoMapper;
using HotChocolate.Subscriptions;
using LibreBorr.BL.GraphQl.Inputs;
using LibreBorr.BL.Interfaces;
using LibreBorr.Web.Responses;

namespace LibreBorr.Web.GraphQl.Mutations;

[ExtendObjectType(Name = "Mutation")]
public class BookMutation(IBooksContext context, ILogger<BookMutation> logger, IMapper mapper, IHttpClientFactory httpClientFactory, IConfiguration configuration)
{
    private readonly ILogger _logger = logger;

    public async Task<BookResponse?> CreateBook(BookInput bookInput, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        var encodedImageUrl = Uri.EscapeDataString(bookInput.Image);
        var fetchBookImageApiUrl = configuration.GetSection("FetchBookApi").GetSection("ImageEndpoint").Value ?? "http://localhost:5104/api/books/images?imageUrl=";
        var httpRequestMessage =
            new HttpRequestMessage(HttpMethod.Get, $"{fetchBookImageApiUrl}{encodedImageUrl}"); // TODO: add to config
        var httpClient = httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var contentStream =
                 await httpResponseMessage.Content.ReadAsByteArrayAsync(cancellationToken);

            _logger.LogInformation("Trying to add book with title: {title}", bookInput.Title);
            var response = new BookResponse(bookInput.Title, nameof(CreateBook));
            try
            {
                var blBook = mapper.Map<BL.Models.Book>(bookInput);

                blBook.Image = contentStream;

                await context.CreateBook(blBook);
                _logger.LogInformation("Book: {book} created.", bookInput.Title);
                
                await sender.SendAsync(nameof(CreateBook), response, cancellationToken);

                return response;
            }
            catch (Exception ex)
            {
                response.Error.Code = 0;
                response.Error.InnerMessage = ex.InnerException?.Message;
                response.Error.Message = ex.Message;

                _logger.LogError("{error}", ex.Message);
                return response;
            }
            finally
            {
                _logger.LogInformation("Adding book finished!");
            }
        }
        return null;
    }

    public async Task<BookResponse?> UpdateBook(BookInput bookInput, [Service] ITopicEventSender sender, CancellationToken cancellationToken)
    {
        var response = new BookResponse(bookInput.Title ?? string.Empty, nameof(UpdateBook), id: bookInput.Id, description: bookInput.Description);
        try
        {
            var blBook = mapper.Map<BL.Models.Book>(bookInput);
            await context.UpdateBook(blBook);
            _logger.LogInformation("Book: {book} updated.", bookInput.Title);
            
            await sender.SendAsync(nameof(UpdateBook), response, cancellationToken);

            return response;
        }
        catch (Exception ex)
        {
            if (response.Error != null)
            {
                response.Error.Code = 0;
                response.Error.InnerMessage = ex.InnerException?.Message;
                response.Error.Message = ex.Message;
            }

            _logger.LogError("{error}", ex.Message);
            return response;
        }
        finally
        {
            _logger.LogInformation("Adding book finished!");
        }
    }

    public async Task<BookResponse> DeleteBook(BookInput bookInput, [Service] ITopicEventSender sender,
        CancellationToken cancellationToken)
    {
        var response = new BookResponse(bookInput.Title, nameof(DeleteBook), id: bookInput.Id, description: bookInput.Description);

        try
        {
            var blBook = mapper.Map<BL.Models.Book>(bookInput);
            await context.DeleteBook(blBook);
            await sender.SendAsync(nameof(DeleteBook), response, cancellationToken);
            _logger.LogInformation("Book: {book} deleted.", bookInput.Title);
            return response;
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            if (response.Error != null)
            {
                response.Error.Code = 0;
                response.Error.InnerMessage = ex.InnerException?.Message;
                response.Error.Message = ex.Message;
            }
            
            return response;
        }
    }

    public async Task AddToWishlist(BookInput bookInput, [Service] ITopicEventSender sender,
        CancellationToken cancellationToken)
    {
        var blBook = mapper.Map<BL.Models.Book>(bookInput);
        await context.AddToWishlist(blBook);
    }
}
