using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.Entities;

/// <summary>
/// Represents a scheduled report execution configuration
/// Supports daily, weekly, monthly, quarterly, or custom cron-based schedules
/// </summary>
[Index(nameof(ParentId), nameof(IsActive))]
[Index(nameof(NextRunAt))]
public class ReportSchedule : TenantEntity
{
    /// <summary>
    /// Foreign key to the report template to execute
    /// </summary>
    [Required]
    public Guid ReportTemplateId { get; set; }

    /// <summary>
    /// Display name for this schedule
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Frequency of execution (Daily, Weekly, Monthly, Quarterly, Custom)
    /// </summary>
    public ReportFrequency Frequency { get; set; }

    /// <summary>
    /// Cron expression for custom schedules (e.g., "0 9 * * 1" for Mondays at 9am)
    /// Only used when Frequency = Custom
    /// </summary>
    [MaxLength(100)]
    public string? CronExpression { get; set; }

    /// <summary>
    /// Next scheduled execution time
    /// Automatically updated after each successful run
    /// </summary>
    public DateTime? NextRunAt { get; set; }

    /// <summary>
    /// Timestamp of the most recent execution
    /// </summary>
    public DateTime? LastRunAt { get; set; }

    /// <summary>
    /// JSON-serialized parameter overrides for report execution
    /// Allows schedules to specify different parameter values
    /// </summary>
    public string? ParametersJson { get; set; }

    /// <summary>
    /// Comma-separated list of email addresses to receive the report
    /// Example: "user1@example.com,user2@example.com"
    /// </summary>
    [MaxLength(1000)]
    public string? EmailRecipients { get; set; }

    /// <summary>
    /// Output format for the generated report file
    /// </summary>
    public ReportFormat OutputFormat { get; set; } = ReportFormat.PDF;

    /// <summary>
    /// Indicates if this schedule is active
    /// Inactive schedules will not be executed
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// User ID who created this schedule
    /// </summary>
    public Guid? CreatedByUserId { get; set; }

    // Navigation properties

    /// <summary>
    /// The report template to execute
    /// </summary>
    [ForeignKey(nameof(ReportTemplateId))]
    public virtual ReportTemplate ReportTemplate { get; set; } = null!;

    /// <summary>
    /// Execution history for this schedule
    /// </summary>
    public virtual ICollection<ReportExecution> Executions { get; set; } = new List<ReportExecution>();
}
