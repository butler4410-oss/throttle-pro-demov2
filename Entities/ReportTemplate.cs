using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.Entities;

/// <summary>
/// Represents a report template definition (system or user-created)
/// Contains the report configuration, visualization settings, and metadata
/// </summary>
[Index(nameof(ParentId), nameof(Category))]
[Index(nameof(ParentId), nameof(IsSystemTemplate))]
public class ReportTemplate : TenantEntity
{
    /// <summary>
    /// Display name of the report template
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Detailed description of what this report shows
    /// </summary>
    [MaxLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// Category for grouping reports
    /// </summary>
    public ReportCategory Category { get; set; }

    /// <summary>
    /// Type of report visualization
    /// </summary>
    public ReportType Type { get; set; }

    /// <summary>
    /// JSON-serialized ReportConfiguration object containing query definition
    /// Includes fields, filters, grouping, sorting, and calculations
    /// </summary>
    [Required]
    public string ConfigurationJson { get; set; } = string.Empty;

    /// <summary>
    /// JSON-serialized ReportVisualization object for chart settings
    /// Null for table-only reports
    /// </summary>
    public string? VisualizationJson { get; set; }

    /// <summary>
    /// Indicates if this is a built-in system template
    /// System templates cannot be deleted or modified by users
    /// </summary>
    public bool IsSystemTemplate { get; set; } = false;

    /// <summary>
    /// Indicates if this template is available to all users in the tenant
    /// Private templates are only visible to the creator
    /// </summary>
    public bool IsPublic { get; set; } = false;

    /// <summary>
    /// User ID of the creator (null for system templates)
    /// </summary>
    public Guid? CreatedByUserId { get; set; }

    /// <summary>
    /// Number of times this report has been executed
    /// Used for popularity tracking and analytics
    /// </summary>
    public int TimesRun { get; set; } = 0;

    /// <summary>
    /// Timestamp of the most recent execution
    /// </summary>
    public DateTime? LastRunAt { get; set; }

    /// <summary>
    /// Indicates if the template is active and available for use
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Navigation properties

    /// <summary>
    /// Scheduled executions using this template
    /// </summary>
    public virtual ICollection<ReportSchedule> Schedules { get; set; } = new List<ReportSchedule>();

    /// <summary>
    /// Execution history for this template
    /// </summary>
    public virtual ICollection<ReportExecution> Executions { get; set; } = new List<ReportExecution>();
}
