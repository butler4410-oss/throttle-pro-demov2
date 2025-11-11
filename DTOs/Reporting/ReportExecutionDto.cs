using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.DTOs.Reporting;

/// <summary>
/// DTO for displaying report execution history
/// </summary>
public class ReportExecutionDto
{
    /// <summary>
    /// Unique identifier for this execution
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// ID of the report template that was executed
    /// </summary>
    public Guid ReportTemplateId { get; set; }

    /// <summary>
    /// Name of the report template
    /// </summary>
    public string ReportTemplateName { get; set; } = string.Empty;

    /// <summary>
    /// Execution status
    /// </summary>
    public ReportExecutionStatus Status { get; set; }

    /// <summary>
    /// When execution started
    /// </summary>
    public DateTime? StartedAt { get; set; }

    /// <summary>
    /// When execution completed
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Duration in milliseconds
    /// </summary>
    public int DurationMs { get; set; }

    /// <summary>
    /// Number of rows returned
    /// </summary>
    public int RowCount { get; set; }

    /// <summary>
    /// Error message if execution failed
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// URL to the generated output file
    /// </summary>
    public string? OutputFileUrl { get; set; }

    /// <summary>
    /// Format of the output file
    /// </summary>
    public ReportFormat? OutputFormat { get; set; }

    /// <summary>
    /// Size of the output file in bytes
    /// </summary>
    public long? FileSizeBytes { get; set; }
}
