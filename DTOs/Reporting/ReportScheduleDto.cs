using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.DTOs.Reporting;

/// <summary>
/// DTO for displaying report schedules in lists
/// </summary>
public class ReportScheduleDto
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// ID of the report template being scheduled
    /// </summary>
    public Guid ReportTemplateId { get; set; }

    /// <summary>
    /// Name of the report template
    /// </summary>
    public string ReportTemplateName { get; set; } = string.Empty;

    /// <summary>
    /// Name of this schedule
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Execution frequency
    /// </summary>
    public ReportFrequency Frequency { get; set; }

    /// <summary>
    /// Cron expression for custom schedules
    /// </summary>
    public string? CronExpression { get; set; }

    /// <summary>
    /// Next scheduled run time
    /// </summary>
    public DateTime? NextRunAt { get; set; }

    /// <summary>
    /// Last execution time
    /// </summary>
    public DateTime? LastRunAt { get; set; }

    /// <summary>
    /// Email recipients for the report
    /// </summary>
    public string? EmailRecipients { get; set; }

    /// <summary>
    /// Output format for generated reports
    /// </summary>
    public ReportFormat OutputFormat { get; set; }

    /// <summary>
    /// Whether this schedule is active
    /// </summary>
    public bool IsActive { get; set; }
}
