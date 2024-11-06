namespace LibreBorr.FetchBookApi.Models;

using System.Text.Json.Serialization;

public class PanelizationSummary
    {
        [JsonPropertyName("containsEpubBubbles")]
        public bool ContainsEpubBubbles { get; set; }

        [JsonPropertyName("containsImageBubbles")]
        public bool ContainsImageBubbles { get; set; }
    }
