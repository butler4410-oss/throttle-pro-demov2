using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ThrottlePro.Shared.Entities;

/// <summary>
/// Represents a physical store location under a Parent
/// </summary>
[Index(nameof(ParentId), nameof(IsActive))]
[Index(nameof(ParentId), nameof(StoreNumber))]
[Index(nameof(ParentId), nameof(City), nameof(State))]
public class Store : TenantEntity
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? StoreNumber { get; set; }

    [MaxLength(200)]
    public string? Address { get; set; }

    [MaxLength(100)]
    public string? City { get; set; }

    [MaxLength(50)]
    public string? State { get; set; }

    [MaxLength(20)]
    public string? ZipCode { get; set; }

    [MaxLength(50)]
    public string? Phone { get; set; }

    [MaxLength(200)]
    public string? Email { get; set; }

    public bool IsActive { get; set; } = true;

    // Navigation properties

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
