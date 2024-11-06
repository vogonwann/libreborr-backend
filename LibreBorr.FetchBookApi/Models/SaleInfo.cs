namespace LibreBorr.FetchBookApi.Models;

using System.Text.Json.Serialization;


public class SaleInfo
{
    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("saleability")]
    public string Saleability { get; set; }

    [JsonPropertyName("onSaleDate")]
    public DateTime? OnSaleDate { get; set; }

    [JsonPropertyName("isEbook")]
    public bool? IsEbook { get; set; }

    [JsonPropertyName("listPrice")]
    public ListPrice ListPrice { get; set; }

    [JsonPropertyName("retailPrice")]
    public RetailPrice RetailPrice { get; set; }

    [JsonPropertyName("buyLink")]
    public string BuyLink { get; set; }
}