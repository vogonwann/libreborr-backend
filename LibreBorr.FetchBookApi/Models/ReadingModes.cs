namespace LibreBorr.FetchBookApi.Models;

using System.Text.Json.Serialization;

public class ReadingModes
    {
        [JsonPropertyName("text")]
        public bool Text { get; set; }

        [JsonPropertyName("image")]
        public bool Image { get; set; }
    }
