using System.ComponentModel.DataAnnotations;
using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.DTOs.Reporting;

/// <summary>
/// Request DTO for executing a report on-demand
/// </summary>
public class ExecuteReportRequest
{
    /// <summary>
    /// ID of the report template to execute
    /// </summary>
    [Required]
    public Guid ReportTemplateId { get; set; }

    /// <summary>
    /// Runtime parameter values (parameter name -> value)
    /// </summary>
    public Dictionary<string, object>? Parameters { get; set; }

    /// <summary>
    /// Output format for export (null for in-app display only)
    /// </summary>
    public ReportFormat? OutputFormat { get; set; }
}
