using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.DTOs.Reporting;

/// <summary>
/// Lightweight DTO for displaying report templates in lists
/// </summary>
public class ReportTemplateDto
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Report template name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description of what the report shows
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Report category
    /// </summary>
    public ReportCategory Category { get; set; }

    /// <summary>
    /// Report type (Table, Chart, Dashboard, etc.)
    /// </summary>
    public ReportType Type { get; set; }

    /// <summary>
    /// Whether this is a built-in system template
    /// </summary>
    public bool IsSystemTemplate { get; set; }

    /// <summary>
    /// Whether this template is public to all users
    /// </summary>
    public bool IsPublic { get; set; }

    /// <summary>
    /// Number of times this report has been run
    /// </summary>
    public int TimesRun { get; set; }

    /// <summary>
    /// Timestamp of the most recent execution
    /// </summary>
    public DateTime? LastRunAt { get; set; }

    /// <summary>
    /// Timestamp when the template was created
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
