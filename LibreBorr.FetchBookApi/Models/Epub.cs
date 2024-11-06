using System.Text.Json.Serialization;

namespace LibreBorr.FetchBookApi.Models; public class Epub
{
    [JsonPropertyName("isAvailable")]
    public bool? IsAvailable { get; set; }

    [JsonPropertyName("downloadLink")]
    public string DownloadLink { get; set; }

    [JsonPropertyName("acsTokenLink")]
    public string AcsTokenLink { get; set; }
}
