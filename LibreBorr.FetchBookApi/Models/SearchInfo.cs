namespace LibreBorr.FetchBookApi.Models;

using System.Text.Json.Serialization;

public class SearchInfo
{
    [JsonPropertyName("textSnippet")]
    public string TextSnippet { get; set; }
}
