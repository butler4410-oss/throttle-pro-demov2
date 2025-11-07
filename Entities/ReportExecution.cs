using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.Entities;

/// <summary>
/// Represents a single execution of a report
/// Stores execution metadata, results cache, and generated file information
/// </summary>
[Index(nameof(ParentId), nameof(Status))]
[Index(nameof(ReportTemplateId), nameof(CreatedAt))]
public class ReportExecution : TenantEntity
{
    /// <summary>
    /// Foreign key to the report template that was executed
    /// </summary>
    [Required]
    public Guid ReportTemplateId { get; set; }

    /// <summary>
    /// Foreign key to the schedule (if this was a scheduled execution)
    /// Null for manual on-demand executions
    /// </summary>
    public Guid? ReportScheduleId { get; set; }

    /// <summary>
    /// User ID who triggered the execution (null for scheduled runs)
    /// </summary>
    public Guid? ExecutedByUserId { get; set; }

    /// <summary>
    /// Current status of the execution
    /// </summary>
    public ReportExecutionStatus Status { get; set; } = ReportExecutionStatus.Queued;

    /// <summary>
    /// Timestamp when execution started
    /// </summary>
    public DateTime? StartedAt { get; set; }

    /// <summary>
    /// Timestamp when execution completed (success or failure)
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// Duration of execution in milliseconds
    /// Used for performance monitoring
    /// </summary>
    public int DurationMs { get; set; } = 0;

    /// <summary>
    /// Number of rows returned by the report query
    /// </summary>
    public int RowCount { get; set; } = 0;

    /// <summary>
    /// JSON-serialized parameters used for this execution
    /// Preserves the exact parameters for reproducibility
    /// </summary>
    public string? ParametersJson { get; set; }

    /// <summary>
    /// JSON-serialized result data cached for quick re-display
    /// May be null for large results or old executions
    /// </summary>
    public string? ResultDataJson { get; set; }

    /// <summary>
    /// Error message if Status = Failed
    /// </summary>
    [MaxLength(2000)]
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// URL or file path to the generated report file
    /// Used for PDF, Excel, CSV exports
    /// </summary>
    [MaxLength(500)]
    public string? OutputFileUrl { get; set; }

    /// <summary>
    /// Format of the generated output file
    /// </summary>
    public ReportFormat? OutputFormat { get; set; }

    /// <summary>
    /// Size of the generated file in bytes
    /// Used for storage tracking and download estimates
    /// </summary>
    [Column(TypeName = "bigint")]
    public long? FileSizeBytes { get; set; }

    // Navigation properties

    /// <summary>
    /// The report template that was executed
    /// </summary>
    [ForeignKey(nameof(ReportTemplateId))]
    public virtual ReportTemplate ReportTemplate { get; set; } = null!;

    /// <summary>
    /// The schedule that triggered this execution (if applicable)
    /// </summary>
    [ForeignKey(nameof(ReportScheduleId))]
    public virtual ReportSchedule? ReportSchedule { get; set; }
}
