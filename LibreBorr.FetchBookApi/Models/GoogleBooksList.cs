namespace LibreBorr.FetchBookApi.Models;

using System.Text.Json.Serialization;

public class GoogleBooksList
{
    [JsonPropertyName("items")]
    public List<GoogleBook> Items { get; set; }
}
