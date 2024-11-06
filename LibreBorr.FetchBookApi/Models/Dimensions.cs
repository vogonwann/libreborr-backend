using System;
using System.Text.Json.Serialization;

namespace LibreBorr.FetchBookApi.Models;

public class Dimensions
{
    [JsonPropertyName("height")]
    public string Height { get; set; }

    [JsonPropertyName("width")]
    public string Width { get; set; }

    [JsonPropertyName("thickness")]
    public string Thickness { get; set; }
}
