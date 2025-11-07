using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ThrottlePro.Shared.Entities;

/// <summary>
/// Represents a parent company/tenant (e.g., Grease Monkey Corporate, Jiffy Lube Franchise Group).
/// Each Parent is a separate tenant in the multi-tenant architecture.
/// </summary>
[Index(nameof(Name))]
[Index(nameof(IsActive))]
public class Parent : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? BrandName { get; set; }

    [MaxLength(500)]
    public string? LogoUrl { get; set; }

    public bool IsActive { get; set; } = true;

    // Navigation properties
    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
    public virtual ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();
    public virtual ICollection<Segment> Segments { get; set; } = new List<Segment>();
    public virtual ICollection<Journey> Journeys { get; set; } = new List<Journey>();
    public virtual ICollection<ReportTemplate> ReportTemplates { get; set; } = new List<ReportTemplate>();
    public virtual ICollection<ReportSchedule> ReportSchedules { get; set; } = new List<ReportSchedule>();
    public virtual ICollection<ReportExecution> ReportExecutions { get; set; } = new List<ReportExecution>();
    public virtual ICollection<ReportDataSource> ReportDataSources { get; set; } = new List<ReportDataSource>();
}
