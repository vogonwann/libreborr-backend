namespace LibreBorr.FetchBookApi.Models;

using System.Text.Json.Serialization;

public class Pdf
{
    [JsonPropertyName("isAvailable")]
    public bool? IsAvailable { get; set; }

    [JsonPropertyName("downloadLink")]
    public string DownloadLink { get; set; }

    [JsonPropertyName("acsTokenLink")]
    public string AcsTokenLink { get; set; }
}
