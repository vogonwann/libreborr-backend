using System.Text.Json.Serialization;

namespace LibreBorr.FetchBookApi.Models;
public class IndustryIdentifier
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("identifier")]
    public string Identifier { get; set; }
}
