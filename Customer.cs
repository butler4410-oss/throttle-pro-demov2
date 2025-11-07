using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.Entities;

/// <summary>
/// Represents a customer in the system
/// </summary>
[Index(nameof(Email), IsUnique = true)]
[Index(nameof(ParentId), nameof(LastVisitDate))]
[Index(nameof(ParentId), nameof(LifecycleStage))]
[Index(nameof(ParentId), nameof(Email))]
public class Customer : TenantEntity
{

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? Phone { get; set; }

    [MaxLength(200)]
    public string? Address { get; set; }

    [MaxLength(100)]
    public string? City { get; set; }

    [MaxLength(50)]
    public string? State { get; set; }

    [MaxLength(20)]
    public string? ZipCode { get; set; }

    public DateTime? DateOfBirth { get; set; }

    // Lifecycle tracking
    public CustomerLifecycleStage LifecycleStage { get; set; } = CustomerLifecycleStage.New;

    public DateTime? FirstVisitDate { get; set; }

    public DateTime? LastVisitDate { get; set; }

    public int TotalVisits { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalSpent { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal AverageOrderValue { get; set; } = 0;

    public int DaysSinceLastVisit { get; set; } = 0;

    // Marketing preferences
    public bool EmailOptIn { get; set; } = true;

    public bool SmsOptIn { get; set; } = false;

    public bool DirectMailOptIn { get; set; } = true;

    // Metadata
    public bool IsActive { get; set; } = true;

    // Navigation properties

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
    public virtual ICollection<CustomerSegment> CustomerSegments { get; set; } = new List<CustomerSegment>();
}
