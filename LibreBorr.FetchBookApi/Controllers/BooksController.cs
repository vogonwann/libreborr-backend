using System.ComponentModel.DataAnnotations;
using LibreBorr.FetchBookApi.Models;
using LibreBorr.FetchBookApi.Refit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace LibreBorr.FetchBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(IGoogleBooksApi googleBooksApi, IGoogleBooksImageApi googleBooksImageApi, ILogger<BooksController> logger, IConfiguration configuration) : ControllerBase
    {
        private readonly ILogger _logger = logger;

        private readonly string _googleBooksApiKey = configuration["GOOGLE_API_KEY"]!;

        [HttpGet]
        public async Task<GoogleBooksList?> GetBooks([FromQuery] [Required] string book)
        {
            try
            {
                var response = await googleBooksApi.GetBooks(book, _googleBooksApiKey);
                _logger.LogInformation($"Succesfully got data for book: {book}");
                return response;
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        [HttpGet("images")]
        public async Task<string?> GetBookImage(string imageUrl)
        {
            _logger.LogInformation("Trying to get image from google books api...");
            try
            {
                var response = await googleBooksImageApi.GetBookImage(new ImageQueryParameters
                {
                    Id = GetParamFrоmUrl(imageUrl, "id"),
                    Printsec = GetParamFrоmUrl(imageUrl, "printsec"),
                    Img = Convert.ToInt32(GetParamFrоmUrl(imageUrl, "img")),
                    Zoom = Convert.ToInt32(GetParamFrоmUrl(imageUrl, "zoom")),
                    Source = GetParamFrоmUrl(imageUrl, "source")
                });

                response.EnsureSuccessStatusCode();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _logger.LogInformation("Get image was successful");

                    var bytes = await response.Content.ReadAsByteArrayAsync();
                    return Convert.ToBase64String(bytes);
                }
                else
                {
                    _logger.LogError("Failed to get image from google books api");
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Get image failed. Error: {message}", ex.Message);
                return null;
            }
        }

        private string? GetParamFrоmUrl(string url, string parameterName)
        {
            return url?.Split('?')[1]?.Split('&')?.Where(p => Convert.ToBoolean(p?.StartsWith(parameterName)))
                .FirstOrDefault()?.Split('=')[1];
        }

        // [HttpPost]
        // public async Task<byte[]> GetBookImage(string imageUrl)
        // {

        // }
    }
}