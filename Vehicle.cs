using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ThrottlePro.Shared.Entities;

/// <summary>
/// Represents a vehicle owned by a customer
/// </summary>
[Index(nameof(CustomerId))]
[Index(nameof(ParentId), nameof(CustomerId))]
[Index(nameof(VIN), IsUnique = true)]
[Index(nameof(LicensePlate))]
public class Vehicle : TenantEntity
{
    [Required]
    public Guid CustomerId { get; set; }

    [MaxLength(100)]
    public string? Make { get; set; }

    [MaxLength(100)]
    public string? Model { get; set; }

    public int? Year { get; set; }

    [MaxLength(50)]
    public string? Color { get; set; }

    [MaxLength(50)]
    public string? LicensePlate { get; set; }

    [MaxLength(50)]
    public string? VIN { get; set; }

    public int? Mileage { get; set; }

    public bool IsPrimary { get; set; } = false;

    // Navigation properties
    [ForeignKey(nameof(CustomerId))]
    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
