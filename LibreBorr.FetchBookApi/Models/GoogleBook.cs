namespace LibreBorr.FetchBookApi.Models;

using System.Text.Json.Serialization;

public class GoogleBook
{
    [JsonPropertyName("kind")]
    public string Kind { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("etag")]
    public string Etag { get; set; }

    [JsonPropertyName("selfLink")]
    public string SelfLink { get; set; }

    [JsonPropertyName("volumeInfo")]
    public VolumeInfo VolumeInfo { get; set; }

    [JsonPropertyName("userInfo")]
    public UserInfo UserInfo { get; set; }

    [JsonPropertyName("saleInfo")]
    public SaleInfo SaleInfo { get; set; }

    [JsonPropertyName("accessInfo")]
    public AccessInfo AccessInfo { get; set; }

    [JsonPropertyName("searchInfo")]
    public SearchInfo SearchInfo { get; set; }
}
