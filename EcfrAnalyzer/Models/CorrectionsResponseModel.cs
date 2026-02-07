using System.Text.Json.Serialization;

namespace EcfrAnalyzer.Models
{

    public sealed class CorrectionsResponseModel
    {
        [JsonPropertyName("ecfr_corrections")]
        public List<EcfrCorrection> EcfrCorrections { get; set; } = new();
    }

    public sealed class EcfrCorrection
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("cfr_references")]
        public List<CorrectionsCfrReference> CfrReferences { get; set; } = new();

        /// <summary>
        /// Examples: "Amended", or longer strings like " (e)(398) and (399) correctly designated as (e)(298) and (299)\r\n           \r\n"
        /// </summary>
        [JsonPropertyName("corrective_action")]
        public string? CorrectiveAction { get; set; }

        /// <summary>
        /// Date when the error was corrected (yyyy-MM-dd in the sample). Using DateOnly to avoid time confusion.
        /// </summary>
        [JsonPropertyName("error_corrected")]
        public DateOnly? ErrorCorrected { get; set; }

        /// <summary>
        /// Date when the error occurred (yyyy-MM-dd in the sample).
        /// </summary>
        [JsonPropertyName("error_occurred")]
        public DateOnly? ErrorOccurred { get; set; }

        /// <summary>
        /// Federal Register citation, e.g., "70 FR 55706".
        /// </summary>
        [JsonPropertyName("fr_citation")]
        public string? FrCitation { get; set; }

        [JsonPropertyName("position")]
        public int? Position { get; set; }

        [JsonPropertyName("display_in_toc")]
        public bool? DisplayInToc { get; set; }

        [JsonPropertyName("title")]
        public int? Title { get; set; }

        [JsonPropertyName("year")]
        public int? Year { get; set; }

        /// <summary>
        /// Last modified date; sample is date-only (yyyy-MM-dd). If your source sometimes includes time,
        /// change this to DateTime? and adjust the converter as needed.
        /// </summary>
        [JsonPropertyName("last_modified")]
        public DateOnly? LastModified { get; set; }
    }

    public sealed class CorrectionsCfrReference
    {
        /// <summary>
        /// Full CFR reference string, e.g., "7 CFR 2.80" or "50 CFR 660.71".
        /// </summary>
        [JsonPropertyName("cfr_reference")]
        public string? Reference { get; set; }

        [JsonPropertyName("hierarchy")]
        public CfrHierarchy? Hierarchy { get; set; }
    }


    public sealed class CfrHierarchy
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        /// Optional; present in some items (e.g., "A").
        /// </summary>
        [JsonPropertyName("subtitle")]
        public string? Subtitle { get; set; }

        [JsonPropertyName("part")]
        public string? Part { get; set; }

        /// <summary>
        /// Optional; present in some items (e.g., "N").
        /// </summary>
        [JsonPropertyName("subpart")]
        public string? Subpart { get; set; }

        [JsonPropertyName("section")]
        public string? Section { get; set; }
    }

}
