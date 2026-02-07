using System.Text.Json.Serialization;

public class SearchResultsModel
{
    [JsonPropertyName("results")]
    public List<Result> Results { get; set; } = new();

    [JsonPropertyName("meta")]
    public Meta Meta { get; set; } = new();
}

public class Result
{
    /// <summary>
    /// ISO date (yyyy-MM-dd) or null
    /// </summary>
    [JsonPropertyName("starts_on")]
    public DateOnly? StartsOn { get; set; }

    /// <summary>
    /// ISO date (yyyy-MM-dd) or null
    /// </summary>
    [JsonPropertyName("ends_on")]
    public DateOnly? EndsOn { get; set; }

    /// <summary>
    /// Example: "Section"
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("hierarchy")]
    public Hierarchy Hierarchy { get; set; } = new();

    [JsonPropertyName("hierarchy_headings")]
    public HierarchyHeadings HierarchyHeadings { get; set; } = new();

    [JsonPropertyName("headings")]
    public Headings Headings { get; set; } = new();

    [JsonPropertyName("full_text_excerpt")]
    public string? FullTextExcerpt { get; set; }

    [JsonPropertyName("score")]
    public double Score { get; set; }

    [JsonPropertyName("structure_index")]
    public int StructureIndex { get; set; }

    [JsonPropertyName("reserved")]
    public bool Reserved { get; set; }

    [JsonPropertyName("removed")]
    public bool Removed { get; set; }

    [JsonPropertyName("change_types")]
    public List<string> ChangeTypes { get; set; } = new();

}

public class Hierarchy
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("subtitle")]
    public string? Subtitle { get; set; }

    [JsonPropertyName("chapter")]
    public string? Chapter { get; set; }

    [JsonPropertyName("subchapter")]
    public string? Subchapter { get; set; }

    [JsonPropertyName("part")]
    public string? Part { get; set; }

    [JsonPropertyName("subpart")]
    public string? Subpart { get; set; }

    [JsonPropertyName("subject_group")]
    public string? SubjectGroup { get; set; }

    [JsonPropertyName("section")]
    public string? Section { get; set; }

    [JsonPropertyName("appendix")]
    public string? Appendix { get; set; }
}

public class HierarchyHeadings
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("subtitle")]
    public string? Subtitle { get; set; }

    [JsonPropertyName("chapter")]
    public string? Chapter { get; set; }

    [JsonPropertyName("subchapter")]
    public string? Subchapter { get; set; }

    [JsonPropertyName("part")]
    public string? Part { get; set; }

    [JsonPropertyName("subpart")]
    public string? Subpart { get; set; }

    [JsonPropertyName("subject_group")]
    public string? SubjectGroup { get; set; }

    [JsonPropertyName("section")]
    public string? Section { get; set; }

    [JsonPropertyName("appendix")]
    public string? Appendix { get; set; }
}

public class Headings
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("subtitle")]
    public string? Subtitle { get; set; }

    [JsonPropertyName("chapter")]
    public string? Chapter { get; set; }

    [JsonPropertyName("subchapter")]
    public string? Subchapter { get; set; }

    [JsonPropertyName("part")]
    public string? Part { get; set; }

    [JsonPropertyName("subpart")]
    public string? Subpart { get; set; }

    [JsonPropertyName("subject_group")]
    public string? SubjectGroup { get; set; }

    [JsonPropertyName("section")]
    public string? Section { get; set; }

    [JsonPropertyName("appendix")]
    public string? Appendix { get; set; }
}

public class Meta
{
    [JsonPropertyName("current_page")]
    public int CurrentPage { get; set; }

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("total_count")]
    public int TotalCount { get; set; }

    [JsonPropertyName("max_score")]
    public double MaxScore { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}

