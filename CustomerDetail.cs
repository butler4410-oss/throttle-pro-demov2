using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.DTOs;

/// <summary>
/// Complete customer information with related data
/// </summary>
public class CustomerDetail
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public DateTime? DateOfBirth { get; set; }
    
    public CustomerLifecycleStage LifecycleStage { get; set; }
    public DateTime? FirstVisitDate { get; set; }
    public DateTime? LastVisitDate { get; set; }
    public int TotalVisits { get; set; }
    public decimal TotalSpent { get; set; }
    public decimal AverageOrderValue { get; set; }
    public int DaysSinceLastVisit { get; set; }
    
    public bool EmailOptIn { get; set; }
    public bool SmsOptIn { get; set; }
    public bool DirectMailOptIn { get; set; }
    
    public List<VehicleSummary> Vehicles { get; set; } = new();
    public List<VisitSummary> RecentVisits { get; set; } = new();
    public List<string> Segments { get; set; } = new();
}

public class VehicleSummary
{
    public Guid Id { get; set; }
    public string? Make { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }
    public string? LicensePlate { get; set; }
    public bool IsPrimary { get; set; }
}

public class VisitSummary
{
    public Guid Id { get; set; }
    public DateTime VisitDate { get; set; }
    public string StoreName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string? ServicesPerformed { get; set; }
}
