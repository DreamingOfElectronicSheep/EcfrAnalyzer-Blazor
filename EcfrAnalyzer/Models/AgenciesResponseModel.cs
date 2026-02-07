using System.Text.Json.Serialization;

namespace EcfrAnalyzer.Models
{
    public sealed class AgenciesResponseModel
    {
        [JsonPropertyName("agencies")]
        public List<Agency> Agencies { get; set; } = new();
    }

    public sealed class Agency
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("short_name")]
        public string? ShortName { get; set; }

        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("sortable_name")]
        public string? SortableName { get; set; }

        [JsonPropertyName("slug")]
        public string? Slug { get; set; }

        // Recursive children
        [JsonPropertyName("children")]
        public List<Agency> Children { get; set; } = new();

        [JsonPropertyName("cfr_references")]
        public List<CfrReference> CfrReferences { get; set; } = new();

        private string? _cachedChecksum;

        /// <summary>
        /// Gets a fast checksum including all children.
        /// Cached to avoid recomputation on repeated calls.
        /// </summary>
        public string GetChecksum()
        {
            _cachedChecksum ??= ComputeChecksum();
            return _cachedChecksum;
        }

        private string ComputeChecksum()
        {
            var hash = HashCode.Combine(
                Name,
                ShortName,
                DisplayName,
                SortableName,
                Slug,
                Children.Count,
                string.Concat(Children.Select(c => c.GetChecksum()))
            );
            return Math.Abs(hash).ToString("X8");
        }
    }

    public sealed class CfrReference
    {
        [JsonPropertyName("title")]
        public int? Title { get; set; }

        [JsonPropertyName("chapter")]
        public string? Chapter { get; set; }
    }

}
