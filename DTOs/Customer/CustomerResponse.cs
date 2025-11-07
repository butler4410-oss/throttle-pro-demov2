using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.DTOs.Customer;

/// <summary>
/// Response DTO for Customer entity - includes all display data
/// </summary>
public class CustomerResponse
{
    public Guid Id { get; set; }
    public Guid ParentId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public DateTime? DateOfBirth { get; set; }

    // Lifecycle tracking
    public CustomerLifecycleStage LifecycleStage { get; set; }
    public DateTime? FirstVisitDate { get; set; }
    public DateTime? LastVisitDate { get; set; }
    public int TotalVisits { get; set; }
    public decimal TotalSpent { get; set; }
    public decimal AverageOrderValue { get; set; }
    public int DaysSinceLastVisit { get; set; }

    // Marketing preferences
    public bool EmailOptIn { get; set; }
    public bool SmsOptIn { get; set; }
    public bool DirectMailOptIn { get; set; }

    // Metadata
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
