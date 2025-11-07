namespace ThrottlePro.Shared.DTOs.Reporting;

/// <summary>
/// Contains the results of a report execution
/// Includes data rows, column definitions, visualization config, and metadata
/// </summary>
public class ReportResult
{
    /// <summary>
    /// Unique identifier for this execution
    /// </summary>
    public Guid ExecutionId { get; set; }

    /// <summary>
    /// Name of the report that was executed
    /// </summary>
    public string ReportName { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp when the report was executed
    /// </summary>
    public DateTime ExecutedAt { get; set; }

    /// <summary>
    /// Execution duration in milliseconds
    /// </summary>
    public int DurationMs { get; set; }

    /// <summary>
    /// Number of rows returned
    /// </summary>
    public int RowCount { get; set; }

    /// <summary>
    /// Result data as list of dictionaries (field name -> value)
    /// Flexible structure supports any report configuration
    /// </summary>
    public List<Dictionary<string, object?>> Data { get; set; } = new();

    /// <summary>
    /// Column definitions with display names and formatting
    /// </summary>
    public List<ReportField> Columns { get; set; } = new();

    /// <summary>
    /// Visualization configuration if this is a chart report
    /// </summary>
    public ReportVisualization? Visualization { get; set; }

    /// <summary>
    /// Summary statistics (totals, averages, etc.)
    /// </summary>
    public Dictionary<string, object>? Summary { get; set; }
}
