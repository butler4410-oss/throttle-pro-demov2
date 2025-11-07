using System.ComponentModel.DataAnnotations;
using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.DTOs.Reporting;

/// <summary>
/// Request DTO for creating a new report template
/// </summary>
public class CreateReportTemplateRequest
{
    /// <summary>
    /// Display name for the report template
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description of what the report shows
    /// </summary>
    [MaxLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// Report category for organization
    /// </summary>
    [Required]
    public ReportCategory Category { get; set; }

    /// <summary>
    /// Type of report visualization
    /// </summary>
    [Required]
    public ReportType Type { get; set; }

    /// <summary>
    /// Complete report configuration (data source, fields, filters, etc.)
    /// </summary>
    [Required]
    public ReportConfiguration Configuration { get; set; } = new();

    /// <summary>
    /// Visualization settings for chart reports
    /// </summary>
    public ReportVisualization? Visualization { get; set; }

    /// <summary>
    /// Whether this template should be available to all users
    /// </summary>
    public bool IsPublic { get; set; } = false;
}
