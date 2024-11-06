using System.Text.Json.Serialization;

namespace LibreBorr.FetchBookApi.Models;
public class DownloadAccess
{
    [JsonPropertyName("kind")]
    public string Kind { get; set; }

    [JsonPropertyName("volumeId")]
    public string VolumeId { get; set; }

    [JsonPropertyName("restricted")]
    public bool? Restricted { get; set; }

    [JsonPropertyName("deviceAllowed")]
    public bool? DeviceAllowed { get; set; }

    [JsonPropertyName("justAcquired")]
    public bool? JustAcquired { get; set; }

    [JsonPropertyName("maxDownloadDevices")]
    public int? MaxDownloadDevices { get; set; }

    [JsonPropertyName("downloadsAcquired")]
    public int? DownloadsAcquired { get; set; }

    [JsonPropertyName("nonce")]
    public string Nonce { get; set; }

    [JsonPropertyName("source")]
    public string Source { get; set; }

    [JsonPropertyName("reasonCode")]
    public string ReasonCode { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("signature")]
    public string Signature { get; set; }
}
