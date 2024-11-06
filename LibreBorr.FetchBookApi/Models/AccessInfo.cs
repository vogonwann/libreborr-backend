using System.Text.Json.Serialization;

namespace LibreBorr.FetchBookApi.Models;
public class AccessInfo
{
    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("viewability")]
    public string Viewability { get; set; }

    [JsonPropertyName("embeddable")]
    public bool? Embeddable { get; set; }

    [JsonPropertyName("publicDomain")]
    public bool? PublicDomain { get; set; }

    [JsonPropertyName("textToSpeechPermission")]
    public string TextToSpeechPermission { get; set; }

    [JsonPropertyName("epub")]
    public Epub Epub { get; set; }

    [JsonPropertyName("pdf")]
    public Pdf Pdf { get; set; }

    [JsonPropertyName("webReaderLink")]
    public string WebReaderLink { get; set; }

    [JsonPropertyName("accessViewStatus")]
    public string AccessViewStatus { get; set; }

    [JsonPropertyName("downloadAccess")]
    public DownloadAccess DownloadAccess { get; set; }
}