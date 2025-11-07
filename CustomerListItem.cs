using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.DTOs;

/// <summary>
/// Lightweight customer data for list views
/// </summary>
public class CustomerListItem
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public CustomerLifecycleStage LifecycleStage { get; set; }
    public DateTime? LastVisitDate { get; set; }
    public int TotalVisits { get; set; }
    public decimal TotalSpent { get; set; }
    public int DaysSinceLastVisit { get; set; }
    public bool EmailOptIn { get; set; }
    public bool SmsOptIn { get; set; }
}
