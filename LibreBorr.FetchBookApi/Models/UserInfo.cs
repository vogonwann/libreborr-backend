using System;
using System.Text.Json.Serialization;

namespace LibreBorr.FetchBookApi.Models;

public class UserInfo
{
    [JsonPropertyName("review")]
    public string Review { get; set; }

    [JsonPropertyName("readingPosition")]
    public string ReadingPosition { get; set; }

    [JsonPropertyName("isPurchased")]
    public bool? IsPurchased { get; set; }

    [JsonPropertyName("isPreordered")]
    public bool? IsPreordered { get; set; }

    [JsonPropertyName("updated")]
    public DateTime? Updated { get; set; }
}
