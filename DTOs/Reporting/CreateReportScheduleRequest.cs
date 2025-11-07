using System.ComponentModel.DataAnnotations;
using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.DTOs.Reporting;

/// <summary>
/// Request DTO for creating a new report schedule
/// </summary>
public class CreateReportScheduleRequest
{
    /// <summary>
    /// ID of the report template to schedule
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
    /// Execution frequency
    /// </summary>
    [Required]
    public ReportFrequency Frequency { get; set; }

    /// <summary>
    /// Cron expression (required if Frequency = Custom)
    /// Example: "0 9 * * 1" for Mondays at 9am
    /// </summary>
    [MaxLength(100)]
    public string? CronExpression { get; set; }

    /// <summary>
    /// Comma-separated email addresses to receive the report
    /// </summary>
    [MaxLength(1000)]
    public string? EmailRecipients { get; set; }

    /// <summary>
    /// Output format for the generated report file
    /// </summary>
    public ReportFormat OutputFormat { get; set; } = ReportFormat.PDF;

    /// <summary>
    /// Parameter values to use for this schedule
    /// </summary>
    public Dictionary<string, object>? Parameters { get; set; }
}
