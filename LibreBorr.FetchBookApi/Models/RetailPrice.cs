using System.Text.Json.Serialization;

namespace LibreBorr.FetchBookApi.Models;

public class RetailPrice
{
    [JsonPropertyName("amount")]
    public double? Amount { get; set; }

    [JsonPropertyName("currencyCode")]
    public string CurrencyCode { get; set; }
}
