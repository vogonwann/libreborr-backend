using LibreBorr.FetchBookApi.Models;
using Refit;

namespace LibreBorr.FetchBookApi.Refit;

public interface IGoogleBooksApi
{
    [Get("/volumes")]
    Task<GoogleBooksList> GetBooks([AliasAs("q") ]string book, [AliasAs("key")] string googleApiKey);
}

public interface IGoogleBooksImageApi
{
    [Get("/books/content")]
    Task<HttpResponseMessage> GetBookImage(ImageQueryParameters imageQueryParameters);
    
}

public class ImageQueryParameters
{
    [AliasAs("id")]
    public string? Id { get; set; }
    [AliasAs("printsec")]
    public string? Printsec { get; set; }
    [AliasAs("img")]
    public int? Img { get; set; }
    [AliasAs("zoom")]
    public int? Zoom { get; set; }
    [AliasAs("source")]
    public string? Source { get; set; }
}
