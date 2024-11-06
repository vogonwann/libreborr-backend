using System.Text.Json.Serialization;

namespace LibreBorr.FetchBookApi.Models;
public class VolumeInfo
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("subtitle")]
    public string Subtitle { get; set; }

    [JsonPropertyName("authors")]
    public List<string> Authors { get; set; }

    [JsonPropertyName("publisher")]
    public string Publisher { get; set; }

    [JsonPropertyName("publishedDate")]
    public string PublishedDate { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("industryIdentifiers")]
    public List<IndustryIdentifier> IndustryIdentifiers { get; set; }

    [JsonPropertyName("pageCount")]
    public int? PageCount { get; set; }

    [JsonPropertyName("dimensions")]
    public Dimensions Dimensions { get; set; }

    [JsonPropertyName("printType")]
    public string PrintType { get; set; }

    [JsonPropertyName("mainCategory")]
    public string MainCategory { get; set; }

    [JsonPropertyName("categories")]
    public List<string> Categories { get; set; }

    [JsonPropertyName("averageRating")]
    public double? AverageRating { get; set; }

    [JsonPropertyName("ratingsCount")]
    public int? RatingsCount { get; set; }

    [JsonPropertyName("contentVersion")]
    public string ContentVersion { get; set; }

    [JsonPropertyName("imageLinks")]
    public ImageLinks ImageLinks { get; set; }

    [JsonPropertyName("language")]
    public string Language { get; set; }

    [JsonPropertyName("previewLink")]
    public string PreviewLink { get; set; }

    [JsonPropertyName("infoLink")]
    public string InfoLink { get; set; }

    [JsonPropertyName("canonicalVolumeLink")]
    public string CanonicalVolumeLink { get; set; }
}