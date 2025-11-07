using System.ComponentModel.DataAnnotations;

namespace ThrottlePro.Shared.Entities;

/// <summary>
/// Represents a parent company/tenant (e.g., Grease Monkey Corporate, Jiffy Lube Franchise Group)
/// </summary>
public class Parent
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? BrandName { get; set; }

    [MaxLength(500)]
    public string? LogoUrl { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
    public virtual ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();
    public virtual ICollection<Segment> Segments { get; set; } = new List<Segment>();
    public virtual ICollection<Journey> Journeys { get; set; } = new List<Journey>();
}
